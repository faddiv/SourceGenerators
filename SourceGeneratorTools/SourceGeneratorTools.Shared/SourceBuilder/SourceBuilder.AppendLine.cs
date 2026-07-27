using System.Runtime.CompilerServices;

namespace AttributeParser.SourceGenerator.SourceBuilder;

public partial class SourceBuilder
{
    public void AppendLine([InterpolatedStringHandlerArgument("")] in InterpolatedStringHandler handler)
    {
        AppendNewLine();
    }

    public void AppendLine()
    {
        AppendNewLine();
    }

    public void AppendLine(string? text)
    {
        AppendLineInternal(text);
    }
}
