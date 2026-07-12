using Microsoft.CodeAnalysis;

namespace AttributeParser.SourceGenerator;

public static class DiagnosticReports
{
    private const string Category = "AttributeParser.SourceGenerator";

    public static DiagnosticDescriptor InternalError { get; } = new DiagnosticDescriptor(
        "APG1000",
        "Internal error",
        "Internal error occured during source generation: {0}",
        Category,
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true);
}
