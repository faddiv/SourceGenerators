using SourceGeneratorTools;

namespace Foxy.Params.SourceGenerator.Data;

internal sealed record FailedParamsCandidate : ParamsCandidate
{
    public override bool HasErrors => true;

    public required ComparableArray<DiagnosticInfo> Diagnostics { get; init; }
}

