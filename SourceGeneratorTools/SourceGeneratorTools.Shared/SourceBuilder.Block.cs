using System;

namespace SourceGeneratorTools;

partial class SourceBuilder
{
    public Block CreateBlock(string openingElement = "{", string closingElement = "}")
    {
        OpenBlock(openingElement);
        return new Block(this, closingElement);
    }

    public Block CreateIndented()
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
