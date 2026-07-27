namespace AttributeParser.SourceGenerator.Tests.TestInfrstructure;

public class AttributeParserGeneratorCompilationHarness
    : IncrementalGeneratorCompilationHarness<AttributeParserGenerator>
{
    public AttributeParserGeneratorCompilationHarness()
    {
        AddMetadataReference(typeof(AttributeParserAttribute));
    }
}
