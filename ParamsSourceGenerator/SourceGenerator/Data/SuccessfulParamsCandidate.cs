namespace Foxy.Params.SourceGenerator.Data;

internal record SuccessfulParamsCandidate : SuccessfulParams
{
    public required CandidateTypeInfo TypeInfo { get; init; }
}

