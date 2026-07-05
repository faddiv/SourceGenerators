using System.Collections.Concurrent;
using System.Text;
using Microsoft.CodeAnalysis;

namespace AttributeParserGenerator.Core;

public partial class AttributeDataParser
{
    private readonly ConcurrentDictionary<string, string> _nameCache = new();

    public ParseResult Parse(AttributeData attributeData)
    {
        return new ParseResult(attributeData, this);
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
