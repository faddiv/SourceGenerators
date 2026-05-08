using SourceGeneratorTools;

namespace Foxy.Params.SourceGenerator.Data;

internal sealed record FailedParamsCandidate(
    ComparableArray<DiagnosticInfo> Diagnostics) : ParamsCandidate
{
    public override bool HasErrors => true;
}
