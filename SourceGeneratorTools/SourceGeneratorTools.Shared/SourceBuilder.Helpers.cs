using System.Collections.Generic;
using System.Text;

namespace SourceGeneratorTools;

partial class SourceBuilder
{
    private static class Helpers
    {
        public static void AppendJoin<T>(StringBuilder builder, string? separator, IEnumerable<T> values)
        {
            using var en = values.GetEnumerator();
            // Stryker disable once block, statement
            if (!en.MoveNext())
            {
                return;
            }

            var value = en.Current;
            if (value != null)
            {
                builder.Append(value);
            }

            while (en.MoveNext())
            {
                builder.Append(separator);
                value = en.Current;
                if (value != null)
                {
                    builder.Append(value);
                }
            }
        }
    }
}
