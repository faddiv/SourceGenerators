using System;

namespace SourceGeneratorTools;

partial class SourceBuilder
{
    public readonly ref struct SourceBuilderSegment(SourceBuilder builder)
    {
        public SourceBuilder Builder { get; } = builder;
    }
}
