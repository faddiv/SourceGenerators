using SourceGeneratorTools;

namespace Foxy.Params.SourceGenerator.Data;

public record SuccessfulParamsGroupCandidate : ParamsCandidate
{
    public override bool HasErrors => false;

    public required CandidateTypeInfo TypeInfo { get; init; }

    public required ComparableArray<SuccessfulParams> ParamCandidates { get; init; }
}

