using System;

namespace Foxy.Params.SourceGenerator.Helpers;

partial class SourceBuilder
{
    public Block StartBlock()
    {
        OpenBlock();
        return new Block(this, true);
    }

    public Block StartIndented()
    {
        IncreaseIndent();
        return new Block(this, false);
    }

    private void OpenBlock()
    {
        AppendLineInternal("{");
        IncreaseIndent();
    }

    private void CloseBlock()
    {
        DecreaseIndent();
        AppendLineInternal("}");
    }

    public readonly struct Block(SourceBuilder sourceBuilder, bool addClosingBracket) : IDisposable
    {
        private readonly bool _addClosingBracket = addClosingBracket;
        private readonly SourceBuilder _sourceBuilder = sourceBuilder;

        public void Dispose()
        {
            if (_addClosingBracket)
            {
                _sourceBuilder.CloseBlock();
            }
            else
            {
                _sourceBuilder.DecreaseIndent();
            }
        }
    }
}
