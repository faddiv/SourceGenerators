namespace AttributeParser.SourceGenerator.Tests;

public class GeneratorTests
{
    private readonly AttributeParserGeneratorCompilationHarness _harness = new();

    [Test]
    public Task GeneratesStringParser(CancellationToken token)
    {
        _harness.AddSource(
            """
            public class ParsedData
            {
                public string? StringValue { get; set; }
            }
            """,
            token);

        _harness.AddSource(
            """
            using AttributeParser;
            using AttributeParser.Core;
            using Microsoft.CodeAnalysis;

            public static partial class AttributeParsers
            {
                [AttributeParser]
                public static partial ParsedData ParseData(
                    AttributeData attributeData,
                    AttributeDataParser parser);
            }
            """,
            token);
        var results = _harness.RunSourceGenerator(token)
            .GetRunResult();

        return Verify(results);
    }

    [Test]
    public Task GeneratesIntParser(CancellationToken token)
    {
        _harness.AddSource(
            """
            public class ParsedData
            {
                public int IntValue { get; set; }
            }
            """,
            token);

        _harness.AddSource(
            """
            using AttributeParser;
            using AttributeParser.Core;
            using Microsoft.CodeAnalysis;

            public static partial class AttributeParsers
            {
                [AttributeParser]
                public static partial ParsedData ParseData(
                    AttributeData attributeData,
                    AttributeDataParser parser);
            }
            """,
            token);
        var results = _harness.RunSourceGenerator(token)
            .GetRunResult();

        return Verify(results);
    }

    [Test]
    public Task GeneratesBoolParser(CancellationToken token)
    {
        _harness.AddSource(
            """
            public class ParsedData
            {
                public bool BoolValue { get; set; }
            }
            """,
            token);

        _harness.AddSource(
            """
            using AttributeParser;
            using AttributeParser.Core;
            using Microsoft.CodeAnalysis;

            public static partial class AttributeParsers
            {
                [AttributeParser]
                public static partial ParsedData ParseData(
                    AttributeData attributeData,
                    AttributeDataParser parser);
            }
            """,
            token);
        var results = _harness.RunSourceGenerator(token)
            .GetRunResult();

        return Verify(results);
    }

    [Test]
    public Task GeneratesEnumParser(CancellationToken token)
    {
        _harness.AddSource(
            """
            public enum EnumValue
            {
                Value1,
                Value2,
                Value3,
            }

            public class ParsedData
            {
                public EnumValue EnumValue { get; set; }
            }
            """,
            token);

        _harness.AddSource(
            """
            using AttributeParser;
            using AttributeParser.Core;
            using Microsoft.CodeAnalysis;

            public static partial class AttributeParsers
            {
                [AttributeParser]
                public static partial ParsedData ParseData(
                    AttributeData attributeData,
                    AttributeDataParser parser);
            }
            """,
            token);
        var results = _harness.RunSourceGenerator(token)
            .GetRunResult();

        return Verify(results);
    }

    [Test]
    public Task GeneratesDoubleParser(CancellationToken token)
    {
        _harness.AddSource(
            """
            public class ParsedData
            {
                public double DoubleValue { get; set; }
            }
            """,
            token);

        _harness.AddSource(
            """
            using AttributeParser;
            using AttributeParser.Core;
            using Microsoft.CodeAnalysis;

            public static partial class AttributeParsers
            {
                [AttributeParser]
                public static partial ParsedData ParseData(
                    AttributeData attributeData,
                    AttributeDataParser parser);
            }
            """,
            token);
        var results = _harness.RunSourceGenerator(token)
            .GetRunResult();

        return Verify(results);
    }

    [Test]
    public Task GeneratesTypeParser(CancellationToken token)
    {
        _harness.AddSource(
            """
            using Microsoft.CodeAnalysis;

            public class ParsedData
            {
                public INamedTypeSymbol? TypeValue { get; set; }
            }
            """,
            token);

        _harness.AddSource(
            """
            using AttributeParser;
            using AttributeParser.Core;
            using Microsoft.CodeAnalysis;

            public static partial class AttributeParsers
            {
                [AttributeParser]
                public static partial ParsedData ParseData(
                    AttributeData attributeData,
                    AttributeDataParser parser);
            }
            """,
            token);
        var results = _harness.RunSourceGenerator(token)
            .GetRunResult();

        return Verify(results);
    }

    [Test]
    public Task GeneratesStringArrayParser(CancellationToken token)
    {
        _harness.AddSource(
            """
            using System.Collections.Immutable;

            public class ParsedData
            {
                public ImmutableArray<string> StringArray { get; set; }
            }
            """,
            token);

        _harness.AddSource(
            """
            using AttributeParser;
            using AttributeParser.Core;
            using Microsoft.CodeAnalysis;

            public static partial class AttributeParsers
            {
                [AttributeParser]
                public static partial ParsedData ParseData(
                    AttributeData attributeData,
                    AttributeDataParser parser);
            }
            """,
            token);
        var results = _harness.RunSourceGenerator(token)
            .GetRunResult();

        return Verify(results);
    }

    [Test]
    public Task GeneratesIntArrayParser(CancellationToken token)
    {
        _harness.AddSource(
            """
            using System.Collections.Immutable;

            public class ParsedData
            {
                public ImmutableArray<int> IntArray { get; set; }
            }
            """,
            token);

        _harness.AddSource(
            """
            using AttributeParser;
            using AttributeParser.Core;
            using Microsoft.CodeAnalysis;

            public static partial class AttributeParsers
            {
                [AttributeParser]
                public static partial ParsedData ParseData(
                    AttributeData attributeData,
                    AttributeDataParser parser);
            }
            """,
            token);
        var results = _harness.RunSourceGenerator(token)
            .GetRunResult();

        return Verify(results);
    }

    [Test]
    public Task GeneratesBoolArrayParser(CancellationToken token)
    {
        _harness.AddSource(
            """
            using System.Collections.Immutable;

            public class ParsedData
            {
                public ImmutableArray<bool> BoolArray { get; set; }
            }
            """,
            token);

        _harness.AddSource(
            """
            using AttributeParser;
            using AttributeParser.Core;
            using Microsoft.CodeAnalysis;

            public static partial class AttributeParsers
            {
                [AttributeParser]
                public static partial ParsedData ParseData(
                    AttributeData attributeData,
                    AttributeDataParser parser);
            }
            """,
            token);
        var results = _harness.RunSourceGenerator(token)
            .GetRunResult();

        return Verify(results);
    }

    [Test]
    public Task GeneratesEnumArrayParser(CancellationToken token)
    {
        _harness.AddSource(
            """
            using System.Collections.Immutable;

            public enum EnumValue
            {
                Value1,
                Value2,
                Value3,
            }

            public class ParsedData
            {
                public ImmutableArray<EnumValue> EnumArray { get; set; }
            }
            """,
            token);

        _harness.AddSource(
            """
            using AttributeParser;
            using AttributeParser.Core;
            using Microsoft.CodeAnalysis;

            public static partial class AttributeParsers
            {
                [AttributeParser]
                public static partial ParsedData ParseData(
                    AttributeData attributeData,
                    AttributeDataParser parser);
            }
            """,
            token);
        var results = _harness.RunSourceGenerator(token)
            .GetRunResult();

        return Verify(results);
    }

    [Test]
    public Task GeneratesDoubleArrayParser(CancellationToken token)
    {
        _harness.AddSource(
            """
            using System.Collections.Immutable;

            public class ParsedData
            {
                public ImmutableArray<double> DoubleArray { get; set; }
            }
            """,
            token);

        _harness.AddSource(
            """
            using AttributeParser;
            using AttributeParser.Core;
            using Microsoft.CodeAnalysis;

            public static partial class AttributeParsers
            {
                [AttributeParser]
                public static partial ParsedData ParseData(
                    AttributeData attributeData,
                    AttributeDataParser parser);
            }
            """,
            token);
        var results = _harness.RunSourceGenerator(token)
            .GetRunResult();

        return Verify(results);
    }

    [Test]
    public Task GeneratesTypeArrayParser(CancellationToken token)
    {
        _harness.AddSource(
            """
            using System.Collections.Immutable;
            using Microsoft.CodeAnalysis;

            public class ParsedData
            {
                public ImmutableArray<INamedTypeSymbol> TypeArray { get; set; }
            }
            """,
            token);

        _harness.AddSource(
            """
            using AttributeParser;
            using AttributeParser.Core;
            using Microsoft.CodeAnalysis;

            public static partial class AttributeParsers
            {
                [AttributeParser]
                public static partial ParsedData ParseData(
                    AttributeData attributeData,
                    AttributeDataParser parser);
            }
            """,
            token);
        var results = _harness.RunSourceGenerator(token)
            .GetRunResult();

        return Verify(results);
    }
}
