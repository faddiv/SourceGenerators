namespace AttributeParser.SourceGenerator;

internal static class Extensions
{
    public static string ToCamelCase(this string input)
    {
        if (string.IsNullOrEmpty(input) || char.IsLower(input[0]))
            return input;

        return $"{char.ToLower(input[0])}{input[1..]}";
    }
}
