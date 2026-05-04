using SourceGeneratorTools.Tests.TestInfrastructure;

namespace SourceGeneratorTools.Tests;

// ReSharper disable once InconsistentNaming
public class SourceBuilder_Append
{
    [Test]
    public async Task Append_WithString_AddsToTheSameLine()
    {
        var builder = new SourceBuilder();

        builder.Append("Hello, ");
        builder.Append("World");
        builder.AppendLine("!");

        await Assert.That(builder).HasContent("Hello, World!");
    }

    [Test]
    public async Task Append_WithNull_AddsEmptyString()
    {
        var builder = new SourceBuilder();

        builder.Append((string?)null);

        await Assert.That(builder).HasContent(string.Empty);
    }

    [Test]
    public async Task Append_InBlockWithNull_DoesNotIndent()
    {
        var builder = new SourceBuilder();

        using (builder.CreateBlock())
        {
            builder.Append((string?)null);
        }

        await Assert.That(builder).HasContent(
            """
            {
            }
            """);
    }

    [Test]
    public async Task Append_InBlock_IndentsAppendedText()
    {
        var builder = new SourceBuilder();

        builder.AppendLine("First line");
        using (builder.CreateBlock())
        {
            builder.Append("Hello, ");
            builder.AppendLine("World!");
        }

        builder.AppendLine("Next line");

        await Assert.That(builder).HasContent(
            """
            First line
            {
                Hello, World!
            }
            Next line
            """);
    }

    [Test]
    public async Task Append_InIndented_IndentsAppendedText()
    {
        var builder = new SourceBuilder();

        builder.AppendLine("First line");
        using (builder.CreateIndented())
        {
            builder.Append("Hello, ");
            builder.AppendLine("World!");
        }

        builder.AppendLine("Next line");

        await Assert.That(builder).HasContent(
            """
            First line
                Hello, World!
            Next line
            """);
    }
}
