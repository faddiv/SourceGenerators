using System.Runtime.CompilerServices;

namespace SourceGeneratorTools;

public sealed partial class SourceBuilder
{
    public void Append(string? text)
    {
        AppendInternal(text);
    }

    public void Append([InterpolatedStringHandlerArgument("")] in InterpolatedStringHandler handler)
    {
    }
}
