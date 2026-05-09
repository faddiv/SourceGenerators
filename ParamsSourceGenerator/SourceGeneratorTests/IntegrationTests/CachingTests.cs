using System.Threading;
using System.Threading.Tasks;
using Foxy.Params.SourceGenerator.Data;
using SourceGeneratorTests.TestInfrastructure;
using Foxy.Params.SourceGenerator;
using static SourceGeneratorTests.TestInfrastructure.CachingTestHelpers;
using Microsoft.CodeAnalysis;
using Test.Infrastructure;

namespace SourceGeneratorTests.IntegrationTests
{
    [ClassDataSource<TestEnvironment>(Shared = SharedType.PerTestSession)]
    public class CachingTests
    {
        // A collection of all the tracking names. I'll show how to simplify this later
        private static readonly string[] _allTrackingNames = [TrackingNames.GetSpanParamsMethods, TrackingNames.NotNullFilter];
        private readonly CompilerRunner _compilerRunner;
        private readonly TestEnvironment _testEnvironment;

        public CachingTests(TestEnvironment testEnvironment)
        {
            _testEnvironment = testEnvironment;
            _compilerRunner = _testEnvironment.Compiler;
        }

        [Test]
        public async Task Caches_When_Nothing_Changes(CancellationToken cancellationToken)
        {
            var runner = new SourceGeneratorRunner<ParamsIncrementalGenerator>();
            var input = _testEnvironment.GetCachingSource();
            var expected = _testEnvironment.GetCachingOutputs();

            var compilation = _compilerRunner.CompileSources([input], cancellationToken);

            var result1 = runner.RunSourceGenerator(compilation, cancellationToken);

            var compilation2 = compilation.Clone();

            var result2 = runner.RunSourceGenerator(compilation2, cancellationToken);

            await AssertRunsEqual(result1, result2, _allTrackingNames);
            await AssertAllOutputs(result2, IncrementalStepRunReason.Cached);
            await Assert.That(result1.Diagnostics).IsEmpty();
            await AssertOutputsMatch(result1, expected);
        }

        [Test]
        public async Task Caches_When_MethodBody_Changes(CancellationToken cancellationToken)
        {
            var runner = new SourceGeneratorRunner<ParamsIncrementalGenerator>();
            var inputs = _testEnvironment.GetCachingSources();
            var expected = _testEnvironment.GetCachingOutputs();

            var compilation = _compilerRunner.CompileSources([inputs[0]], cancellationToken);

            var result1 = runner.RunSourceGenerator(compilation, cancellationToken);

            var compilation2 = _compilerRunner.CompileSources([inputs[1]], cancellationToken);

            var result2 = runner.RunSourceGenerator(compilation2, cancellationToken);

            await AssertRunsEqual(result1, result2, _allTrackingNames);
            await AssertAllOutputs(result2, IncrementalStepRunReason.Cached);
            await Assert.That(result2.Diagnostics).IsEmpty();
            await AssertOutputsMatch(result1, expected);
        }

        [Test]
        public async Task Regenerate_When_MaxOverrides_Changes(CancellationToken cancellationToken)
        {
            var runner = new SourceGeneratorRunner<ParamsIncrementalGenerator>();
            var inputs = _testEnvironment.GetCachingSources();
            var expected = _testEnvironment.GetCachingOutputs();

            var compilation = _compilerRunner.CompileSources([inputs[0]], cancellationToken);

            runner.RunSourceGenerator(compilation, cancellationToken);

            var compilation2 = _compilerRunner.CompileSources([inputs[1]], cancellationToken);

            var result2 = runner.RunSourceGenerator(compilation2, cancellationToken);

            await AssertAllOutputs(result2, IncrementalStepRunReason.Modified);
            await Assert.That(result2.Diagnostics).IsEmpty();
            await AssertOutputsMatch(result2, expected);
        }

        [Test]
        public async Task Regenerate_When_HasParams_Changes(CancellationToken cancellationToken)
        {
            var runner = new SourceGeneratorRunner<ParamsIncrementalGenerator>();
            var inputs = _testEnvironment.GetCachingSources();
            var expected = _testEnvironment.GetCachingOutputs();

            var compilation = _compilerRunner.CompileSources([inputs[0]], cancellationToken);

            runner.RunSourceGenerator(compilation, cancellationToken);

            var compilation2 = _compilerRunner.CompileSources([inputs[1]], cancellationToken);

            var result2 = runner.RunSourceGenerator(compilation2, cancellationToken);

            await AssertAllOutputs(result2, IncrementalStepRunReason.Modified);
            await Assert.That(result2.Diagnostics).IsEmpty();
            await AssertOutputsMatch(result2, expected);
        }

        [Test]
        public async Task Caches_When_OtherFileChanges(CancellationToken cancellationToken)
        {
            var runner = new SourceGeneratorRunner<ParamsIncrementalGenerator>();
            var inputs = _testEnvironment.GetCachingSources();
            var expected = _testEnvironment.GetCachingOutputs();

            var compilation = _compilerRunner.CompileSources([inputs[0], inputs[1]], cancellationToken);

            runner.RunSourceGenerator(compilation, cancellationToken);

            var compilation2 = _compilerRunner.CompileSources([inputs[0], inputs[2]], cancellationToken);

            var result2 = runner.RunSourceGenerator(compilation2, cancellationToken);

            await AssertOutputsMatch(result2, expected);
            await AssertOutput(result2, "Something.Foo.g.cs", IncrementalStepRunReason.Cached);
            await AssertOutput(result2, "Something.Baz.g.cs", IncrementalStepRunReason.Modified);
        }
    }
}
