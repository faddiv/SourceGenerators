using SourceGeneratorTools;

namespace Foxy.Params.SourceGenerator.Data;

public sealed record FailedParamsCandidate(
    ComparableArray<DiagnosticInfo> Diagnostics) : ParamsCandidate
{
    public override bool HasErrors => true;
}
