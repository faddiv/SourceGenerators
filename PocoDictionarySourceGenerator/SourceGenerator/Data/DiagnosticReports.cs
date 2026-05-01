using Microsoft.CodeAnalysis;

namespace Foxy.PocoDictionary.SourceGenerator.Data;

public class DiagnosticReports
{
    public const string Category = "Foxy.PocoDictionary";

    public static DiagnosticDescriptor InternalError { get; } = new(
        "PD1000",
        "Internal error",
        "Internal error occured during source generation: {0}",
        Category,
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true);

    public static DiagnosticDescriptor ClassMustBePartial { get; } = new(
        "PD0001",
        "Class must be partial",
        "Class '{0}' must be declared as partial to be used with PocoDictionary",
        Category,
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true);
}
