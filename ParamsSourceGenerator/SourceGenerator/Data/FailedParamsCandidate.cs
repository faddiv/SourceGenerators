using SourceGeneratorTools;

namespace Foxy.Params.SourceGenerator.Data;

internal record FailedParamsCandidate : ParamsCandidate
{
    public override bool HasErrors => true;

    public required ComparableArray<DiagnosticInfo> Diagnostics { get; init; }
}

