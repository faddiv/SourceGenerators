using System.Linq;
using System.Threading;
using Foxy.PocoDictionary.SourceGenerator.Data;
using Foxy.PocoDictionary.SourceGenerator.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Foxy.PocoDictionary.SourceGenerator;

partial class PocoDictionaryIncrementalGenerator
{
    private static CollectedData? CollectData(
        GeneratorAttributeSyntaxContext context,
        CancellationToken cancel)
    {
        if (context.TargetNode is not TypeDeclarationSyntax targetNode ||
            context.TargetSymbol is not INamedTypeSymbol typeSymbol)
        {
            return null;
        }

        if (Validators.HasError(typeSymbol))
        {
            return null;
        }

        //Is partial class?
        if (!Validators.IsPartial(typeSymbol, cancel))
        {
            return new FailedCollectedData([
                DiagnosticInfo.Create(
                    DiagnosticReports.ClassMustBePartial,
                    targetNode.Identifier.GetLocation(),
                    typeSymbol.Name)
            ]);
        }

        var properties = typeSymbol.GetMembers()
            .OfType<IPropertySymbol>()
            .Where(PropertyFilter)
            .Select(property => new CandidatePropertyInfo(property.Name))
            .ToArray();

        var containingNamespace =
            typeSymbol.ContainingNamespace.IsGlobalNamespace
                ? null
                : typeSymbol.ContainingNamespace.ToDisplayString(DisplayFormats.ForFileName);
        return new SuccessfulCollectedData(
            new CandidateTypeInfo(
                TypeName: typeSymbol.Name,
                Namespace: containingNamespace,
                SemanticHelpers.GetTypeHierarchy(typeSymbol.ContainingType),
                properties,
                SemanticHelpers.GetTypeKind(typeSymbol)));

        bool PropertyFilter(IPropertySymbol property)
        {
            return property is { IsStatic: false, IsIndexer: false } &&
                   (!typeSymbol.IsRecord || property.Name != "EqualityContract");
        }
    }
}
