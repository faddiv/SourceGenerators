using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;

namespace AttributeParserGenerator.Core;

public partial class AttributeDataParser
{
    private ConcurrentDictionary<string, AttributeTypeInfo> _attributeTypeInfoCache = new();

    public ParseResult Parse(AttributeData attributeData)
    {
        return new ParseResult(attributeData, this);
    }

    private AttributeTypeInfo GetAttributeTypeInfo(AttributeData attributeData)
    {
        var key = attributeData.AttributeClass?.ToDisplayString() ?? "";
        if (_attributeTypeInfoCache.TryGetValue(key, out var info))
        {
            return info;
        }

        var argumentNames = CollectArgumentNames(attributeData.AttributeClass);
        // Create a new AttributeTypeInfo and add it to the cache
        var newInfo = new AttributeTypeInfo
        {
            ArgumentNames = argumentNames
        };
        _attributeTypeInfoCache[key] = newInfo;
        return newInfo;
    }

    private static string[] CollectArgumentNames(INamedTypeSymbol? attributeClass)
    {
        if (attributeClass == null)
        {
            return [];
        }

        var constructorArguments = attributeClass.InstanceConstructors
            .SelectMany(c => c.Parameters)
            .Select(p => p.Name);

        var namedArguments = attributeClass.GetMembers()
            .OfType<IPropertySymbol>()
            .Where(p => p is
            {
                IsStatic: false,
                DeclaredAccessibility: Accessibility.Public,
                SetMethod.DeclaredAccessibility: Accessibility.Public
            })
            .Select(p => FirstCharToLower(p.Name));

        return constructorArguments
            .Concat(namedArguments)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();
    }

    private static string FirstCharToLower(string name)
    {
        if (string.IsNullOrEmpty(name) || char.IsLower(name[0]))
        {
            return name;
        }

        var sb = new StringBuilder(name.Length);
        sb.Append(char.ToLower(name[0]));
        sb.Append(name, 1, name.Length - 1);
        return sb.ToString();
    }
}

internal class AttributeTypeInfo
{
    public required string[] ArgumentNames { get; init; }
    public int CountArguments => ArgumentNames.Length;
}
