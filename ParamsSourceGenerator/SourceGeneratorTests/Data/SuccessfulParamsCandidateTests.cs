using SourceGeneratorTests.TestInfrastructure;
using System.Threading.Tasks;

namespace SourceGeneratorTests.Data;

public class SuccessfulParamsCandidateTests
{
    [Test]
    public async Task Equals_WithSameObject_ShouldReturnTrue()
    {
        // Arrange
        var candidate = TestData.CreateSuccessfulParamsCandidate();

        // Act & Assert
        await Assert.That(candidate.Equals(candidate)).IsTrue();
    }

    [Test]
    public async Task Equals_WithEqualObject_ShouldReturnTrue()
    {
        // Arrange
        var candidate1 = TestData.CreateSuccessfulParamsCandidate();

        var candidate2 = TestData.CreateSuccessfulParamsCandidate();

        // Act & Assert
        await Assert.That(candidate1.Equals(candidate2)).IsTrue();
    }

    [Test]
    public async Task Equals_WithNullObject_ShouldReturnFalse()
    {
        // Arrange
        var candidate = TestData.CreateSuccessfulParamsCandidate();

        // Act & Assert
        await Assert.That(candidate.Equals(null)).IsFalse();
    }

    [Test]
    public async Task Equals_WithDifferentType_ShouldReturnFalse()
    {
        // Arrange
        var candidate = TestData.CreateSuccessfulParamsCandidate();

        var differentTypeObject = new { };

        // Act & Assert
        await Assert.That(candidate.Equals(differentTypeObject)).IsFalse();
    }

    [Test]
    public async Task Equals_WithTypeInfoDifferent_ShouldReturnFalse()
    {
        // Arrange
        var candidate1 = TestData.CreateSuccessfulParamsCandidate();

        var candidate2 = TestData.CreateSuccessfulParamsCandidate(
            typeInfo: TestData.CreateCandidateTypeInfo(typeName: "DifferentTypeName"));

        // Act & Assert
        await Assert.That(candidate1.Equals(candidate2)).IsFalse();
    }

    [Test]
    public async Task Equals_WithDerivedDataDifferent_ShouldReturnFalse()
    {
        // Arrange
        var candidate1 = TestData.CreateSuccessfulParamsCandidate();

        var candidate2 = TestData.CreateSuccessfulParamsCandidate(
            derivedData: TestData.CreateDerivedData(methodName: "DifferentMethod"));

        // Act & Assert
        await Assert.That(candidate1.Equals(candidate2)).IsFalse();
    }

    [Test]
    public async Task Equals_WithMaxOverridesDifferent_ShouldReturnFalse()
    {
        // Arrange
        var candidate1 = TestData.CreateSuccessfulParamsCandidate();

        var candidate2 = TestData.CreateSuccessfulParamsCandidate(
            maxOverrides: 10);

        // Act & Assert
        await Assert.That(candidate1.Equals(candidate2)).IsFalse();
    }

    [Test]
    public async Task Equals_WithHasParamDifferent_ShouldReturnFalse()
    {
        // Arrange
        var candidate1 = TestData.CreateSuccessfulParamsCandidate();

        var candidate2 = TestData.CreateSuccessfulParamsCandidate(
            hasParams: false);

        // Act & Assert
        await Assert.That(candidate1.Equals(candidate2)).IsFalse();
    }
}
