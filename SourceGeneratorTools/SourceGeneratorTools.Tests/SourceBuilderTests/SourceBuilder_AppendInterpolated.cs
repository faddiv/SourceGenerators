using SourceGeneratorTools.Tests.TestInfrastructure;

namespace SourceGeneratorTools.Tests.SourceBuilderTests;

public class SourceBuilder_AppendInterpolated
{
    [Test]
    public async Task Append_WithString_AddsToTheSameLine()
    {
        var builder = new SourceGeneratorTools.SourceBuilder();
        var text = TestHelpers.GenerateRandomName();

        builder.Append($"Hello, {text}!");
        builder.Append($" Goodbye, {text}!");
        builder.AppendLine($" {text}!");


        await Assert.That(builder).HasContent(
            $"Hello, {text}! Goodbye, {text}! {text}!");
    }

    [Test]
    public async Task Append_InBlock_IndentsAppendedText()
    {
        var builder = new SourceGeneratorTools.SourceBuilder();
        var text = TestHelpers.GenerateRandomName();

        builder.AppendLine("First line");
        using (builder.CreateBlock())
        {
            builder.Append($"Hello, {text}!");
            builder.Append($" Goodbye, {text}!");
            builder.AppendLine($" {text}!");
        }

        builder.AppendLine("Next line");

        await Assert.That(builder).HasContent(
            $$"""
            First line
            {
                Hello, {{text}}! Goodbye, {{text}}! {{text}}!
            }
            Next line
            """);
    }

    [Test]
    public async Task Append_InIndented_IndentsAppendedText()
    {
        var builder = new SourceGeneratorTools.SourceBuilder();
        var text = TestHelpers.GenerateRandomName();

        builder.AppendLine("First line");
        using (builder.CreateIndented())
        {
            builder.Append($"Hello, {text}!");
            builder.Append($" Goodbye, {text}!");
            builder.AppendLine($" {text}!");
        }

        builder.AppendLine("Next line");

        await Assert.That(builder).HasContent(
            $$"""
            First line
                Hello, {{text}}! Goodbye, {{text}}! {{text}}!
            Next line
            """);
    }
}
