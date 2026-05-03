using SourceGeneratorTools.Tests.TestInfrastructure;

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

    [Theory]
    [InlineData("\n")]
    [InlineData("\r\n")]
    public void AppendLine_WithDifferentNewLine_AddsLineWithCustomNewLine(string newLine)
    {
        var builder = new SourceBuilder(newLine: newLine);
        const string text = "Hello, World!";

        builder.AppendLine(text);

        Assert.RawContent($"{text}{newLine}", builder);
    }

    [Theory]
    [InlineData("\n")]
    [InlineData("\r\n")]
    public void AppendLineEmpty_WithDifferentNewLine_AddsLineWithCustomNewLine(string newLine)
    {
        var builder = new SourceBuilder(newLine: newLine);

        builder.AppendLine();

        Assert.RawContent(newLine, builder);
    }
}
