using System.Collections.Concurrent;
using System.Text;
using Microsoft.CodeAnalysis;

namespace AttributeParserGenerator.PreviousImplementation;

public partial class AttributeDataParser
{
    private readonly ConcurrentDictionary<string, string> _nameCache = new();

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

        return ToCamelCase(attributeData.NamedArguments[index - attributeData.ConstructorArguments.Length]
            .Key);
    }

    private string ToCamelCase(string name)
    {
        if (string.IsNullOrEmpty(name) || char.IsLower(name[0]))
        {
            return name;
        }

        return _nameCache.GetOrAdd(name, static name =>
        {
            var sb = new StringBuilder(name.Length);
            sb.Append(char.ToLower(name[0]));
            sb.Append(name, 1, name.Length - 1);
            return sb.ToString();
        });
    }
}
