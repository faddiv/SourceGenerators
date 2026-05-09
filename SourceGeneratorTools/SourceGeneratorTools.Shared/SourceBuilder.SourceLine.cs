using System;
using System.Runtime.CompilerServices;

namespace SourceGeneratorTools;

partial class SourceBuilder
{
    public readonly struct SourceLine(SourceBuilder builder) : IDisposable
    {
        private readonly int _originalIndent = builder._indentLevel;

        public SourceBuilder Builder { get; } = builder;

        public void Dispose()
        {
            if (!EndsWithNewline())
            {
                Builder.AppendLine();
            }

            Builder._indentLevel = _originalIndent;
        }

        // ReSharper disable once UnusedParameter.Global
        public void Append(
            [InterpolatedStringHandlerArgument("")]
            in InterpolatedStringHandler input)
        {
        }

        public void Append(string segment)
        {
            Builder.AppendInternal(segment);
        }

        public void AppendLine(
            [InterpolatedStringHandlerArgument("")]
            in InterpolatedStringHandler input)
        {
            Builder.AppendLine();

            EnsureSecondLineIndentation();
        }

        public void AppendLine(string segment)
        {
            Builder.AppendLineInternal(segment);

            EnsureSecondLineIndentation();
        }

        private void EnsureSecondLineIndentation()
        {
            if (_originalIndent == Builder._indentLevel)
            {
                Builder.IncreaseIndent();
            }
        }

        private bool EndsWithNewline()
        {
            var builder = Builder._builder;
            if (builder.Length == 0)
            {
                return false;
            }

            var newLine = Builder.NewLine;
            return newLine.Length switch
            {
                1 => builder[^1] == newLine[0],
                // Stryker disable once all
                2 => builder.Length > 1 && builder[^2] == newLine[0] && builder[^1] == newLine[1],
                // Stryker disable once all
                _ => throw new InvalidOperationException("NewLine must be either 1 or 2 characters long.")
            };
        }

        public static implicit operator SourceBuilderSegment(SourceLine line)
        {
            return new SourceBuilderSegment(line.Builder);
        }
    }
}
