using Foxy.Params.SourceGenerator.Helpers;

namespace SourceGeneratorTools.Tests;

public partial class SourceBuilderTests
{
    [Fact]
    public void CreateBlock_WithNoParameter_CreatesBlockWithBraces()
    {
        var builder = new SourceBuilder();

        builder.AppendLine("First line");
        using (builder.CreateBlock())
        {
            builder.AppendLine("Inside block");
        }

        builder.AppendLine("Next line");

        Assert.Content(
            """
            First line
            {
                Inside block
            }
            Next line
            """, builder);
    }

    [Fact]
    public void CreateBlock_WithParameter_CreatesBlockWithCustomStartAndEnd()
    {
        var builder = new SourceBuilder();

        builder.AppendLine("First line");
        using (builder.CreateBlock("[", "]"))
        {
            builder.AppendLine("Element 1,");
            builder.AppendLine("Element 2");
        }

        builder.AppendLine("Next line");

        Assert.Content(
            """
            First line
            [
                Element 1,
                Element 2
            ]
            Next line
            """, builder);
    }
}
