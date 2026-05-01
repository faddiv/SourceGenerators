using System.Collections.Generic;
using Foxy.PocoDictionary.SourceGenerator.Data;
using SourceGeneratorTests.TestInfrastructure;
using Xunit;

namespace SourceGeneratorTests.Data;

public class EqualityTests
{
    public static IEnumerable<TheoryDataRow<SuccessfulCollectedData>> Data()
    {
        yield return TestData.CreateSuccessfulCollectedData(
            typeName: "DifferentType");

        yield return TestData.CreateSuccessfulCollectedData(
            @namespace: "DifferentNamespace");

        yield return TestData.CreateSuccessfulCollectedData(
            outerTypes: ["OuterType1", "DifferentOuterType2"]);

        yield return TestData.CreateSuccessfulCollectedData(
            properties: [TestData.CreatePropertyInfo(), TestData.CreatePropertyInfo("DifferentProperty")]);
    }

    [Fact]
    public void Equals_WithStructuralEqualObjects_ShouldReturnTrue()
    {
        // Arrange
        var candidate1 = TestData.CreateSuccessfulCollectedData();
        var candidate2 = TestData.CreateSuccessfulCollectedData();

        // Act
        var equals = candidate1.Equals(candidate2);

        // Assert
        Assert.True(equals);
    }

    [Theory]
    [MemberData(nameof(Data))]
    public void Equals_WithDifferentTypeName_ShouldReturnFalse(SuccessfulCollectedData candidate2)
    {
        // Arrange
        var candidate1 = TestData.CreateSuccessfulCollectedData();

        // Act
        var equals = candidate1.Equals(candidate2);

        // Assert
        Assert.False(equals);
    }
}
