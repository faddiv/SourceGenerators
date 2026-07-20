namespace AttributeParser.SourceGenerator.Tests;

public class GeneratorTests
{
    private readonly AttributeParserGeneratorCompilationHarness _harness = new();

    [Test]
    public Task GeneratesStringParser(CancellationToken token)
    {
        _harness.AddSource(
            """
            public class ParsedData
            {
                public string? Value1 { get; set; }
            }
            """,
            token);

        _harness.AddSource(
            """
            using AttributeParser;
            using AttributeParser.Core;
            using Microsoft.CodeAnalysis;

            public static partial class AttributeParsers
            {
                [AttributeParser]
                public static partial ParsedData ParseData(
                    AttributeData attributeData,
                    AttributeDataParser parser);
            }
            """,
            token);
        var results = _harness.RunSourceGenerator(token)
            .GetRunResult();

        return Verify(results);
    }
}
