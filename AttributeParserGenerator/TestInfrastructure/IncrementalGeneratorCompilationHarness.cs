using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace TestInfrastructure;

public class IncrementalGeneratorCompilationHarness<TGenerator> : CompilationHarness
    where TGenerator : IIncrementalGenerator, new()

{
    protected IncrementalGeneratorCompilationHarness()
    {
        AddMetadataReference(typeof(TGenerator));
    }

    public GeneratorDriver RunSourceGenerator(CancellationToken token)
    {
        var compilation = CompileNoDiagnostics(token);

        var generator = new TGenerator();
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);
        return driver.RunGenerators(compilation, token);
    }
}
