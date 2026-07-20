using System.Linq;
using System.Threading.Tasks;
using AttributeParser.Core;
using AttributeParserGenerator.SampleCode.DecoratedClasses;
using AttributeParserGenerator.SampleCode.TestAttributes;
using AttributeParserGenerator.TestInfrastructure;
using EnumValue = AttributeParserGenerator.TestInfrastructure.EnumValue;

namespace AttributeParserGenerator.Tests.Tests;

[ClassDataSource<TestEnvironment>(Shared = SharedType.PerTestSession)]
public class AttributeDataParserGenericGetValueTests(TestEnvironment testEnvironment)
{
    private readonly TestEnvironment _testEnvironment = testEnvironment;

    private InputWithDifferentTypesData ProcessAttributeDataForTesting()
    {
        var attributeData = _testEnvironment.GetClassAttributeData(nameof(ClassWithProvidedDifferentTypes));
        var attributeParser = new AttributeDataParser();

        var result = AttributeParsers.ParseInputWithDifferentTypes(attributeData, attributeParser);

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

    [Test]
    [DependsOn(nameof(AttributeDataParserWithDifferentTypesSucceed))]
    public async Task AttributeDataParserReadsStringArray()
    {
        var result = ProcessAttributeDataForTesting();

        await Assert.That(result.StringArray)
            .IsNotNull()
            .And
            .IsEquivalentTo(["Test String 1", "Test String 2"]);
    }

    [Test]
    [DependsOn(nameof(AttributeDataParserWithDifferentTypesSucceed))]
    public async Task AttributeDataParserReadsIntArray()
    {
        var result = ProcessAttributeDataForTesting();

        await Assert.That(result.IntArray)
            .IsNotNull()
            .And
            .IsEquivalentTo([1, 2, 3]);
    }

    [Test]
    [DependsOn(nameof(AttributeDataParserWithDifferentTypesSucceed))]
    public async Task AttributeDataParserReadsBoolArray()
    {
        var result = ProcessAttributeDataForTesting();

        await Assert.That(result.BoolArray)
            .IsNotNull()
            .And
            .IsEquivalentTo([true, false, true]);
    }

    [Test]
    [DependsOn(nameof(AttributeDataParserWithDifferentTypesSucceed))]
    public async Task AttributeDataParserReadsDoubleArray()
    {
        var result = ProcessAttributeDataForTesting();

        await Assert.That(result.DoubleArray)
            .IsNotNull()
            .And
            .IsEquivalentTo([1.1, 2.2, 3.3]);
    }

    [Test]
    [DependsOn(nameof(AttributeDataParserWithDifferentTypesSucceed))]
    public async Task AttributeDataParserReadsEnumArray()
    {
        var result = ProcessAttributeDataForTesting();

        await Assert.That(result.EnumArray)
            .IsNotNull()
            .And
            .IsEquivalentTo([EnumValue.Value1, EnumValue.Value3]);
    }

    [Test]
    [DependsOn(nameof(AttributeDataParserWithDifferentTypesSucceed))]
    public async Task AttributeDataParserReadsTypeArray()
    {
        var result = ProcessAttributeDataForTesting();

        var typeArray = await Assert.That(result.TypeArray)
            .IsNotNull();

        await Assert.That(typeArray?.Select(e => e.Name))
            .IsEquivalentTo([
                nameof(TargetClass),
                nameof(AnotherTargetClass)
            ]);
    }
}
