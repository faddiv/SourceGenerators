using System;
using Microsoft.CodeAnalysis;

namespace Foxy.PocoDictionary.SourceGenerator.Helpers;

internal static class SemanticHelpers
{
    public static string[] GetTypeHierarchy(INamedTypeSymbol symbol)
    {
        var nestLevel = CountTypeNesting(symbol);
        return CreateTypeHierarchyInternal(symbol, nestLevel);

        static string[] CreateTypeHierarchyInternal(INamedTypeSymbol? symbol, int level)
        {
            var count = 1;
            var container = new string[level];
            while (symbol is not null)
            {
                container[^count] = symbol.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);
                symbol = symbol.ContainingType;
                count++;
            }

            return container;
        }

        static int CountTypeNesting(INamedTypeSymbol? symbol)
        {
            var count = 0;
            while (symbol is not null)
            {
                symbol = symbol.ContainingType;
                count++;
            }

            return count;
        }
    }

    public static string CreateFileName(string containingType)
    {
        return $"{containingType}.g.cs";
    }

    public static string GetTypeKind(INamedTypeSymbol typeSymbol)
    {
        return typeSymbol.TypeKind switch
        {
            TypeKind.Class when typeSymbol.IsRecord => "record",
            TypeKind.Class when !typeSymbol.IsRecord => "class",
            TypeKind.Struct when typeSymbol.IsRecord => "record struct",
            TypeKind.Struct when !typeSymbol.IsRecord => "struct",
            _ => throw new InvalidOperationException($"Unsupported type kind {typeSymbol.TypeKind}")
        };
    }
}
