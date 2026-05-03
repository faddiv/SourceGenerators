using Foxy.Params.SourceGenerator.Helpers;

namespace SourceGeneratorTools.Tests;

public partial class SourceBuilderTests
{
    [Fact]
    public void Ctor_WithoutParameters_CreatesDefaultInstance()
    {
        var builder = new SourceBuilder();

        Assert.Equal(string.Empty, builder.ToString());
        Assert.Equal("    ", builder.Indent);
    }

    [Fact]
    public void Ctor_WithoutDifferentIndentSize_CreatesInstanceWithCustomIndent()
    {
        const string indent = "  ";
        var builder = new SourceBuilder(indent);

        Assert.Equal(indent, builder.Indent);
    }
}
