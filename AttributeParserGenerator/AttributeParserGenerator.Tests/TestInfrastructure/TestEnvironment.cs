using System.Linq;
using System.Threading;
using AttributeParserGenerator.Sample;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace AttributeParserGenerator.Tests.Tests;

public class TestEnvironment
{
    private readonly Lazy<CSharpCompilation> _compilation =
        new(CreateCompilationCreateCompilation, LazyThreadSafetyMode.ExecutionAndPublication);

    public CSharpCompilation Compilation => _compilation.Value;

    private static CSharpCompilation CreateCompilationCreateCompilation()
    {
        var syntaxTrees = TestHelpers.GetSyntaxes();
        var compilationOptions = new CSharpCompilationOptions(
            OutputKind.DynamicallyLinkedLibrary,
            nullableContextOptions: NullableContextOptions.Enable);
        var references = TestHelpers.GetFrameworkReferences();

        return CSharpCompilation.Create(
            "SampleAssembly.dll",
            syntaxTrees,
            references,
            options: compilationOptions);
    }

    public AttributeData GetClassAttributeData(string name)
    {
        return Compilation.GetSymbolsWithName(n => n == name, SymbolFilter.Type)
            .FirstOrDefault()?
            .GetAttributes()
            .FirstOrDefault() ?? throw new InvalidOperationException($"Attribute not found on class {name}");
    }
}
