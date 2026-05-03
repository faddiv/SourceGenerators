using System;
using System.Runtime.CompilerServices;

namespace Foxy.Params.SourceGenerator.Helpers;

partial class SourceBuilder
{
    public ref struct SourceLine : IDisposable
    {
        private readonly SourceBuilder _builder;
        private readonly int _originalIndent;
        private bool _indentAdded;

        public SourceLine(SourceBuilder builder)
        {
            _builder = builder;
            _originalIndent = builder._intendLevel;
            _indentAdded = true;
            builder.AddIndent();
            _builder.IncreaseIndent();
        }

        public void Dispose()
        {
            if (!EndsWithNewline())
            {
                _builder.AppendLine();
            }

            _builder._intendLevel = _originalIndent;
        }

        // ReSharper disable once UnusedParameter.Global
        public void Append(
            [InterpolatedStringHandlerArgument("")]
            in InterpolatedStringHandler input)
        {
        }

        public void Append(string segment)
        {
            EnsureIndentation();

            _builder.AppendInternal(segment);
        }

        public void AppendLine(string segment)
        {
            EnsureIndentation();

            _builder.AppendInternal(segment);
            _builder.AppendLine();

            _indentAdded = false;
        }

        private void EnsureIndentation()
        {
            if (_indentAdded) return;
            _builder.AddIndent();
            _indentAdded = true;
        }

        private bool EndsWithNewline()
        {
            var builder = _builder._builder;
            if (builder.Length == 0)
            {
                return false;
            }

            var newLine = Environment.NewLine;

            return newLine.Length switch
            {
                1 => builder[^1] == newLine[0],
                2 => builder.Length > 1 && builder[^2] == newLine[0] && builder[^1] == newLine[1],
                _ => false
            };
        }

        public static implicit operator SourceBuilderSegment(SourceLine line) =>
            new(line._builder, SourceBuilderSegmentFlags.None);
    }
}
