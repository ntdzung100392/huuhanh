using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Lucene.Net.Analysis;
using Umbraco.Core;

namespace HHCoApps.CMSWeb.Helpers
{
    public static class StringExtension
    {
        public static string ReplaceDashAndEmptySpace(this string inputString)
        {
            return inputString.Replace(" ", string.Empty).Replace("-", string.Empty);
        }

        public static string ReplaceEmptySpaceByDash(this string inputString)
        {
            return inputString.Replace(" ", "-");
        }

        public static IEnumerable<string> ReplaceStopWords(this IEnumerable<string> filterValues)
        {
            var escapedFilterValues = new List<string>();
            foreach (var value in filterValues)
            {
                var escapedValue = value.Replace(" ", string.Empty);
                if (StopAnalyzer.ENGLISH_STOP_WORDS_SET.Contains(value, StringComparer.OrdinalIgnoreCase))
                {
                    escapedFilterValues.Add($"s_{escapedValue}");
                }
                else
                {
                    escapedFilterValues.Add(escapedValue);
                }
            }

            return escapedFilterValues;
        }

        public static string NormalizeFilterValue(this string inputString)
        {
            if (string.IsNullOrEmpty(inputString))
                return string.Empty;

            var regexSpecialCharacters = new Dictionary<string, string>
            {
                {"!", "ExclamationMark"}
            };

            return inputString.ReplaceMany(regexSpecialCharacters).Replace(" ", string.Empty);
        }

        public static bool IsValidEmailAddress(this string inputString)
        {
            if (string.IsNullOrEmpty(inputString))
                return false;
            
            var regex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            return regex.IsMatch(inputString) && !inputString.EndsWith(".");
        }

        public static string Slugify(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            value = value.ToLowerInvariant();
            var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(value);
            value = Encoding.ASCII.GetString(bytes);
            value = Regex.Replace(value, @"\s", "-", RegexOptions.Compiled);
            value = Regex.Replace(value, @"[^a-z0-9\s-_]", "", RegexOptions.Compiled);
            value = value.Trim('-', '_');
            value = Regex.Replace(value, @"([-_]){2,}", "$1", RegexOptions.Compiled);

            return value;
        }
    }
}