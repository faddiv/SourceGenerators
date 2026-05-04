using SourceGeneratorTools.Tests.TestInfrastructure;

namespace SourceGeneratorTools.Tests;

public partial class SourceBuilderTests
{
    [Test]
    public async Task AppendLine_WithoutParameters_AddsEmptyLine()
    {
        var builder = new SourceBuilder();

        builder.AppendLine();

        await Assert.That(builder).HasContent(Environment.NewLine);
    }

    [Test]
    public async Task AppendLine_WithString_AddsLineWithText()
    {
        var builder = new SourceBuilder();
        const string text = "Hello, World!";

        builder.AppendLine(text);

        await Assert.That(builder).HasContent(text);
    }

    [Test]
    [Arguments("\n")]
    [Arguments("\r\n")]
    public async Task AppendLine_WithDifferentNewLine_AddsLineWithCustomNewLine(string newLine)
    {
        var builder = new SourceBuilder(newLine: newLine);
        const string text = "Hello, World!";

        builder.AppendLine(text);

        await Assert.That(builder).HasRawContent($"{text}{newLine}");
    }

    [Test]
    [Arguments("\n")]
    [Arguments("\r\n")]
    public async Task AppendLineEmpty_WithDifferentNewLine_AddsLineWithCustomNewLine(string newLine)
    {
        var builder = new SourceBuilder(newLine: newLine);

        builder.AppendLine();

        await Assert.That(builder).HasRawContent(newLine);
    }
}
