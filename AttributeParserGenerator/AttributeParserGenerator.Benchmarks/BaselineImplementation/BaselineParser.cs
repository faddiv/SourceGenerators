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
                results[attributeConstructor.Parameters[index].Name] = argument.Value;
            }
        }

        foreach (var argument in attributeData.NamedArguments)
        {
            var name = $"{char.ToLower(argument.Key[0])}{argument.Key[1..]}";
            results[name] = argument.Value.Value;
        }

        return results;
    }
}
