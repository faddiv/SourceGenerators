using System.Runtime.CompilerServices;

namespace SourceGeneratorTools;

partial class SourceBuilder
{
    [InterpolatedStringHandler]
    public readonly ref struct InterpolatedStringHandler
    {
        private readonly SourceBuilder _builder;

        public InterpolatedStringHandler(int literalLength, int formattedCount, SourceBuilderSegment segment)
        {
            _builder = segment.Builder;
            // Stryker disable once all
            _builder.EnsureCapacity(_builder._builder.Length + literalLength + (formattedCount << 4));
        }


        public void AppendLiteral(string s)
        {
            _builder.AppendInternal(s);
        }

        public void AppendFormatted<T>(T? t)
        {
            if (t is IEnumerable<string> strings)
            {
                AppendFormatted(strings);
            }
            else
            {
                _builder.AppendInternal(t);
            }
        }

        public void AppendFormatted(string? arg)
        {
            if (arg is not null)
            {
                _builder.AppendInternal(arg);
            }
        }

        public void AppendFormatted<T>(IEnumerable<T>? args)
        {
            _builder.AddCommaSeparatedList(args);
        }

        public void AppendFormatted<T>(T[]? args)
        {
            _builder.AddCommaSeparatedList(args);
        }
    }
}
