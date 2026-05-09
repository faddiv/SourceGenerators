using System.Linq;
using Foxy.Params.SourceGenerator.Helpers;
using Microsoft.CodeAnalysis;
using SourceGeneratorTools;

namespace Foxy.Params.SourceGenerator.Data;

public sealed record TypeInfo(
    string TypeName,
    ComparableArray<string> GenericParameters = default)
{
    public static TypeInfo Create(ITypeSymbol type)
    {
        var name = type.ToDisplayString(DisplayFormats.ForRootTypeDisplay);
        if (type is INamedTypeSymbol { TypeArguments.Length: > 0 } namedType)
        {
            return new TypeInfo(name, namedType.TypeArguments.Select(e => e.ToDisplayString(DisplayFormats.ForGenericArgumentFormat)).ToArray());
        }
        else
        {
            return new TypeInfo(name, []);
        }
    }

    public override string ToString()
    {
        return GenericParameters.Count > 0
            ? $"{TypeName}<{string.Join(", ", GenericParameters)}>"
            : TypeName;
    }
}
