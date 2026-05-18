using AttributeParserGenerator.Benchmarks.BaselineImplementation;
using AttributeParserGenerator.Core;
using AttributeParserGenerator.SampleCode.DecoratedClasses;
using AttributeParserGenerator.TestInfrastructure;
using BenchmarkDotNet.Attributes;
using Microsoft.CodeAnalysis;

namespace AttributeParserGenerator.Benchmarks;

[MemoryDiagnoser]
public class ParserBenchmark
{
    private readonly TestEnvironment _testEnvironment = new();
    private AttributeData _attributeData = null!;
    private AttributeDataParser _attributeParser = null!;

    [GlobalSetup]
    public void Setup()
    {
        _attributeData = _testEnvironment.GetClassAttributeData(nameof(ClassWithLotsOfArguments));
        _attributeParser = new AttributeDataParser();
    }

    [Benchmark(Baseline = true)]
    public Dictionary<string, object?> ExtractValues()
    {
        return BaselineParser.Parse(_attributeData);
    }

    [Benchmark]
    public Dictionary<string, object?> ExtractLotsOfValues()
    {
        return TestHelpers.ExtractValues(_attributeParser, _attributeData);
    }
}
