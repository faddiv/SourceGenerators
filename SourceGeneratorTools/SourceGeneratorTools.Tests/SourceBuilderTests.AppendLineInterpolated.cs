using SourceGeneratorTools.Tests.TestInfrastructure;

namespace SourceGeneratorTools.Tests;

public partial class SourceBuilderTests
{
    [Fact]
    public void AppendLineInterpolated_WithString_AddsLineWithText()
    {
        var builder = new SourceBuilder();
        var name = TestHelpers.GenerateRandomName();

        builder.AppendLine($"Hello, {name}!");

        Assert.Content($"Hello, {name}!", builder);
    }

    [Fact]
    public void AppendLineInterpolated_WithArray_AddsCommaSeparatedList()
    {
        var builder = new SourceBuilder();
        var items = new[] { "Item1", "Item2", "Item3" };

        builder.AppendLine($"({items})");

        Assert.Content("(Item1, Item2, Item3)", builder);
    }

    [Fact]
    public void AppendLineInterpolated_WithIEnumerable_AddsCommaSeparatedList()
    {
        var builder = new SourceBuilder();
        IEnumerable<string> items = ["Item1", "Item2", "Item3"];

        builder.AppendLine($"({items})");

        Assert.Content("(Item1, Item2, Item3)", builder);
    }

    [Fact]
    public void AppendLineInterpolated_WithInt32_AddsValue()
    {
        var builder = new SourceBuilder();
        const int value = 42;

        builder.AppendLine($"Value: {value}");

        Assert.Content("Value: 42", builder);
    }

    [Fact]
    public void AppendLineInterpolated_WithInt32Nullable_AddsValue()
    {
        var builder = new SourceBuilder();
        int? value = 42;

        builder.AppendLine($"Value: {value}");

        Assert.Content("Value: 42", builder);
    }

    [Fact]
    public void AppendLineInterpolated_WithNull_AddsEmptyString()
    {
        var builder = new SourceBuilder();
        string? value = null;

        builder.AppendLine($"Value: {value}");

        Assert.Content("Value: ", builder);
    }

    [Fact]
    public void AppendLineInterpolated_WithObject_AddsObjectToString()
    {
        var builder = new SourceBuilder();
        var value = new { Name = "Test" };

        builder.AppendLine($"Value: {value}");

        Assert.Content("Value: { Name = Test }", builder);
    }
}
