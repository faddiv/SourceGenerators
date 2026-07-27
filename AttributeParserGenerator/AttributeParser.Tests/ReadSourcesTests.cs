namespace AttributeParser.Tests;

public class ReadSourcesTests
{
    private readonly AttributeDataParserHarness _harness = new();

    [Test]
    public async Task ReadsFromNamedArguments(CancellationToken token)
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

        var argument = await _harness.ParseAndGetSingleArgument(token);

        await Assert.That(argument.GetName()).IsEqualTo("StringValue");
        await Assert.That(argument.GetValue<string>()).IsEqualTo("Hello");
    }

    [Test]
    public async Task ReadsFromConstructor(CancellationToken token)
    {
        _harness.AddSource(
            """
            [AttributeUsage(AttributeTargets.Class)]
            public class ExampleAttribute(string stringValue) : Attribute
            {
                public string StringValue { get; } = stringValue;
            }
            """,
            token);
        _harness.AddAttributeOnClass(
            """
            [Example("Hello")]
            """,
            token);

        var argument = await _harness.ParseAndGetSingleArgument(token);

        await Assert.That(argument.GetName()).IsEqualTo("stringValue");
        await Assert.That(argument.GetValue<string>()).IsEqualTo("Hello");
    }

    [Test]
    public async Task WhenHasConstructorAndNamedArgument_NamedArgumentIsSecond(CancellationToken token)
    {
        _harness.AddSource(
            """
            [AttributeUsage(AttributeTargets.Class)]
            public class ExampleAttribute(string stringValue) : Attribute
            {
                public string? StringValue { get; set; } = stringValue;
            }
            """,
            token);
        _harness.AddAttributeOnClass(
            """
            [Example("First", StringValue = "Second")]
            """,
            token);

        var arguments = await _harness.ParseAndGetArguments(token);

        await Assert.That(arguments).Count().IsEqualTo(2);
        var first = arguments.First();
        var second = arguments.Last();
        await Assert.That(first.GetName()).IsEqualTo("stringValue");
        await Assert.That(first.GetValue<string>()).IsEqualTo("First");
        await Assert.That(second.GetName()).IsEqualTo("StringValue");
        await Assert.That(second.GetValue<string>()).IsEqualTo("Second");
    }
}
