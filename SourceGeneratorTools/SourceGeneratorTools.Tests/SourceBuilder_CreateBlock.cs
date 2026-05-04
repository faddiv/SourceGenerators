using SourceGeneratorTools.Tests.TestInfrastructure;

namespace SourceGeneratorTools.Tests;

// ReSharper disable once InconsistentNaming
public partial class SourceBuilder_CreateBlock
{
    [Test]
    public async Task CreateBlock_WithNoParameter_CreatesBlockWithBraces()
    {
        var builder = new SourceBuilder();

        builder.AppendLine("First line");
        using (builder.CreateBlock())
        {
            await Assert.That(builder.IndentLevel).IsEqualTo(1);
            builder.AppendLine("Inside block");
        }

        builder.AppendLine("Next line");

        await Assert.That(builder.IndentLevel).IsEqualTo(0);
        await Assert.That(builder).HasContent(
            """
            First line
            {
                Inside block
            }
            Next line
            """);
    }

    [Test]
    public async Task CreateBlock_WithParameter_CreatesBlockWithCustomStartAndEnd()
    {
        var builder = new SourceBuilder();

        builder.AppendLine("First line");
        using (builder.CreateBlock("[", "]"))
        {
            await Assert.That(builder.IndentLevel).IsEqualTo(1);
            builder.AppendLine("Element 1,");
            builder.AppendLine("Element 2");
        }

        builder.AppendLine("Next line");

        await Assert.That(builder.IndentLevel).IsEqualTo(0);
        await Assert.That(builder).HasContent(
            """
            First line
            [
                Element 1,
                Element 2
            ]
            Next line
            """);
    }
}
