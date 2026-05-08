using System;

namespace Foxy.Params.SourceGenerator.Data;

internal record SuccessfulParams : ParamsCandidate
{
    public override bool HasErrors => false;

    public required MethodInfo MethodInfo { get; init; }

    public required int MaxOverrides { get; init; }

    public required bool HasParams { get; init; }
}

