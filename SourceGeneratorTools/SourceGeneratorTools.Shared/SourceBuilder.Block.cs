using System;

namespace Foxy.Params.SourceGenerator.Helpers;

partial class SourceBuilder
{
    public Block StartBlock(string openingElement = "{", string closingElement = "}")
    {
        OpenBlock(openingElement);
        return new Block(this, closingElement);
    }

    public Block StartIndented()
    {
        IncreaseIndent();
        return new Block(this, null);
    }

    private void OpenBlock(string openingElement = "{")
    {
        AppendLineInternal(openingElement);
        IncreaseIndent();
    }

    private void CloseBlock(string closingElement)
    {
        DecreaseIndent();
        AppendLineInternal(closingElement);
    }

    public readonly struct Block(SourceBuilder sourceBuilder, string? closingElement) : IDisposable
    {
        private readonly SourceBuilder _sourceBuilder = sourceBuilder;
        private readonly string? _closingElement = closingElement;

        public void Dispose()
        {
            if (_closingElement is not null)
            {
                _sourceBuilder.CloseBlock(_closingElement);
            }
            else
            {
                _sourceBuilder.DecreaseIndent();
            }
        }
    }
}
