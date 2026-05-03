using SourceGeneratorTools.Tests.TestInfrastructure;

namespace SourceGeneratorTools.Tests;

public partial class SourceBuilderTests
{
    [Fact]
    public void CreateIndented_CreatesIndentedBuilder()
    {
        var builder = new SourceBuilder();

        builder.AppendLine("First line");
        using (builder.CreateIndented())
        {
            builder.AppendLine("Indented line");
        }

        builder.AppendLine("Next line");

        Assert.Content(
            """
            First line
                Indented line
            Next line
            """, builder);
    }
}
