using System.Collections.Concurrent;
using Microsoft.CodeAnalysis;

namespace AttributeParser.Core;

public partial class AttributeDataParser
{
    private readonly ConcurrentDictionary<string, string> _nameCache = new();

    public ParseResult Parse(AttributeData attributeData)
    {
        return new ParseResult(attributeData, this);
    }
}
