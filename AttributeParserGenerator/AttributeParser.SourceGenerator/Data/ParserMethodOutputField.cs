namespace AttributeParser.SourceGenerator.Data;

public record ParserMethodOutputField(string Name, string Type, string? EnumerableElementType)
{
    public bool IsEnumerable => EnumerableElementType is not null;
}
