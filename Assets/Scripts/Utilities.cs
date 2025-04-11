using System;
using System.Collections;
using UnityEngine;
using System.Text;

class Utilities
{
    public static IEnumerator WaitForAFrameThen(Action callback)
    {
        yield return new WaitForEndOfFrame();
        callback();
    }
}

public static class StringUtilities
{
    public static string ToKebabCase(string str)
    {
        if (string.IsNullOrEmpty(str)) return str;

        var builder = new StringBuilder();
        bool previousWasUpper = false;

        for (int i = 0; i < str.Length; i++)
        {
            char c = str[i];
            
            // Skip any non-letter/digit chars
            if (!char.IsLetterOrDigit(c)) continue;

            // Handle casing
            if (char.IsUpper(c))
            {
                // Add hyphen if:
                // 1. Not the first char AND
                // 2. Previous char wasn't upper (avoid splitting acronyms) OR next char is lower
                if (i > 0 && (!previousWasUpper || (i + 1 < str.Length && char.IsLower(str[i + 1]))))
                {
                    builder.Append('-');
                }
                builder.Append(char.ToLower(c));
                previousWasUpper = true;
            }
            else
            {
                builder.Append(char.ToLower(c));
                previousWasUpper = false;
            }
        }

        return builder.ToString();
    }
}