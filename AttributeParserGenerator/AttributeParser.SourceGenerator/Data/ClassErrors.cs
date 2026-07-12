using System.Collections.Immutable;

namespace AttributeParser.SourceGenerator.Data;

public record ClassErrors(
    ContainingType ContainingType,
    ImmutableArray<Error> Errors) : Result;
