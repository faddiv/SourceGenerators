using SourceGeneratorTools.Tests.TestInfrastructure;

namespace SourceGeneratorTools.Tests;

public partial class SourceBuilderTests
{
    [Test]
    public async Task CreateLine_WithoutElements_CreatesSingleLine()
    {
        var builder = new SourceBuilder();

        using (builder.CreateLine())
        {
            // No elements added to the line
        }

        await Assert.That(builder).HasContent(Environment.NewLine);
    }

    [Test]
    [Arguments("\n")]
    [Arguments("\r\n")]
    public async Task CreateLine_WithMultipleElement_CreatesSingleLine(string newLine)
    {
        var builder = new SourceBuilder(newLine: newLine);

        using (var line = builder.CreateLine())
        {
            line.Append("Hello, ");
            line.Append("World!");
        }

        await Assert.That(builder).HasRawContent($"Hello, World!{newLine}");
    }

    [Test]
    public async Task CreateLine_WithInterpolatedString_CreatesSingleLine()
    {
        var builder = new SourceBuilder();
        string name = TestHelpers.GenerateRandomName();

        using (var line = builder.CreateLine())
        {
            line.Append($"Hello, {name}!");
        }

        builder.AppendLine("Next line");

        await Assert.That(builder).HasContent(
            $"""
             Hello, {name}!
             Next line
             """);
    }

    [Test]
    public async Task CreateLine_WithCommaSeparatedList_CreatesSingleLine()
    {
        var builder = new SourceBuilder();
        var items = new[] { "Item1", "Item2", "Item3" };

        using (var line = builder.CreateLine())
        {
            line.Append("(");
            line.Append($"{items}");
            line.Append(")");
        }

        await Assert.That(builder).HasContent("(Item1, Item2, Item3)");
    }

    [Test]
    public async Task CreateLine_WhenAppendLine_CreatesIndentedLines()
    {
        var builder = new SourceBuilder();

        using (var line = builder.CreateLine())
        {
            line.Append("public void ");
            line.AppendLine("Method(");
            line.AppendLine("param1,");
            line.AppendLine("param2);");
        }

        builder.AppendLine("{");

        await Assert.That(builder).HasContent(
            """
            public void Method(
                param1,
                param2);
            {
            """);
    }

    [Test]
    public async Task CreateLine_WhenAppendLine_SecondLineCanBeAddedWithoutIndent()
    {
        var builder = new SourceBuilder();

        using (var line = builder.CreateLine())
        {
            line.AppendLine("public void Method(");
            line.Append("param1, ");
            line.AppendLine("param2);");
        }

        builder.AppendLine("{");

        await Assert.That(builder).HasContent(
            """
            public void Method(
                param1, param2);
            {
            """);
    }

    [Test]
    public async Task CreateLine_WhenAppendLineInterpolated_SecondAppendLineIndented()
    {
        var builder = new SourceBuilder();
        var argument = TestHelpers.GenerateRandomName();

        using (var line = builder.CreateLine())
        {
            line.AppendLine($"Method({argument});");
            line.Append("Second line");
        }

        builder.AppendLine("Third line");

        await Assert.That(builder).HasContent(
            $"""
            Method({argument});
                Second line
            Third line
            """);
    }

    [Test]
    public async Task CreateLine_WhenAppendLine_SecondAppendLineInterpolatedIsIndented()
    {
        var builder = new SourceBuilder();
        var argument = TestHelpers.GenerateRandomName();

        using (var line = builder.CreateLine())
        {
            line.AppendLine("Method");
            line.AppendLine($"Second line {argument}");
        }

        builder.AppendLine("Third line");

        await Assert.That(builder).HasContent(
            $"""
            Method
                Second line {argument}
            Third line
            """);
    }
}
