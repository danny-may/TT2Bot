using System;
using System.Collections.Generic;
using System.Linq;
using TitanBot.Commands;
using TitanBot.Formatting;
using TT2Bot.Models;

namespace TT2Bot.Helpers
{
    static class Extensions
    {
        public static string Join<T>(this IEnumerable<T> values, string separator, string finalSeparator = null)
        {
            finalSeparator = finalSeparator ?? separator;
            var firstElements = values.Take(Math.Max(values.Count() - 1, 0)).ToArray();
            var finalElement = values.Last();
            if (firstElements.Length == 0)
                return finalElement.ToString();
            return string.Join(separator, firstElements) + finalSeparator + finalElement.ToString();
        }

        public static bool Between<TValue, TRange>(this TValue value, TRange minimum, TRange maximum) 
            where TValue : IComparable<TRange> 
            where TRange : IComparable<TRange>
        {
            if (minimum.CompareTo(maximum) == 1)
                (minimum, maximum) = (maximum, minimum);
            return value.CompareTo(minimum) == 1 && value.CompareTo(maximum) == -1;
        }

        public static string Without(this string text, params string[] strings)
        {
            foreach (var str in strings)
                text = text.Replace(str, "");
            return text;
        }
        
    }
}
