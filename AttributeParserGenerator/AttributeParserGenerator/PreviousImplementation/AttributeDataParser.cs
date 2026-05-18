using Microsoft.CodeAnalysis;

namespace AttributeParserGenerator.PreviousImplementation;

public partial class AttributeDataParser
{
    public ParseResult Parse(AttributeData attributeData)
    {
        return new ParseResult(attributeData, this);
    }

    private object? GetValue(int index, AttributeData attributeData)
    {
        var constructorArguments = attributeData.ConstructorArguments.Length;
        if (index < constructorArguments)
        {
            var argument = attributeData.ConstructorArguments[index];
            return argument.Value;
        }

        var namedArgument =
            attributeData.NamedArguments[index - constructorArguments];
        return namedArgument.Value.Value;
    }

    private string GetName(int index, AttributeData attributeData)
    {
        if (index < attributeData.ConstructorArguments.Length)
        {
            return attributeData.AttributeConstructor?.Parameters[index].Name ?? "";
        }

        return attributeData.NamedArguments[index - attributeData.ConstructorArguments.Length]
            .Key;
    }
}
