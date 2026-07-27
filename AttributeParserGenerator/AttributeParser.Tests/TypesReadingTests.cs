namespace AttributeParser.Tests;

public class TypesReadingTests
{
    private readonly AttributeDataParserHarness _harness = new();

    [Test]
    public async Task ParseReadsStringValue(CancellationToken token)
    {
        _harness.AddSource(
            """
            [AttributeUsage(AttributeTargets.Class)]
            public class ExampleAttribute : Attribute
            {
                public string? StringValue { get; set; }
            }
            """,
            token);
        _harness.AddAttributeOnClass(
            """
            [Example(StringValue = "Hello")]
            """,
            token);

        var argument = await _harness.ExtractSingleArgument(token);

        await Assert.That(argument.GetName()).IsEqualTo("StringValue");
        await Assert.That(argument.GetValue<string>()).IsEqualTo("Hello");
    }

    [Test]
    public async Task ParseReadsIntValue(CancellationToken token)
    {
        _harness.AddSource(
            """
            [AttributeUsage(AttributeTargets.Class)]
            public class ExampleAttribute : Attribute
            {
                public int IntValue { get; set; }
            }
            """,
            token);
        _harness.AddAttributeOnClass(
            """
            [Example(IntValue = 42)]
            """,
            token);

        var argument = await _harness.ExtractSingleArgument(token);

        await Assert.That(argument.GetName()).IsEqualTo("IntValue");
        await Assert.That(argument.GetValue<int>()).IsEqualTo(42);
    }
}
