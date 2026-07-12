using System.Collections.Immutable;
using System.Linq;
using AttributeParser.Core;
using AttributeParser.SourceGenerator.Data;
using Microsoft.CodeAnalysis;

namespace AttributeParser.SourceGenerator;

[Generator]
public partial class AttributeParserGenerator : IIncrementalGenerator
{
    private static readonly string _attributeParserAttributeName = typeof(AttributeParserAttribute).FullName!;
    private static readonly string _attributeDataParserTypeName = typeof(AttributeDataParser).FullName!;
    private static readonly string _attributeDataTypeName = typeof(AttributeData).FullName!;

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var declarations = context.SyntaxProvider
            .ForAttributeWithMetadataName(_attributeParserAttributeName,
                (syntaxContext, cancellationToken) => true,
                CollectData)
            .Collect()
            .SelectMany((array, token) =>
            {
                var results = ImmutableArray.CreateBuilder<Result>();
                results.AddRange(array.OfType<Error>());
                results.AddRange(array.OfType<ParserMethodData>()
                    .GroupBy(e => e.ContainingType)
                    .Select(e => new ParserClassData(e.Key, e.ToArray())));
                return results.ToImmutable();
            });

        context.RegisterImplementationSourceOutput(declarations, GenerateSource);
    }
}
