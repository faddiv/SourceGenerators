using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace TestInfrastructure;

public class CompilationHarness
{
    private readonly List<SyntaxTree> _syntaxTrees = [];
    private readonly List<MetadataReference> _references = [];

    protected CompilationHarness()
    {
        AddMetadataReferencesFromCurrentDomain();
        AddMetadataReference(typeof(AttributeData));
    }

    public CSharpCompilationOptions CompilationOptions { get; private set; } = new(
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

    public void AddGlobalUsings(params ReadOnlySpan<string> usings)
    {
        var builder = new StringBuilder();
        foreach (var @using in usings)
        {
            builder.AppendLine($"global using {@using};");
        }
        AddSource(builder.ToString(), CancellationToken.None, "GlobalUsings.cs");
    }

    public CSharpCompilation Compile()
    {
        return CSharpCompilation.Create(
            "SampleAssembly.dll",
            _syntaxTrees,
            _references,
            options: CompilationOptions);
    }

    public CSharpCompilation CompileNoDiagnostics(CancellationToken token)
    {
        var compilation = Compile();
        var diagnostics = compilation.GetDiagnostics(token);
        if (!diagnostics.Any(d => d.Id != "CS8795"))
            return compilation;
        var builder = new StringBuilder();
        builder.AppendLine("Compilation diagnostics:");
        foreach (var diagnostic in diagnostics)
        {
            builder.AppendLine(diagnostic.ToString());
        }

        NoDiagnosticsFailed(builder.ToString());

        return compilation;
    }

    protected virtual void NoDiagnosticsFailed(string message) =>
        throw new ApplicationException(message, null);
}
