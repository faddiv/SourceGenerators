using AttributeParser.Tests.TestInfrstructure;

namespace AttributeParser.Tests;

public enum ExampleEnum
{
    Value1,
    Value2,
    Value3,
}

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

        var argument = await _harness.ParseAndGetSingleArgument(token);

        await Assert.That(argument)
            .HasName("StringValue")
            .WithValue("Hello");
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

        var argument = await _harness.ParseAndGetSingleArgument(token);

        await Assert.That(argument)
            .HasName("IntValue")
            .WithValue(42);
    }

    [Test]
    public async Task ParseReadsBoolValue(CancellationToken token)
    {
        _harness.AddSource(
            """
            [AttributeUsage(AttributeTargets.Class)]
            public class ExampleAttribute : Attribute
            {
                public bool BoolValue { get; set; }
            }
            """,
            token);
        _harness.AddAttributeOnClass(
            """
            [Example(BoolValue = true)]
            """,
            token);

        var argument = await _harness.ParseAndGetSingleArgument(token);

        await Assert.That(argument)
            .HasName("BoolValue")
            .WithValue(true);
    }

    [Test]
    public async Task ParseReadsEnumValue(CancellationToken token)
    {
        _harness.AddSource(
            """
            public enum ExampleEnum
            {
                Value1,
                Value2,
                Value3,
            }

            [AttributeUsage(AttributeTargets.Class)]
            public class ExampleAttribute : Attribute
            {
                public ExampleEnum EnumValue { get; set; }
            }
            """,
            token);
        _harness.AddAttributeOnClass(
            """
            [Example(EnumValue = ExampleEnum.Value2)]
            """,
            token);

        var argument = await _harness.ParseAndGetSingleArgument(token);

        await Assert.That(argument)
            .HasName("EnumValue")
            .WithValue(ExampleEnum.Value2);
    }

    [Test]
    public async Task ParseReadsDoubleValue(CancellationToken token)
    {
        _harness.AddSource(
            """
            [AttributeUsage(AttributeTargets.Class)]
            public class ExampleAttribute : Attribute
            {
                public double DoubleValue { get; set; }
            }
            """,
            token);
        _harness.AddAttributeOnClass(
            """
            [Example(DoubleValue = 3.14)]
            """,
            token);

        var argument = await _harness.ParseAndGetSingleArgument(token);

        await Assert.That(argument)
            .HasName("DoubleValue")
            .WithValue(3.14);
    }

    [Test]
    public async Task ParseReadsTypeValue(CancellationToken token)
    {
        _harness.AddSource(
            """
            public class TargetClass;

            [AttributeUsage(AttributeTargets.Class)]
            public class ExampleAttribute : Attribute
            {
                public Type? TypeValue { get; set; }
            }
            """,
            token);
        _harness.AddAttributeOnClass(
            """
            [Example(TypeValue = typeof(TargetClass))]
            """,
            token);

        var argument = await _harness.ParseAndGetSingleArgument(token);

        await Assert.That(argument)
            .HasName("TypeValue")
            .WithSymbol("TargetClass");
    }

    [Test]
    public async Task ParseReadsStringArray(CancellationToken token)
    {
        _harness.AddSource(
            """
            [AttributeUsage(AttributeTargets.Class)]
            public class ExampleAttribute : Attribute
            {
                public string[] StringArray { get; set; } = [];
            }
            """,
            token);
        _harness.AddAttributeOnClass(
            """
            [Example(StringArray = ["Hello", "World"])]
            """,
            token);

        var argument = await _harness.ParseAndGetSingleArgument(token);

        await Assert.That(argument)
            .HasName("StringArray")
            .WithValues(["Hello", "World"]);
    }

    [Test]
    public async Task ParseReadsIntArray(CancellationToken token)
    {
        _harness.AddSource(
            """
            [AttributeUsage(AttributeTargets.Class)]
            public class ExampleAttribute : Attribute
            {
                public int[] IntArray { get; set; } = [];
            }
            """,
            token);
        _harness.AddAttributeOnClass(
            """
            [Example(IntArray = [1, 2, 3])]
            """,
            token);

        var argument = await _harness.ParseAndGetSingleArgument(token);

        await Assert.That(argument)
            .HasName("IntArray")
            .WithValues([1, 2, 3]);
    }

    [Test]
    public async Task ParseReadsBoolArray(CancellationToken token)
    {
        _harness.AddSource(
            """
            [AttributeUsage(AttributeTargets.Class)]
            public class ExampleAttribute : Attribute
            {
                public bool[] BoolArray { get; set; } = [];
            }
            """,
            token);
        _harness.AddAttributeOnClass(
            """
            [Example(BoolArray = [true, false, true])]
            """,
            token);

        var argument = await _harness.ParseAndGetSingleArgument(token);

        await Assert.That(argument)
            .HasName("BoolArray")
            .WithValues([true, false, true]);
    }

    [Test]
    public async Task ParseReadsEnumArray(CancellationToken token)
    {
        _harness.AddSource(
            """
            public enum ExampleEnum
            {
                Value1,
                Value2,
                Value3,
            }

            [AttributeUsage(AttributeTargets.Class)]
            public class ExampleAttribute : Attribute
            {
                public ExampleEnum[] EnumArray { get; set; } = [];
            }
            """,
            token);
        _harness.AddAttributeOnClass(
            """
            [Example(EnumArray = [ExampleEnum.Value1, ExampleEnum.Value3])]
            """,
            token);

        var argument = await _harness.ParseAndGetSingleArgument(token);

        await Assert.That(argument)
            .HasName("EnumArray")
            .WithValues([ExampleEnum.Value1, ExampleEnum.Value3]);
    }

    [Test]
    public async Task ParseReadsDoubleArray(CancellationToken token)
    {
        _harness.AddSource(
            """
            [AttributeUsage(AttributeTargets.Class)]
            public class ExampleAttribute : Attribute
            {
                public double[] DoubleArray { get; set; } = [];
            }
            """,
            token);
        _harness.AddAttributeOnClass(
            """
            [Example(DoubleArray = [1.1, 2.2, 3.3])]
            """,
            token);

        var argument = await _harness.ParseAndGetSingleArgument(token);

        await Assert.That(argument)
            .HasName("DoubleArray")
            .WithValues([1.1, 2.2, 3.3]);
    }

    [Test]
    public async Task ParseReadsTypeArray(CancellationToken token)
    {
        _harness.AddSource(
            """
            public class TargetClass;
            public class AnotherTargetClass;

            [AttributeUsage(AttributeTargets.Class)]
            public class ExampleAttribute : Attribute
            {
                public Type[] TypeArray { get; set; } = [];
            }
            """,
            token);
        _harness.AddAttributeOnClass(
            """
            [Example(TypeArray = [typeof(TargetClass), typeof(AnotherTargetClass)])]
            """,
            token);

        var argument = await _harness.ParseAndGetSingleArgument(token);

        await Assert.That(argument)
            .HasName("TypeArray")
            .WithSymbols(["TargetClass", "AnotherTargetClass"]);
    }
}
