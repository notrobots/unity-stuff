using System;
using System.Collections.Generic;
using System.Text;

public static class IEnumerableExtensions
{
    public static string JoinToString<T>(
        this IEnumerable<T> enumerable,
        string separator = ", ",
        string prefix = "",
        string postfix = "",
        int limit = -1,
        string truncated = "...",
        Func<T, string> transform = null
    )
    {
        var buffer = new StringBuilder(prefix);
        var count = 0;

        foreach (var element in enumerable)
        {
            if (++count > 1)
            {
                buffer.Append(separator);
            }

            if (limit < 0 || count <= limit)
            {
                buffer.Append(transform != null ? transform.Invoke(element) : element.ToString());
            }
            else
            {
                break;
            }
        }

        if (limit >= 0 && count > limit)
        {
            buffer.Append(truncated);
        }

        buffer.Append(postfix);

        return buffer.ToString();
    }
}
