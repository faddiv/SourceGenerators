using System.Threading.Tasks;
using AttributeParser.Core;
using AttributeParserGenerator.SampleCode.DecoratedClasses;
using AttributeParserGenerator.TestInfrastructure;

namespace AttributeParserGenerator.Tests.Tests;

[ClassDataSource<TestEnvironment>(Shared = SharedType.PerTestSession)]
public class AttributeDataParserOptionalReadTests(TestEnvironment testEnvironment)
{
    private readonly TestEnvironment _testEnvironment = testEnvironment;

    [Test]
    public async Task OptionalParametersNotProvided_UsesDefaultValues()
    {
        var attributeData = _testEnvironment.GetClassAttributeData(nameof(ClassWithOptionalArguments));
        var attributeParser = new AttributeDataParser();

        var results = AttributeParsers.ParseInputWithOptionalData(attributeData, attributeParser);

        await Assert.That(results).IsNotNull();
        await Assert.That(results.Value).IsEqualTo("Default Value");
        await Assert.That(results.IntValue).IsEqualTo(42);
    }

    [Test]
    public async Task OptionalParametersProvided_UsesProvidedValues()
    {
        var attributeData = _testEnvironment.GetClassAttributeData(nameof(ClassWithProvidedOptionalArguments));
        var attributeParser = new AttributeDataParser();

        var results = AttributeParsers.ParseInputWithOptionalData(attributeData, attributeParser);

        await Assert.That(results).IsNotNull();
        await Assert.That(results.Value).IsEqualTo("Hello World");
        await Assert.That(results.IntValue).IsEqualTo(42);
    }

    [Test]
    public async Task NamedOptionalParametersProvided_UsesProvidedValues()
    {
        var attributeData = _testEnvironment.GetClassAttributeData(nameof(ClassWithNamedOptionalArguments));
        var attributeParser = new AttributeDataParser();

        var results = AttributeParsers.ParseInputWithOptionalData(attributeData, attributeParser);

        await Assert.That(results).IsNotNull();
        await Assert.That(results.Value).IsEqualTo("Hello World");
        await Assert.That(results.IntValue).IsEqualTo(42);
    }
}
