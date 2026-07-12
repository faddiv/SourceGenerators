using SourceGeneratorTools;

namespace AttributeParser.SourceGenerator.Data;

public record ParserClassData(
    ContainingType ThisType,
    ComparableArray<ParserMethodData> ParserMethods) : Result;
