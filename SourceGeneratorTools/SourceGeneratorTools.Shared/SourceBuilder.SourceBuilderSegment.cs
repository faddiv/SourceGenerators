using System;

namespace SourceGeneratorTools;

partial class SourceBuilder
{
    public readonly ref struct SourceBuilderSegment
    {
        public SourceBuilderSegment(SourceBuilder builder, bool addIndent = false)
        {
            Builder = builder;
            if (addIndent)
            {
                Builder.AddIndent();
            }
        }

        public SourceBuilder Builder { get; }
    }
}
