using SourceGeneratorTools.Tests.TestInfrastructure;

namespace SourceGeneratorTools.Tests.SourceBuilderTests;

// ReSharper disable once InconsistentNaming
public class SourceBuilder_Ctor
{
    [Test]
    public async Task Ctor_WithoutParameters_CreatesDefaultInstance()
    {
        var builder = new SourceGeneratorTools.SourceBuilder();

        await Assert.That(builder).HasContent(string.Empty);
        await Assert.That(builder.NewLine).IsEqualTo(Environment.NewLine);
        await Assert.That(builder.Indent).IsEqualTo("    ");
    }

    [Test]
    public async Task Ctor_WithDifferentIndent_CreatesInstanceWithCustomIndent()
    {
        const string indent = "\t";
        var builder = new SourceGeneratorTools.SourceBuilder(indent);

        await Assert.That(builder.Indent).IsEqualTo(indent);
    }

    [Test]
    [Arguments("\n")]
    [Arguments("\r\n")]
    public async Task Ctor_WithDifferentNewLine_CreatesInstanceWithCustomNewLine(string newLine)
    {
        var builder = await Assert.That(() => new SourceGeneratorTools.SourceBuilder(newLine: newLine))
            .ThrowsNothing()
            .And
            .IsNotNull();

        builder.AppendLine("Test");

        await Assert.That(builder.NewLine).IsEqualTo(newLine);
    }

    [Test]
    [Arguments("")]
    [Arguments("inv")]
    public void Ctor_WithInvalidNewLine_ThrowsArgumentException(string newLine)
    {
        Assert.Throws<ArgumentException>(() => _ = new SourceGeneratorTools.SourceBuilder(newLine: newLine));
    }
}
