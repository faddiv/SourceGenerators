using System.Collections.Generic;
using System.Threading.Tasks;
using AttributeParserGenerator.Sample;
using AttributeParserGenerator.Tests.Core;

namespace AttributeParserGenerator.Tests.Tests;

[ClassDataSource<TestEnvironment>(Shared = SharedType.PerTestSession)]
public class CompilationTests(TestEnvironment testEnvironment)
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
        var attributeParser = new AttributeParser();
        var results = new Dictionary<string, object?>();
        var parser = attributeParser.Parse(attributeData);
        foreach (var result in parser)
        {
            results[result.Name] = parser.GetValue(result);
        }

        await Assert.That(results)
            .ContainsKeyWithValue("Value", "Hello World")
            .And.ContainsKeyWithValue("IntValue", 42)
            .And.ContainsKeyWithValue("BoolValue", true);
    }
}
