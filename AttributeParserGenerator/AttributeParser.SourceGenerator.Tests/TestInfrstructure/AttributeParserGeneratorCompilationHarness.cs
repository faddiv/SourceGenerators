namespace AttributeParser.SourceGenerator.Tests;

public class AttributeParserGeneratorCompilationHarness
    : IncrementalGeneratorCompilationHarness<AttributeParserGenerator>
{
    public AttributeParserGeneratorCompilationHarness()
    {
        AddMetadataReference(typeof(AttributeParserAttribute));
    }
}
