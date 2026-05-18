using AttributeParserGenerator.Benchmarks;
using BenchmarkDotNet.Running;

Console.WriteLine("# Benchmarks for AttributeParserGenerator");

BenchmarkRunner.Run<ParserBenchmark>();
