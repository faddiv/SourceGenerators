using Microsoft.CodeAnalysis;

namespace AttributeParser.SourceGenerator.Data;

public record Error(
    Location Location,
    string Message) : Result;
