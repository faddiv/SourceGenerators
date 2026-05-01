using System.Threading;
using Foxy.PocoDictionary.SourceGenerator.Data;
using Foxy.PocoDictionary.SourceGenerator.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Foxy.PocoDictionary.SourceGenerator;

[Generator]
public partial class PocoDictionaryIncrementalGenerator : IIncrementalGenerator
{
    private const string AttributeName = "Foxy.PocoDictionary.PocoDictionaryAttribute";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(AddPocoDictionaryAttribute);
        var declarations = context.SyntaxProvider.ForAttributeWithMetadataName(
            AttributeName,
            predicate: Filter,
            transform: CollectData)
            .WithTrackingName(TrackingNames.CollectData)
            .NotNull()
            .WithTrackingName(TrackingNames.NotNullFilter);

        context.RegisterSourceOutput(declarations, GenerateSource);
    }

    private static bool Filter(SyntaxNode s, CancellationToken token)
    {
        return s is TypeDeclarationSyntax and not ExtensionBlockDeclarationSyntax;
    }

}

