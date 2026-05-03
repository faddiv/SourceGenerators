namespace SourceGeneratorTools.Tests;

public partial class SourceBuilderTests
{
    [Fact]
    public void Ctor_WithoutParameters_CreatesDefaultInstance()
    {
        var builder = new SourceBuilder();

        Assert.Equal(string.Empty, builder.ToString());
        Assert.Equal(Environment.NewLine, builder.NewLine);
        //Assert.Equal("    ", builder.Indent);
    }

    [Fact]
    public void Ctor_WithDifferentIndent_CreatesInstanceWithCustomIndent()
    {
        const string indent = "\t";
        var builder = new SourceBuilder(indent);

        Assert.Equal(indent, builder.Indent);
    }

    [Theory]
    [InlineData("\n")]
    [InlineData("\r\n")]
    public void Ctor_WithDifferentNewLine_CreatesInstanceWithCustomNewLine(string newLine)
    {
        var builder = new SourceBuilder(newLine: newLine);

        builder.AppendLine("Test");

        Assert.Equal(newLine, builder.NewLine);
    }

    [Theory]
    [InlineData("")]
    [InlineData("inv")]
    public void Ctor_WithInvalidNewLine_ThrowsArgumentException(string newLine)
    {
        Assert.Throws<ArgumentException>(() => new SourceBuilder(newLine: newLine));
    }
}
