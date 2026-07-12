using AttributeParserGenerator.Benchmarks.BaselineImplementation;
using AttributeParserGenerator.SampleCode.DecoratedClasses;
using AttributeParserGenerator.TestInfrastructure;
using BenchmarkDotNet.Attributes;
using Microsoft.CodeAnalysis;
using AttributeDataParser = AttributeParser.Core.AttributeDataParser;
using PreviousAttributeDataParser = AttributeParser.PreviousImplementation.AttributeDataParser;
using PreviousRunner = AttributeParser.PreviousImplementation.PreviousRunner;

namespace AttributeParserGenerator.Benchmarks;

[MemoryDiagnoser]
public class ParserBenchmark
{
    private readonly TestEnvironment _testEnvironment = new();
    private AttributeData _attributeData = null!;
    private AttributeDataParser _attributeParser = new();
    private PreviousAttributeDataParser _previousAttributeParser = new();

    [GlobalSetup]
    public void Setup()
    {
        _attributeData = _testEnvironment.GetClassAttributeData(nameof(ClassWithLotsOfArguments));
    }

    [Benchmark(Baseline = true)]
    public Dictionary<string, object?> ExtractValues()
    {
        return BaselineParser.Parse(_attributeData);
    }

    [Benchmark]
    public Dictionary<string, object?> Previous_ExtractMany()
    {
        return PreviousRunner.ExtractValues(_previousAttributeParser, _attributeData);
    }

    [Benchmark]
    public Dictionary<string, object?> Current_ExtractMany()
    {
        return TestRunner.ExtractValues(_attributeParser, _attributeData);
    }
}
