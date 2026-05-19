using System.Threading.Tasks;
using AttributeParserGenerator.Core;
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

        var results = TestRunner.ExtractValues(attributeParser, attributeData);

        await Assert.That(results)
            .Count().IsEqualTo(3);

        await Assert.That(results)
            .ContainsKeyWithValue("value", "Hello World")
            .And.ContainsKeyWithValue("intValue", 42)
            .And.ContainsKeyWithValue("boolValue", true);
    }

    [Test]
    public async Task ConstructorArgumentsCanBeExtractedFromAttributes()
    {
        var attributeData = _testEnvironment.GetClassAttributeData(nameof(ClassWithConstructorArguments));
        var attributeParser = new AttributeDataParser();

        var results = TestRunner.ExtractValues(attributeParser, attributeData);

        await Assert.That(results)
            .Count().IsEqualTo(3);

        await Assert.That(results)
            .ContainsKeyWithValue("value", "Hello World")
            .And.ContainsKeyWithValue("intValue", 42)
            .And.ContainsKeyWithValue("boolValue", true);
    }

    [Test]
    public async Task MixedConstructorAndNamedArgumentsCanBeExtractedFromAttributes()
    {
        var attributeData = _testEnvironment.GetClassAttributeData(nameof(ClassWithLotsOfArguments));
        var attributeParser = new AttributeDataParser();

        var results = TestRunner.ExtractValues(attributeParser, attributeData);

        await Assert.That(results)
            .Count().IsEqualTo(20);

        await Assert.That(results)
            .ContainsKeyWithValue("value1", "Arg 1")
            .And.ContainsKeyWithValue("value10", "Arg 10")
            .And.ContainsKeyWithValue("intValue1", 1)
            .And.ContainsKeyWithValue("intValue10", 10);
    }
}
