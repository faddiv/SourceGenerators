using AttributeParserGenerator.Benchmarks;
using BenchmarkDotNet.Running;

Console.WriteLine("# Benchmarks for AttributeParser");

BenchmarkRunner.Run<ParserBenchmark>();
