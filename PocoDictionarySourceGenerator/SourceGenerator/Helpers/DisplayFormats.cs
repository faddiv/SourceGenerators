using Microsoft.CodeAnalysis;

namespace Foxy.PocoDictionary.SourceGenerator.Helpers;

internal static class DisplayFormats
{
    public static SymbolDisplayFormat ForFileName => new(
        globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Omitted,
        typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
        genericsOptions: SymbolDisplayGenericsOptions.None);
}
