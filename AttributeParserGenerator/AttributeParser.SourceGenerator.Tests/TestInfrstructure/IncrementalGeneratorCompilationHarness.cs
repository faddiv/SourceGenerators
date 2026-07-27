using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using TestInfrastructure;
using TUnit.Engine.Exceptions;

namespace AttributeParser.SourceGenerator.Tests.TestInfrstructure;

public class IncrementalGeneratorCompilationHarness<TGenerator> : CompilationHarness
    where TGenerator : IIncrementalGenerator, new()

{
    protected IncrementalGeneratorCompilationHarness()
    {
        AddMetadataReference(typeof(AttributeData));
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
