using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using TUnit.Core.Exceptions;
using TUnit.Engine.Exceptions;

namespace AttributeParser.SourceGenerator.Tests;

public class CompilationHarness<TGenerator> where TGenerator : IIncrementalGenerator, new()
{
    private readonly List<SyntaxTree> _syntaxTrees = [];
    private readonly List<MetadataReference> _references = [];

    public CSharpCompilationOptions CompilationOptions { get; } = new(
        OutputKind.DynamicallyLinkedLibrary,
        nullableContextOptions: NullableContextOptions.Enable);

    public void AddSource([StringSyntax("csharp")] string source, CancellationToken token, string? fileName = null)
    {
        var text = SourceText.From(source);
        var syntaxTree = CSharpSyntaxTree.ParseText(text, CSharpParseOptions.Default, fileName ?? "", token);

        if (fileName is null && TryGetFirstNamedSyntaxElement(syntaxTree, out var elementName))
        {
            syntaxTree = syntaxTree.WithFilePath($"{elementName}.cs");
        }

        _syntaxTrees.Add(syntaxTree);
    }

    private static bool TryGetFirstNamedSyntaxElement(
        SyntaxTree syntaxTree,
        [NotNullWhen(true)] out string? elementName)
    {
        var root = syntaxTree.GetRoot();
        var firstNamedElement = root.DescendantNodes()
            .OfType<Microsoft.CodeAnalysis.CSharp.Syntax.BaseTypeDeclarationSyntax>()
            .FirstOrDefault();

        if (firstNamedElement is not null)
        {
            elementName = firstNamedElement.Identifier.Text;
            return true;
        }

        elementName = null;
        return false;
    }

    public void AddMetadataReferencesFromCurrentDomain()
    {
        string runtimeDir = RuntimeEnvironment.GetRuntimeDirectory();
        var currentDomain = AppDomain.CurrentDomain;
        var assemblies = currentDomain.GetAssemblies();
        foreach (var assembly in assemblies
                     .Where(x => !x.IsDynamic && !string.IsNullOrWhiteSpace(x.Location) &&
                                 x.Location.StartsWith(runtimeDir, StringComparison.OrdinalIgnoreCase)))
        {
            AddMetadataReference(assembly);
        }
    }

    public void AddMetadataReference(Assembly assembly)
    {
        var assemblyLocation = assembly.Location;
        if (_references.OfType<PortableExecutableReference>().Any(x => x.FilePath == assemblyLocation))
        {
            return;
        }

        var metadataReference = MetadataReference.CreateFromFile(assemblyLocation);
        _references.Add(metadataReference);
    }

    public void AddMetadataReference(Type type)
    {
        AddMetadataReference(type.Assembly);
    }

    public CSharpCompilation Compile()
    {
        return CSharpCompilation.Create(
            "SampleAssembly.dll",
            _syntaxTrees,
            _references,
            options: CompilationOptions);
    }

    public GeneratorDriver RunSourceGenerator(CancellationToken token)
    {
        var compilation = Compile();
        var diagnostics = compilation.GetDiagnostics(token);
        if (diagnostics.Any(d => d.Id != "CS8795"))
        {
            var builder = new StringBuilder();
            builder.AppendLine("Compilation diagnostics:");
            foreach (var diagnostic in diagnostics)
            {
                builder.AppendLine(diagnostic.ToString());
            }

            throw new TestFailedException(builder.ToString(), null);
        }

        var generator = new TGenerator();
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);
        return driver.RunGenerators(compilation, token);
    }
}
