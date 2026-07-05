using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AttributeParserGenerator.Core;
using AttributeParserGenerator.SampleCode.DecoratedClasses;
using AttributeParserGenerator.SampleCode.TestAttributes;
using AttributeParserGenerator.TestInfrastructure;
using EnumValue = AttributeParserGenerator.TestInfrastructure.EnumValue;

namespace AttributeParserGenerator.Tests.Tests;

[ClassDataSource<TestEnvironment>(Shared = SharedType.PerTestSession)]
[SuppressMessage("Usage", "TUnit0018:Test methods should not assign instance data")]
public class AttributeDataParserDifferentTypes(TestEnvironment testEnvironment)
{
    private readonly TestEnvironment _testEnvironment = testEnvironment;

    private InputWithDifferentTypes ProcessAttributeDataForTesting()
    {
        var attributeData = _testEnvironment.GetClassAttributeData(nameof(ClassWithProvidedDifferentTypes));
        var attributeParser = new AttributeDataParser();

        var result = TestRunner.DeserializeInputWithDifferentTypes(attributeParser, attributeData);
        return result;
    }

    [Test]
    public async Task AttributeDataParserWithDifferentTypesSucceed()
    {
        var result = ProcessAttributeDataForTesting();

        await Assert.That(result).IsNotNull();
    }

    [Test]
    [DependsOn(nameof(AttributeDataParserWithDifferentTypesSucceed))]
    public async Task AttributeDataParserReadsString()
    {
        var result = ProcessAttributeDataForTesting();
        await Assert.That(result.StringValue)
            .IsEqualTo("Test Value");
    }

    [Test]
    [DependsOn(nameof(AttributeDataParserWithDifferentTypesSucceed))]
    public async Task AttributeDataParserReadsInt()
    {
        var result = ProcessAttributeDataForTesting();
        await Assert.That(result.IntValue)
            .IsEqualTo(42);
    }

    [Test]
    [DependsOn(nameof(AttributeDataParserWithDifferentTypesSucceed))]
    public async Task AttributeDataParserReadsBool()
    {
        var result = ProcessAttributeDataForTesting();
        await Assert.That(result.BoolValue)
            .IsTrue();
    }

    [Test]
    [DependsOn(nameof(AttributeDataParserWithDifferentTypesSucceed))]
    public async Task AttributeDataParserReadsEnum()
    {
        var result = ProcessAttributeDataForTesting();
        await Assert.That(result.EnumValue)
            .IsEqualTo(EnumValue.Value2);
    }

    [Test]
    [DependsOn(nameof(AttributeDataParserWithDifferentTypesSucceed))]
    public async Task AttributeDataParserReadsDouble()
    {
        var result = ProcessAttributeDataForTesting();
        await Assert.That(result.DoubleValue)
            .IsEqualTo(3.14);
    }

    [Test]
    [DependsOn(nameof(AttributeDataParserWithDifferentTypesSucceed))]
    public async Task AttributeDataParserReadsType()
    {
        var result = ProcessAttributeDataForTesting();
        await Assert.That(result.TypeValue)
            .IsNotNull()
            .And
            .HasProperty(x => x.Name)
            .IsEqualTo(nameof(TargetClass));
    }
}
