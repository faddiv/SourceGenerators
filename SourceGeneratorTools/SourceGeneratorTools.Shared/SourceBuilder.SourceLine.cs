using System;
using System.Runtime.CompilerServices;

namespace SourceGeneratorTools;

partial class SourceBuilder
{
    public ref struct SourceLine(SourceBuilder builder) : IDisposable
    {
        private readonly SourceBuilder _builder = builder;
        private readonly int _originalIndent = builder._intendLevel;
        private bool _indentAdded;

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
            EnsureIndentationApplied();

            _builder.AppendInternal(segment);
        }

        public void AppendLine(
            [InterpolatedStringHandlerArgument("")]
            in InterpolatedStringHandler input)
        {
            _builder.AppendLine();

            EnsureSecondLineIndentation();

            _indentAdded = false;
        }

        public void AppendLine(string segment)
        {
            EnsureIndentationApplied();

            _builder.AppendInternal(segment);
            _builder.AppendLine();

            EnsureSecondLineIndentation();

            _indentAdded = false;
        }

        private void EnsureIndentationApplied()
        {
            if (_indentAdded) return;
            _builder.AddIndent();
            _indentAdded = true;
        }

        private void EnsureSecondLineIndentation()
        {
            if (_originalIndent == _builder._intendLevel)
            {
                _builder.IncreaseIndent();
            }
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

        public static implicit operator SourceBuilderSegment(SourceLine line)
        {
            line.EnsureIndentationApplied();
            return new SourceBuilderSegment(line._builder, SourceBuilderSegmentFlags.None);
        }
    }
}
