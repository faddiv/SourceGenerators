using System.Runtime.CompilerServices;

namespace SourceGeneratorTools;

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

    public void AppendLine(string text)
    {
        AppendLineInternal(text);
    }
}
