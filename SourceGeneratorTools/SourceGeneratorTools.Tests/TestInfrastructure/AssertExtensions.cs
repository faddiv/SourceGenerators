namespace SourceGeneratorTools.Tests.TestInfrastructure;

public static class AssertExtensions
{
    extension(Assert)
    {
        public static void Content(string expected, SourceBuilder builder)
        {
            if (!string.IsNullOrEmpty(expected) && !expected.EndsWith(Environment.NewLine))
            {
                expected += Environment.NewLine;
            }

            Assert.Equal(expected, builder.ToString());
        }

        public static void RawContent(string expected, SourceBuilder builder)
        {
            Assert.Equal(expected, builder.ToString());
        }
    }
}
