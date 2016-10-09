using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Klocman.Extensions
{
    public static class StringTools
    {
        #region Fields

        private static readonly char[] InvalidFileNameCharsS = Path.GetInvalidFileNameChars();
        private static readonly char[] InvalidPathCharsS = Path.GetInvalidPathChars();
        private static readonly string[] NewLineCharsS = {Environment.NewLine, "\n", "\r"};

        #endregion Fields

        #region Properties

        public static IEnumerable<char> InvalidFileNameChars => InvalidFileNameCharsS;

        public static IEnumerable<char> InvalidPathChars => InvalidPathCharsS;

        public static IEnumerable<string> NewLineChars => NewLineCharsS;

        #endregion Properties

        #region Methods

        public static string AppendIf(this string value, bool expression, string append)
        {
            return expression ? value + append : value;
        }

        public static bool ContainsAny(this IEnumerable<char> s, IEnumerable<char> items)
        {
            return items.Any(s.Contains);
        }

        public static string GetUniqueName(string baseName, IEnumerable<string> otherItems)
        {
            if (baseName == null)
                throw new ArgumentNullException("baseName");

            var baseNameCounted = baseName.Trim().Normalize();
            if (otherItems == null)
                return baseNameCounted;

            var items = otherItems as string[] ?? otherItems.ToArray();

            for (var i = 0; i < int.MaxValue; i++)
            {
                if (i != 0)
                    baseNameCounted = string.Format("{0}({1})", baseName, i);

                if (!items.Contains(baseNameCounted))
                    return baseNameCounted;
            }
            throw new ArgumentOutOfRangeException("otherItems", "Unique name reached int.MaxValue");
        }

        public static string RemoveNewLines(this string value)
        {
            if (value == null)
                return string.Empty;

            foreach (var str in NewLineChars)
            {
                value = value.Replace(str, string.Empty);
            }
            return value;
        }

        public static string RemoveSpecialCharacters(this string value)
        {
            return value == null ? string.Empty : Regex.Replace(value, @"[^\w ]", string.Empty);
        }

        public static bool StringContainsFilter(string input, string filter)
        {
            var filters = StripAccentsFromString(filter).Split(' ');
            input = StripAccentsFromString(input);
            foreach (var str in filters)
            {
                if (input.IndexOf(str, StringComparison.OrdinalIgnoreCase) == -1)
                    return false;
            }
            return true;
        }

        public static string StripAccentsFromString(string input)
        {
            var tempContentsUnicode = Encoding.GetEncoding(1251).GetBytes(input);
            return Encoding.ASCII.GetString(tempContentsUnicode);
        }

        public static StringFormat ToStringFormat(this ContentAlignment a)
        {
            var cFormat = new StringFormat();
            var lNum = (int) Math.Log((double) a, 2);
            cFormat.LineAlignment = (StringAlignment) (lNum/4);
            cFormat.Alignment = (StringAlignment) (lNum%4);
            cFormat.Trimming = StringTrimming.None;
            return cFormat;
        }

        #endregion Methods
    }
}