using Microsoft.CodeAnalysis;

namespace AttributeParser.SourceGenerator.Tests;

public class AttributeParserGeneratorCompilationHarness : CompilationHarness<AttributeParserGenerator>
{
    public AttributeParserGeneratorCompilationHarness()
    {
        AddMetadataReferencesFromCurrentDomain();
        AddMetadataReference(typeof(AttributeData));
        AddMetadataReference(typeof(AttributeParserAttribute));
    }
}
