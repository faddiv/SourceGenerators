using System.Threading.Tasks;
using AttributeParser.Core;
using AttributeParserGenerator.SampleCode.DecoratedClasses;
using AttributeParserGenerator.TestInfrastructure;
using AttributeParserGenerator.Tests.TestInfrastructure;

namespace AttributeParserGenerator.Tests.Tests;

[ClassDataSource<TestEnvironment>(Shared = SharedType.PerTestSession)]
public class AttributeDataParserBasicReadTests(TestEnvironment testEnvironment)
{
    private readonly TestEnvironment _testEnvironment = testEnvironment;

    [Test]
    public async Task CompiledCodeHasNoDiagnostics()
    {
        var compilation = _testEnvironment.Compilation;

        await Assert.That(compilation)
            .HasNoDiagnostics();
    }

    [Test]
    public async Task NamedArgumentsCanBeExtractedFromAttributes()
    {
        var attributeData = _testEnvironment.GetClassAttributeData(nameof(ClassWithNamedArguments));
        var attributeParser = new AttributeDataParser();

        var results = AttributeParsers.ParseFirstInputData(attributeData, attributeParser);

        await Assert.That(results).IsNotNull();
        await Assert.That(results.Value).IsEqualTo("Hello World");
        await Assert.That(results.IntValue).IsEqualTo(42);
        await Assert.That(results.BoolValue).IsTrue();
    }

    [Test]
    public async Task ConstructorArgumentsCanBeExtractedFromAttributes()
    {
        var attributeData = _testEnvironment.GetClassAttributeData(nameof(ClassWithConstructorArguments));
        var attributeParser = new AttributeDataParser();

        var results = AttributeParsers.ParseFirstInputData(attributeData, attributeParser);

        await Assert.That(results).IsNotNull();
        await Assert.That(results.Value).IsEqualTo("Hello World");
        await Assert.That(results.IntValue).IsEqualTo(42);
        await Assert.That(results.BoolValue).IsTrue();
    }

    [Test]
    public async Task MixedConstructorAndNamedArgumentsCanBeExtractedFromAttributes()
    {
        var attributeData = _testEnvironment.GetClassAttributeData(nameof(ClassWithLotsOfArguments));
        var attributeParser = new AttributeDataParser();

        var results = AttributeParsers.ParseAttributeWithLotsOfArgumentsData(attributeData, attributeParser);

        await Assert.That(results).IsNotNull();
        await Assert.That(results.Value1).IsEqualTo("Arg 1 overridden");
        await Assert.That(results.Value10).IsEqualTo("Arg 10");
        await Assert.That(results.IntValue1).IsEqualTo(1);
        await Assert.That(results.IntValue10).IsEqualTo(10);
    }
}
