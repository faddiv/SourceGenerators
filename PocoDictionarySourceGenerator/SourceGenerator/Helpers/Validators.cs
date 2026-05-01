using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Foxy.PocoDictionary.SourceGenerator.Helpers;

internal static class Validators
{
    public static bool HasError(INamedTypeSymbol typeSymbol)
    {
        return typeSymbol is IErrorTypeSymbol;
    }

    public static bool IsPartial(INamedTypeSymbol typeSymbol, CancellationToken cancellationToken = default)
    {
        return typeSymbol.DeclaringSyntaxReferences
            .Select(reference => reference.GetSyntax(cancellationToken))
            .OfType<TypeDeclarationSyntax>()
            .Any(static syntax => syntax.Modifiers.Any(static m => m.IsKind(Microsoft.CodeAnalysis.CSharp.SyntaxKind.PartialKeyword)));
    }
}
