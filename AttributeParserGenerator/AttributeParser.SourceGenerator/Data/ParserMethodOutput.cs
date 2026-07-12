using SourceGeneratorTools;

namespace AttributeParser.SourceGenerator.Data;

public record ParserMethodOutput(
    string FullName,
    ComparableArray<ParserMethodOutputField> Parameters);
