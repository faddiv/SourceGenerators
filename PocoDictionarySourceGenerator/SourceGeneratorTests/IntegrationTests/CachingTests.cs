using Foxy.PocoDictionary.SourceGenerator;
using Foxy.PocoDictionary.SourceGenerator.Data;
using Xunit;
using SourceGeneratorTests.TestInfrastructure;
using static SourceGeneratorTests.TestInfrastructure.CachingTestHelpers;
using Microsoft.CodeAnalysis;
using Test.Infrastructure;

namespace SourceGeneratorTests.IntegrationTests
{
    public class CachingTests
    {
        // A collection of all the tracking names. I'll show how to simplify this later
        private static readonly string[] _allTrackingNames = [TrackingNames.CollectData, TrackingNames.NotNullFilter];
        private readonly CompilerRunner _compilerRunner;
        private readonly TestEnvironment _testEnvironment;

        public CachingTests(TestEnvironment testEnvironment)
        {
            _testEnvironment = testEnvironment;
            _compilerRunner = _testEnvironment.Compiler;
        }

        [Fact]
        public void Caches_When_Nothing_Changes()
        {
            var runner = new SourceGeneratorRunner<PocoDictionaryIncrementalGenerator>();
            var input = _testEnvironment.GetCachingSource();

            var compilation = _compilerRunner.CompileSources([input], TestContext.Current.CancellationToken);

            var result1 = runner.RunSourceGenerator(compilation, TestContext.Current.CancellationToken);

            var compilation2 = compilation.Clone();

            var result2 = runner.RunSourceGenerator(compilation2, TestContext.Current.CancellationToken);

            AssertRunsEqual(result1, result2, _allTrackingNames);
            AssertAllOutputs(result2, IncrementalStepRunReason.Cached);
            Assert.Empty(result1.Diagnostics);
        }

        [Fact]
        public void Caches_When_IndexerAdded()
        {
            var runner = new SourceGeneratorRunner<PocoDictionaryIncrementalGenerator>();
            var inputs = _testEnvironment.GetCachingSources();

            var compilation = _compilerRunner.CompileSources([inputs[0]], TestContext.Current.CancellationToken);

            runner.RunSourceGenerator(compilation, TestContext.Current.CancellationToken);

            var compilation2 = _compilerRunner.CompileSources([inputs[1]], TestContext.Current.CancellationToken);

            var result2 = runner.RunSourceGenerator(compilation2, TestContext.Current.CancellationToken);

            AssertAllOutputs(result2, IncrementalStepRunReason.Cached);
            Assert.Empty(result2.Diagnostics);
        }

        [Fact]
        public void Regenerate_When_PropertyAdded()
        {
            var runner = new SourceGeneratorRunner<PocoDictionaryIncrementalGenerator>();
            var inputs = _testEnvironment.GetCachingSources();

            var compilation = _compilerRunner.CompileSources([inputs[0]], TestContext.Current.CancellationToken);

            runner.RunSourceGenerator(compilation, TestContext.Current.CancellationToken);

            var compilation2 = _compilerRunner.CompileSources([inputs[1]], TestContext.Current.CancellationToken);

            var result2 = runner.RunSourceGenerator(compilation2, TestContext.Current.CancellationToken);

            AssertAllOutputs(result2, IncrementalStepRunReason.Modified);
            Assert.Empty(result2.Diagnostics);
        }
    }
}
