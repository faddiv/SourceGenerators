using Microsoft.CodeAnalysis;

namespace AttributeParserGenerator.Core;

public partial class AttributeDataParser
{
    public ParseResult Parse(AttributeData attributeData)
    {
        return new ParseResult(attributeData, this);
    }
}
