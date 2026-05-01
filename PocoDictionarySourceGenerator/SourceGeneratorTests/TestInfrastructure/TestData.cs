using Foxy.PocoDictionary.SourceGenerator.Data;
using Foxy.PocoDictionary.SourceGenerator.Helpers;

namespace SourceGeneratorTests.TestInfrastructure;

internal static class TestData
{
    public static SuccessfulCollectedData CreateSuccessfulCollectedData(
        string? typeName = null,
        string? @namespace = null,
        ComparableArray<string>? outerTypes = null,
        ComparableArray<CandidatePropertyInfo>? properties = null)
    {
        return new SuccessfulCollectedData(
            new CandidateTypeInfo(
                typeName ?? "TestType",
                @namespace ?? "TestNamespace",
                outerTypes ?? ["OuterType1", "OuterType2"],
                properties ?? [
                    CreatePropertyInfo(),
                    CreatePropertyInfo("AnotherProperty")
                ]));
    }

    public static CandidatePropertyInfo CreatePropertyInfo(
        string propertyName = "TestProperty")
    {
        return new CandidatePropertyInfo(propertyName);
    }
}
