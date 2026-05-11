using SourceGeneratorTools.Tests.TestInfrastructure;

namespace SourceGeneratorTools.Tests.SourceBuilderTests;

// ReSharper disable once InconsistentNaming
public class SourceBuilder_AppendLine
{
    [Test]
    public async Task AppendLine_WithoutParameters_AddsEmptyLine()
    {
        var builder = new SourceGeneratorTools.SourceBuilder();

        builder.AppendLine();

        await Assert.That(builder).HasContent(Environment.NewLine);
    }

    [Test]
    public async Task AppendLine_WithString_AddsLineWithText()
    {
        var builder = new SourceGeneratorTools.SourceBuilder();
        const string text = "Hello, World!";

        builder.AppendLine(text);

        await Assert.That(builder).HasContent(text);
    }

    [Test]
    public async Task AppendLine_WithNull_DoesNotAddLine()
    {
        var builder = new SourceGeneratorTools.SourceBuilder();

        builder.AppendLine("First line");
        // ReSharper disable once RedundantCast
        builder.AppendLine((string?)null);
        builder.AppendLine("Last line");

        await Assert.That(builder).HasContent(
            """
            First line
            Last line
            """);
    }

    [Test]
    public async Task AppendLine_InBlockWithNull_DoesNotIndent()
    {
        var builder = new SourceGeneratorTools.SourceBuilder();

        using (builder.CreateBlock())
        {
            // ReSharper disable once RedundantCast
            builder.AppendLine((string?)null);

        }

        await Assert.That(builder).HasContent(
            """
            {
            }
            """);
    }

    [Test]
    [Arguments("\n")]
    [Arguments("\r\n")]
    public async Task AppendLine_WithDifferentNewLine_AddsLineWithCustomNewLine(string newLine)
    {
        var builder = new SourceGeneratorTools.SourceBuilder(newLine: newLine);
        const string text = "Hello, World!";

        builder.AppendLine(text);

        await Assert.That(builder).HasRawContent($"{text}{newLine}");
    }

    [Test]
    [Arguments("\n")]
    [Arguments("\r\n")]
    public async Task AppendLineEmpty_WithDifferentNewLine_AddsLineWithCustomNewLine(string newLine)
    {
        var builder = new SourceGeneratorTools.SourceBuilder(newLine: newLine);

        builder.AppendLine();

        await Assert.That(builder).HasRawContent(newLine);
    }
}
