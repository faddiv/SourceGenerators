using Foxy.PocoDictionary.SourceGenerator.Data;
using Foxy.PocoDictionary.SourceGenerator.Helpers;
using Foxy.PocoDictionary.SourceGenerator.SourceGenerator;
using Microsoft.CodeAnalysis;

namespace Foxy.PocoDictionary.SourceGenerator;

partial class PocoDictionaryIncrementalGenerator
{
    private static void GenerateSource(SourceProductionContext context, CollectedData typeSymbols)
    {
        if (typeSymbols is FailedCollectedData fail)
        {
            foreach (var diagnostic in fail.Diagnostics)
            {
                context.ReportDiagnostic(diagnostic.ToDiagnostics());
            }
        }
        else if (typeSymbols is SuccessfulCollectedData data)
        {
            using var generator = new OutputGenerator(data);
            context.AddSource(
                SemanticHelpers.CreateFileName(data.TypeInfo.FullName),
                generator.Execute());
        }
        else
        {
            string diagnosticMessage = $"Invalid CollectedData: {typeSymbols.GetType().Name}";
            Diagnostic diagnostic = Diagnostic.Create(DiagnosticReports.InternalError, null, diagnosticMessage);
            context.ReportDiagnostic(diagnostic);
        }
    }
}
