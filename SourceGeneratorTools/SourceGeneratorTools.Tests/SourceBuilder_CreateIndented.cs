using SourceGeneratorTools.Tests.TestInfrastructure;

namespace SourceGeneratorTools.Tests;

public partial class SourceBuilder_CreateIndented
{
    [Test]
    public async Task CreateIndented_CreatesIndentedBuilder()
    {
        var builder = new SourceBuilder();

        builder.AppendLine("First line");
        using (builder.CreateIndented())
        {
            builder.AppendLine("Indented line");
        }

        builder.AppendLine("Next line");

        await Assert.That(builder).HasContent(
            """
            First line
                Indented line
            Next line
            """);
    }
}
