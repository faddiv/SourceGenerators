using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using AttributeParser.Core;
using AttributeParserGenerator.SampleCode.DecoratedClasses;
using AttributeParserGenerator.SampleCode.TestAttributes;
using AttributeParserGenerator.TestInfrastructure;
using Microsoft.CodeAnalysis;
using EnumValue = AttributeParserGenerator.TestInfrastructure.EnumValue;

namespace AttributeParserGenerator.Tests.Tests;

[ClassDataSource<TestEnvironment>(Shared = SharedType.PerTestSession)]
public class AttributeDataParserNonGenericGetValueTests(TestEnvironment testEnvironment)
{
    private readonly TestEnvironment _testEnvironment = testEnvironment;

    private Dictionary<string, object?> ProcessAttributeDataForTesting()
    {
        var attributeData = _testEnvironment.GetClassAttributeData(nameof(ClassWithProvidedDifferentTypes));
        var attributeParser = new AttributeDataParser();

        var result = TestRunner.ExtractValues(attributeParser, attributeData);

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

        await Assert.That(result["stringValue"])
            .IsEqualTo("Test Value");
    }

    [Test]
    [DependsOn(nameof(AttributeDataParserWithDifferentTypesSucceed))]
    public async Task AttributeDataParserReadsInt()
    {
        var result = ProcessAttributeDataForTesting();

        await Assert.That(result["intValue"])
            .IsEqualTo(42);
    }

    [Test]
    [DependsOn(nameof(AttributeDataParserWithDifferentTypesSucceed))]
    public async Task AttributeDataParserReadsBool()
    {
        var result = ProcessAttributeDataForTesting();

        await Assert.That(result["boolValue"])
            .IsTypeOf<bool>()
            .And
            .IsTrue();
    }

    [Test]
    [DependsOn(nameof(AttributeDataParserWithDifferentTypesSucceed))]
    public async Task AttributeDataParserReadsEnum()
    {
        var result = ProcessAttributeDataForTesting();

        await Assert.That(result["enumValue"])
            .IsTypeOf<int>()
            .And
            .IsEqualTo((int)EnumValue.Value2);
    }


    [Test]
    [DependsOn(nameof(AttributeDataParserWithDifferentTypesSucceed))]
    public async Task AttributeDataParserReadsDouble()
    {
        var result = ProcessAttributeDataForTesting();

        await Assert.That(result["doubleValue"])
            .IsTypeOf<double>()
            .And
            .IsEqualTo(3.14);
    }


    [Test]
    [DependsOn(nameof(AttributeDataParserWithDifferentTypesSucceed))]
    public async Task AttributeDataParserReadsType()
    {
        var result = ProcessAttributeDataForTesting();

        await Assert.That(result["typeValue"])
            .IsTypeOf<INamedTypeSymbol>()
            .And
            .HasProperty(x => x.Name)
            .IsEqualTo(nameof(TargetClass));
    }

    [Test]
    [DependsOn(nameof(AttributeDataParserWithDifferentTypesSucceed))]
    public async Task AttributeDataParserReadsStringArray()
    {
        var result = ProcessAttributeDataForTesting();

        await Assert.That(result["stringArray"])
            .IsTypeOf<ImmutableArray<string>>()
            .And
            .IsEquivalentTo(["Test String 1", "Test String 2"]);
    }

    [Test]
    [DependsOn(nameof(AttributeDataParserWithDifferentTypesSucceed))]
    public async Task AttributeDataParserReadsIntArray()
    {
        var result = ProcessAttributeDataForTesting();

        await Assert.That(result["intArray"])
            .IsTypeOf<ImmutableArray<int>>()
            .And
            .IsEquivalentTo([1, 2, 3]);
    }

    [Test]
    [DependsOn(nameof(AttributeDataParserWithDifferentTypesSucceed))]
    public async Task AttributeDataParserReadsBoolArray()
    {
        var result = ProcessAttributeDataForTesting();

        await Assert.That(result["boolArray"])
            .IsTypeOf<ImmutableArray<bool>>()
            .And
            .IsEquivalentTo([true, false, true]);
    }

    [Test]
    [DependsOn(nameof(AttributeDataParserWithDifferentTypesSucceed))]
    public async Task AttributeDataParserReadsDoubleArray()
    {
        var result = ProcessAttributeDataForTesting();

        await Assert.That(result["doubleArray"])
            .IsTypeOf<ImmutableArray<double>>()
            .And
            .IsEquivalentTo([1.1, 2.2, 3.3]);
    }

    [Test]
    [DependsOn(nameof(AttributeDataParserWithDifferentTypesSucceed))]
    public async Task AttributeDataParserReadsEnumArray()
    {
        var result = ProcessAttributeDataForTesting();

        await Assert.That(result["enumArray"])
            .IsTypeOf<ImmutableArray<int>>()
            .And
            .IsEquivalentTo([(int)EnumValue.Value1, (int)EnumValue.Value3]);
    }

    [Test]
    [DependsOn(nameof(AttributeDataParserWithDifferentTypesSucceed))]
    public async Task AttributeDataParserReadsTypeArray()
    {
        var result = ProcessAttributeDataForTesting();

        var typeArray = await Assert.That(result["typeArray"])
            .IsTypeOf<ImmutableArray<INamedTypeSymbol>>();

        await Assert.That(typeArray.Select(e => e.Name))
            .IsEquivalentTo([
                nameof(TargetClass),
                nameof(AnotherTargetClass)
            ]);
    }
}
