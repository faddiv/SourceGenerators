using Microsoft.CodeAnalysis;

namespace AttributeParserGenerator.Core;

public partial class AttributeDataParser
{
    public ParseResult Parse(AttributeData attributeData)
    {
        return new ParseResult(attributeData, this);
    }

    private object? GetValue(int index, AttributeData attributeData)
    {
        if (index < attributeData.ConstructorArguments.Length)
        {
            var argument = attributeData.ConstructorArguments[index];
            return argument.Value;
        }

        var namedArgument =
            attributeData.NamedArguments[index - attributeData.ConstructorArguments.Length];
        return namedArgument.Value.Value;
    }
}
