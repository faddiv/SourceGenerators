using Foxy.Params.SourceGenerator.Helpers;
using Microsoft.CodeAnalysis;

namespace Foxy.Params.SourceGenerator.Data;

public sealed record ParameterInfo(
    TypeInfo Type,
    string Name,
    RefKind RefKind,
    bool IsNullable)
{
    public bool IsRefType => RefKind is RefKind.Ref or RefKind.RefReadOnlyParameter;

    public string ToParameter()
    {
        return $"{SemanticHelpers.WithModifiers(Type.ToString(), RefKind, false)} {Name}";
    }

    public string GetFirstGenericType()
    {
        return SemanticHelpers.WithModifiers(Type.GenericParameters[0], RefKind.None, IsNullable);
    }

    public string ToPassParameter()
    {
        return SemanticHelpers.WithModifiers(Name, GetPassParameterModifier(RefKind), false);
    }

    private static RefKind GetPassParameterModifier(RefKind refKind)
    {
        return refKind switch
        {
            RefKind.Ref => RefKind.Ref,
            RefKind.Out => RefKind.Out,
            RefKind.In or RefKind.RefReadOnlyParameter => RefKind.In,
            _ => RefKind.None,
        };
    }
}
