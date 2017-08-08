using System;
using System.Collections.Generic;
using System.Linq;
using TitanBot.Formatting;
using TT2Bot.Helpers;
using TT2Bot.Models.General;

namespace TT2Bot.Overrides
{
    class Formatter : ValueFormatter
    {
        
        public static readonly FormatType Scientific = 1;
        private static readonly string Alphabet = "abcdefghijklmnopqrstuvwxyz";
        private static readonly string[] PostFixes = new string[] { "", "K", "M", "B", "T" };

        public Formatter()
        {
            Register(Scientific);

            Add<int>(i => BeautifyMetric(i), (Scientific, i => BeautifyScientific(i)));
            Add<long>(l => BeautifyMetric(l), (Scientific, i => BeautifyScientific(i)));
            Add<Percentage>(p => p.ToString());
            Add<double>(BeautifyMetric, (Scientific, BeautifyScientific));
            Add<TimeSpan>(Beautify);
        }

        public string Beautify(TimeSpan value)
        {
            var s = $"{value.Seconds} {(value.Seconds != 1 ? "seconds" : "second")}";
            var m = $"{value.Minutes} {(value.Minutes != 1 ? "minutes" : "minute")}";
            var h = $"{value.Hours} {(value.Hours != 1 ? "hours" : "hour")}";
            var d = $"{value.Days} {(value.Days != 1 ? "days" : "day")}";

            var sections = new List<string>();
            if (value.Days != 0)
                sections.Add(d);
            if (value.Hours != 0)
                sections.Add(h);
            if (value.Minutes != 0)
                sections.Add(m);
            if (value.Seconds != 0 || sections.Count == 0)
                sections.Add(s);

            return sections.Join(", ", " and ");
        }

        private string BeautifyScientific(double value)
        {
            if (double.IsNaN(value))
                return "NaN";
            if (value == 0)
                return "0";
            var sign = value < 0 ? "-" : "";
            if (double.IsInfinity(value))
                return sign + "∞";

            value = Math.Abs(value);
            var exponent = (int)Math.Floor(Math.Log10(value));
            double mantissa;
            string postfix;
            if (exponent / 3 >= 0 && exponent / 3 < PostFixes.Length)
            {
                mantissa = value / (Math.Pow(10, (exponent / 3) * 3));
                postfix = PostFixes[exponent / 3];
            }
            else
            {
                mantissa = value / Math.Pow(10, exponent);
                postfix = "+e" + exponent;
            }
            return sign + mantissa.ToString("0.###") + postfix;
        }

        private string BeautifyMetric(double value)
        {
            if (double.IsNaN(value))
                return "NaN";
            if (value == 0)
                return "0";
            var sign = value < 0 ? "-" : "";
            if (double.IsInfinity(value))
                return sign + "∞";

            value = Math.Abs(value);
            var exponent = (int)Math.Floor(Math.Log10(value));
            double mantissa;
            string postfix;
            if (exponent / 3 >= 0 && exponent / 3 < PostFixes.Length)
            {
                mantissa = value / (Math.Pow(10, (exponent / 3) * 3));
                postfix = PostFixes[exponent / 3];
            }
            else
            {
                mantissa = value / (Math.Pow(10, (exponent / 3) * 3));
                postfix = Alphabet[(exponent / 3 - (PostFixes.Length)) / 26].ToString() +
                          Alphabet[(exponent / 3 - (PostFixes.Length)) % 26].ToString();
            }
            return sign + mantissa.ToString("0.###") + postfix;            
        }
    }
}
