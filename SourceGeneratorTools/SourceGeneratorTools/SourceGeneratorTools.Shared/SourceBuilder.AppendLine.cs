using System.Runtime.CompilerServices;

namespace Foxy.Params.SourceGenerator.Helpers;

public partial class SourceBuilder
{
    public void AppendLine([InterpolatedStringHandlerArgument("")] in InterpolatedStringHandler handler)
    {
        AppendLine();
    }

    public void AppendLine()
    {
        _builder.AppendLine();
    }

    public void AppendLine(string text)
    {
        AddIndent();
        _builder.AppendLine(text);
    }
}
