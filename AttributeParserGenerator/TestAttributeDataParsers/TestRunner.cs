using AttributeParser.Core;
using Microsoft.CodeAnalysis;

namespace AttributeParserGenerator.TestInfrastructure;

public class TestRunner
{
    public static Dictionary<string, object?> ExtractValues(AttributeDataParser parser, AttributeData attributeData)
    {
        var results = new Dictionary<string, object?>();
        foreach (var result in parser.Parse(attributeData))
        {
            var name = result.GetName();
            var value = result.GetValue();
            results[name] = value;
        }

        return results;
    }
}
