using System.Collections.Generic;
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
            _builder.EnsureCapacity(_builder._builder.Length + literalLength + (formattedCount << 4));
        }


        public void AppendLiteral(string s)
        {
            _builder.Append(s);
        }

        public void AppendFormatted<T>(T? t)
        {
            if (t is null)
            {
                return;
            }

            if (t is IEnumerable<string> strings)
            {
                AppendFormatted(strings);
            }
            else
            {
                _builder.AppendFormatted(t);
            }
        }

        public void AppendFormatted(string? arg)
        {
            if (arg is not null)
            {
                _builder.Append(arg);
            }
        }

        public void AppendFormatted(int arg)
        {
            _builder.Append(arg);
        }

        public void AppendFormatted(int? arg)
        {
            if (arg.HasValue)
            {
                _builder.Append(arg.Value);
            }
        }

        public void AppendFormatted<T>(IEnumerable<T>? args)
        {
            if (args is null)
            {
                return;
            }

            _builder.AddCommaSeparatedList(args);
        }

        public void AppendFormatted<T>(T[]? args)
        {
            if (args is null)
            {
                return;
            }

            _builder.AddCommaSeparatedList(args);
        }
    }
}
