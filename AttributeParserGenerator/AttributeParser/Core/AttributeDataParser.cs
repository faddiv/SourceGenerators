using System.Collections.Concurrent;
using Microsoft.CodeAnalysis;

namespace AttributeParser.Core;

public partial class AttributeDataParser
{
    public ParseResult Parse(AttributeData attributeData)
    {
        return new ParseResult(attributeData, this);
    }
}
