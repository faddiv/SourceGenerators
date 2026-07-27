using TestInfrastructure;
using TUnit.Engine.Exceptions;

namespace AttributeParser.SourceGenerator.Tests.TestInfrstructure;

public class AttributeParserGeneratorCompilationHarness
    : IncrementalGeneratorCompilationHarness<AttributeParserGenerator>
{
    protected override void NoDiagnosticsFailed(string message)
    {
        throw new TestFailedException(message, null);
    }
}
