namespace Foxy.Params.SourceGenerator.Data;

internal sealed record SuccessfulParamsCandidate(
    CandidateTypeInfo TypeInfo,
    int MaxOverrides,
    bool HasParams,
    MethodInfo MethodInfo)
    : ParamsCandidate
{
    public override bool HasErrors => false;
}
