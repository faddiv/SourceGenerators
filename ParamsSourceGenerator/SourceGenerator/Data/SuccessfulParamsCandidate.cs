namespace Foxy.Params.SourceGenerator.Data;

internal sealed record SuccessfulParamsCandidate : ParamsCandidate
{
    public required CandidateTypeInfo TypeInfo { get; init; }

    public override bool HasErrors => false;

    public required MethodInfo MethodInfo { get; init; }

    public required int MaxOverrides { get; init; }

    public required bool HasParams { get; init; }
}

