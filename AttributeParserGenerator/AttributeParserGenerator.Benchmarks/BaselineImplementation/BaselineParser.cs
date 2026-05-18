using Microsoft.CodeAnalysis;

namespace AttributeParserGenerator.Benchmarks.BaselineImplementation;

public class BaselineParser
{
    public static Dictionary<string, object?> Parse(AttributeData attributeData)
    {
        var results = new Dictionary<string, object?>();
        var attributeConstructor = attributeData.AttributeConstructor;
        if (attributeConstructor is not null)
        {
            for (var index = 0; index < attributeData.ConstructorArguments.Length; index++)
            {
                var argument = attributeData.ConstructorArguments[index];
                results.Add(attributeConstructor.Parameters[index].Name, argument.Value);
            }
        }

        foreach (var argument in attributeData.NamedArguments)
        {
            results.Add(argument.Key, argument.Value.Value);
        }

        return results;
    }
}
