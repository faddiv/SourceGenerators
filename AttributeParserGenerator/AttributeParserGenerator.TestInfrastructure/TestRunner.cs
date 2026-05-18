using AttributeParserGenerator.Core;
using Microsoft.CodeAnalysis;

namespace AttributeParserGenerator.TestInfrastructure;

public class TestRunner
{

    public static Dictionary<string, object?> ExtractValues(AttributeDataParser parser, AttributeData attributeData)
    {
        var results = new Dictionary<string, object?>();
        foreach (var result in parser.Parse(attributeData))
        {
            results[result.GetName()] = result.GetValue();
        }

        return results;
    }
}
