namespace Foxy.Params.SourceGenerator.Data;

internal sealed record SuccessfulParams(
    MethodInfo MethodInfo,
    bool HasParams,
    int MaxOverrides) : ParamsCandidate
{
    public override bool HasErrors => false;
}

