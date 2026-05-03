using System;

namespace SourceGeneratorTools;

partial class SourceBuilder
{
    public readonly ref struct SourceBuilderSegment
    {
        private readonly SourceBuilderSegmentFlags _flags;

        public SourceBuilderSegment(SourceBuilder builder, SourceBuilderSegmentFlags flags)
        {
            _flags = flags;
            Builder = builder;
            if (_flags.HasFlag(SourceBuilderSegmentFlags.AddIndent))
            {
                Builder.AddIndent();
            }
        }

        public SourceBuilder Builder { get; }
    }

    [Flags]
    public enum SourceBuilderSegmentFlags
    {
        None = 0,
        AddIndent = 1,
        AddNewLine = 2,
        AddAll = AddIndent | AddNewLine
    }
}
