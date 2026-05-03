using SourceGeneratorTools.Tests.TestInfrastructure;

namespace SourceGeneratorTools.Tests;

public partial class SourceBuilderTests
{
    [Fact]
    public void CreateLine_WithMultipleElement_CreatesSingleLine()
    {
        var builder = new SourceBuilder();

        using (var line = builder.CreateLine())
        {
            line.Append("Hello, ");
            line.Append("World!");
        }

        Assert.Content("Hello, World!", builder);
    }

    [Fact]
    public void CreateLine_WithInterpolatedString_CreatesSingleLine()
    {
        var builder = new SourceBuilder();
        string name = TestHelpers.GenerateRandomName();

        using (var line = builder.CreateLine())
        {
            line.Append($"Hello, {name}!");
        }

        builder.AppendLine("Next line");

        Assert.Content(
            $"""
             Hello, {name}!
             Next line
             """, builder);
    }

    [Fact]
    public void CreateLine_WithCommaSeparatedList_CreatesSingleLine()
    {
        var builder = new SourceBuilder();
        var items = new[] { "Item1", "Item2", "Item3" };

        using (var line = builder.CreateLine())
        {
            line.Append("(");
            line.Append($"{items}");
            line.Append(")");
        }

        Assert.Content("(Item1, Item2, Item3)", builder);
    }

    [Fact]
    public void CreateLine_WhenAppendLine_CreatesIndentedLines()
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

        Assert.Content(
            """
            public void Method(
                param1,
                param2);
            {
            """, builder);
    }

    [Fact]
    public void CreateLine_WhenAppendLine_SecondLineCanBeAddedWithoutIndent()
    {
        var builder = new SourceBuilder();

        using (var line = builder.CreateLine())
        {
            line.AppendLine("public void Method(");
            line.Append("param1, ");
            line.AppendLine("param2);");
        }

        builder.AppendLine("{");

        Assert.Content(
            """
            public void Method(
                param1, param2);
            {
            """, builder);
    }

    [Fact]
    public void CreateLine_WhenAppendLineInterpolated_SecondAppendLineIndented()
    {
        var builder = new SourceBuilder();
        var argument = TestHelpers.GenerateRandomName();

        using (var line = builder.CreateLine())
        {
            line.AppendLine($"Method({argument});");
            line.Append("Second line");
        }

        builder.AppendLine("Third line");

        Assert.Content(
            $"""
            Method({argument});
                Second line
            Third line
            """, builder);
    }

    [Fact]
    public void CreateLine_WhenAppendLine_SecondAppendLineInterpolatedIsIndented()
    {
        var builder = new SourceBuilder();
        var argument = TestHelpers.GenerateRandomName();

        using (var line = builder.CreateLine())
        {
            line.AppendLine("Method");
            line.AppendLine($"Second line {argument}");
        }

        builder.AppendLine("Third line");

        Assert.Content(
            $"""
            Method
                Second line {argument}
            Third line
            """, builder);
    }
}
