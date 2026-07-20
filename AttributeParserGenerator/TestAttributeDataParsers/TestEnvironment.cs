using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace AttributeParserGenerator.TestInfrastructure;

public class TestEnvironment
{
    private readonly Lazy<CSharpCompilation> _compilation =
        new(CreateCompilation, LazyThreadSafetyMode.ExecutionAndPublication);

    public CSharpCompilation Compilation => _compilation.Value;

    private static CSharpCompilation CreateCompilation()
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
                   .FirstOrDefault(a =>
                       a.AttributeClass?.ToDisplayString().StartsWith("AttributeParser") ?? false) ??
               throw new InvalidOperationException($"Attribute not found on class {name}");
    }
}
