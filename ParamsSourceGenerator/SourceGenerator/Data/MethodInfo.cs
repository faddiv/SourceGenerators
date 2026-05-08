using Foxy.Params.SourceGenerator.Helpers;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using SourceGeneratorTools;

namespace Foxy.Params.SourceGenerator.Data;

internal sealed record MethodInfo
{
    public required string ReturnType { get; init; }

    public string SpanArgumentType => ParamsArgument.GetFirstGenericType();

    public required ComparableArray<ParameterInfo> Parameters { get; init; }

    public required ReturnKind ReturnsKind { get; init; }

    public required ComparableArray<GenericTypeInfo> TypeConstraints { get; init; }

    public required string MethodName { get; init; }

    public required bool IsStatic { get; init; }

    private ParameterInfo ParamsArgument => Parameters[^1];

    public IEnumerable<ParameterInfo> GetFixedParameters()
    {
        return Parameters.Take(Parameters.Count - 1);
    }

    public IEnumerable<string> GetFixArguments()
    {
        return GetFixedParameters().Select(e => e.ToParameter()).ToList();
    }

    public string GetArgName()
    {
        return ParamsArgument.Name;
    }

    public string GetArgNameSpan()
    {
        return $"{ParamsArgument.Name}Span";
    }

    public string GetArgNameSpanInput()
    {
        return ParamsArgument.IsRefType
            ? $"ref {GetArgNameSpan()}"
            : GetArgNameSpan();
    }

    public static string CreateReturnTypeFor(IMethodSymbol methodSymbol)
    {
        var returnType = methodSymbol.ReturnType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        var isNullable = methodSymbol.ReturnType.NullableAnnotation == NullableAnnotation.Annotated;
        if (methodSymbol.ReturnsByRef)
        {
            return SemanticHelpers.WithModifiers(returnType, RefKind.Ref, isNullable);
        }
        else if (methodSymbol.ReturnsByRefReadonly)
        {
            return SemanticHelpers.WithModifiers(returnType, RefKind.RefReadOnlyParameter, isNullable);
        }

        return SemanticHelpers.WithModifiers(returnType, RefKind.None, isNullable);
    }

    public static ParameterInfo[] GetArguments(IMethodSymbol methodSymbol)
    {
        return methodSymbol.Parameters
            .Select(arg => new ParameterInfo(
                Type: TypeInfo.Create(arg.Type),
                Name: arg.Name,
                RefKind: arg.RefKind,
                IsNullable: arg.NullableAnnotation == NullableAnnotation.Annotated))
            .ToArray();
    }

    public static GenericTypeInfo[] CreateTypeConstraints(ImmutableArray<ITypeSymbol> typeArguments)
    {
        var typeConstraintsList = new GenericTypeInfo[typeArguments.Length];
        for (int i = 0; i < typeArguments.Length; i++)
        {
            var typeArg = (ITypeParameterSymbol)typeArguments[i];
            typeConstraintsList[i] = new GenericTypeInfo
            {
                Type = typeArg.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
                ConstraintType = SemanticHelpers.GetConstraintType(typeArg),
                ConstraintTypes = typeArg.ConstraintTypes
                    .Select(e => e.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)).ToArray(),
                HasConstructorConstraint = typeArg.HasConstructorConstraint
            };
        }

        return typeConstraintsList;
    }
}
