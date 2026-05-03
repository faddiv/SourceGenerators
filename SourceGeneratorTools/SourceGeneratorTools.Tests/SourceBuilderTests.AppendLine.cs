using Foxy.Params.SourceGenerator.Helpers;

namespace SourceGeneratorTools.Tests;

public partial class SourceBuilderTests
{
    [Fact]
    public void AppendLine_WithoutParameters_AddsEmptyLine()
    {
        var builder = new SourceBuilder();

        builder.AppendLine();

        Assert.Content(Environment.NewLine, builder);
    }

    [Fact]
    public void AppendLine_WithString_AddsLineWithText()
    {
        var builder = new SourceBuilder();
        const string text = "Hello, World!";

        builder.AppendLine(text);

        Assert.Content(text, builder);
    }
}
