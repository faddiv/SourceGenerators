using System.Collections.Generic;

namespace AttributeParser.SourceGenerator.Data;

public record ParserMethodData(
    ContainingType ContainingType,
    string MethodName,
    ParserMethodOutput Output,
    ParserMethodParameter AttributeParser,
    ParserMethodParameter Attribute) : Result
{
    public IEnumerable<string> SerializeParameters()
    {
        for (var i = 0; i < 2; i++)
        {
            if (i == AttributeParser.Index)
                yield return $"{AttributeParser.Type} {AttributeParser.Name}";
            else
                yield return $"{Attribute.Type} {Attribute.Name}";
        }
    }
}

public record ContainingType(
    string Namespace,
    string Name) : Result
{
    public static ContainingType Unknown => new(string.Empty, string.Empty);
}
