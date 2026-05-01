namespace Foxy.PocoDictionary.SourceGenerator.Data;

public record SuccessfulCollectedData(CandidateTypeInfo TypeInfo): CollectedData
{
    public override bool HasErrors => false;
}
