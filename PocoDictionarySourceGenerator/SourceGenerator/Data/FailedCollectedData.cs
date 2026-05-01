using System.Collections.Immutable;

namespace Foxy.PocoDictionary.SourceGenerator.Data;

internal sealed record FailedCollectedData(
    ImmutableArray<DiagnosticInfo> Diagnostics) : CollectedData
{
    public override bool HasErrors => true;
}
