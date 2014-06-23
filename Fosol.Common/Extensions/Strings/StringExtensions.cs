using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
#if !WINDOWS_APP
using System.Web;
#endif

namespace Fosol.Common.Extensions.Strings
{
    /// <summary>
    /// Extension methods for strings.
    /// </summary>
    public static class StringExtensions
    {
        #region Methods
        /// <summary>
        /// Creates a new string an uppercases every single word based on the provided 'text' value.
        /// This method will uppercase ever word after a space.
        /// </summary>
        /// <param name="text">String value to use to create the new result.</param>
        /// <returns>A new string using title case.</returns>
        public static string ToTitleCase(this string text)
        {
            return text.ToTitleCase(new[] { ' ' });
        }

        /// <summary>
        /// Creates a new string an uppercases every single word based on the provided 'text' value.
        /// </summary>
        /// <param name="text">String value to use to create the new result.</param>
        /// <param name="separator">An array of Char which indicate new words.</param>
        /// <returns>A new string using title case.</returns>
        public static string ToTitleCase(this string text, char[] separator)
        {
            var result = new List<char>();
            var new_word = true;
            foreach (var c in text)
            {
                var is_separator = separator.Contains(c);
                if (!is_separator && new_word)
                {
                    result.Add(Char.ToUpper(c));
                    new_word = false;
                }
                else result.Add(Char.ToLower(c));

                if (is_separator) new_word = true;
            }

            return new String(result.ToArray());
        }

        /// <summary>
        /// Indent the text value the specified number of times.
        /// </summary>
        /// <param name="text">Text value to be indented</param>
        /// <param name="quantity">Number of tabs to indent the text.</param>
        /// <param name="tab">Tab value to use when indenting.  Default value is "\t".</param>
        /// <returns>Indented text value.</returns>
        public static string Indent(this string text, int quantity, string tab = "\t")
        {
            if (quantity <= 0)
                return text;

            var indent_array = new string[quantity];

            for (var i = 0; i < quantity; i++) indent_array[i] = tab;

            return indent_array.Aggregate((a, b) => a + b) + text;
        }

        /// <summary>
        /// Converts the string value into an array of byte.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "value" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "value" cannot be null.</exception>
        /// <param name="value">String value to convert.</param>
        /// <param name="encoding">
        ///     Encoding object to use when converting to an array of byte.
        /// </param>
        /// <returns>Array of byte.</returns>
        public static byte[] ToByteArray(this string value, Encoding encoding = null)
        {
            Validation.Assert.IsNotNullOrEmpty(value, "value");
#if WINDOWS_APP
            var default_encoding = Encoding.UTF8;
#else
            var default_encoding = Encoding.Default;
#endif
            Initialization.Assert.IsNotDefault<Encoding>(ref encoding, default_encoding);
            return encoding.GetBytes(value);
        }

        /// <summary>
        /// Provides a way to find and replace a value within a string with option StringComparison parameters.
        /// This method is faster than a RegEx.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameters "original", and "pattern" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameters "original", "pattern" and "replacement" cannot be null.</exception>
        /// <param name="original">Original string to search.</param>
        /// <param name="pattern">Pattern string to look for within the original.</param>
        /// <param name="replacement">Replacement string to use when a pattern is found in the original.</param>
        /// <param name="comparisonType">StringComparison parameter to apply to the search and replace.</param>
        /// <returns>Replacement string value.</returns>
        public static string Replace(this string original, string pattern, string replacement, StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            return Replace(original, pattern, replacement, comparisonType, -1);
        }

        /// <summary>
        /// Provides a way to find and replace a value within a string with option StringComparison parameters.
        /// This method is faster than a RegEx.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameters "original", and "pattern" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameters "original", "pattern" and "replacement" cannot be null.</exception>
        /// <param name="original">Original string to search.</param>
        /// <param name="pattern">Pattern string to look for within the original.</param>
        /// <param name="replacement">Replacement string to use when a pattern is found in the original.</param>
        /// <param name="comparisonType">StringComparison parameter to apply to the search and replace.</param>
        /// <param name="stringBuilderInitialSize">
        ///     Initial size of the StringBuilder.  A large initial size will increase performance slightly but also allocate more memory. 
        ///     Also remember when specifying a large size that the result of a large string could theoretically be a very tiny or even empty string
        /// </param>
        /// <returns>Replacement string value.</returns>
        public static string Replace(this string original, string pattern, string replacement, StringComparison comparisonType = StringComparison.CurrentCulture, int stringBuilderInitialSize = -1)
        {
            Validation.Assert.IsNotNullOrEmpty(original, "original");
            Validation.Assert.IsNotNullOrEmpty(pattern, "pattern");
            Validation.Assert.IsNotNull(replacement, "replacement");

            if (original == null)
                return null;

            if (String.IsNullOrEmpty(pattern))
                return original;

            int pos_current = 0;
            int len_pattern = pattern.Length;
            int index_next = original.IndexOf(pattern, comparisonType);
            StringBuilder result = new StringBuilder(stringBuilderInitialSize < 0 ? Math.Min(4096, original.Length) : stringBuilderInitialSize);
            while (index_next >= 0)
            {
                result.Append(original, pos_current, index_next - pos_current);
                result.Append(replacement);
                pos_current = index_next + len_pattern;
                index_next = original.IndexOf(pattern, pos_current, comparisonType);
            }

            result.Append(original, pos_current, original.Length - pos_current);
            return result.ToString();
        }

        /// <summary>
        /// Returns a byte from a Hex value.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Parameter "value" cannot be null.</exception>
        /// <param name="value">Hex value.</param>
        /// <returns>Byte which is from the hex value.</returns>
        public static byte HexToByte(this string value)
        {
            return Extensions.Numbers.NumberExtensions.HexToByte(value);
        }

        /// <summary> 
        /// Removes control characters and other non-UTF-8 characters 
        /// </summary> 
        /// <exception cref="System.ArgumentNullException">Parameter "value" cannot be null.</exception>
        /// <param name="value">The string to process.</param> 
        /// <returns>A string with no control characters or entities above 0x00FD.</returns> 
        public static string RemoveInvalidUTF8Characters(this string value)
        {
            Validation.Assert.IsNotNull(value, "value");

            StringBuilder newString = new StringBuilder();
            char ch;

            for (int i = 0; i < value.Length; i++)
            {
                ch = value[i];
                // remove any characters outside the valid UTF-8 range as well as all control characters 
                // except tabs and new lines 
                if ((ch == 0x9)
                    || (ch == 0xA)
                    || (ch == 0xD)
                    || ((ch >= 0x20) && (ch <= 0xD7FF))
                    || ((ch >= 0xE000) && (ch <= 0xFFFD))
                    //|| ((ch >= 0x10000) && (ch <= 0x10FFFF))
                    )
                {
                    newString.Append(ch);
                }
                else
                {
                    // Replace invalid character with a space
                    newString.Append(' ');
                    //throw new Exception("Invalid Character " + ch);
                }
            }
            return newString.ToString();
        }

        /// <summary>
        /// Deals with characters outside of the allowable range and encodes them.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Parameter "value" cannot be null.</exception>
        /// <param name="value">String value to HTML encode.</param>
        /// <returns>HTML encoded string.</returns>
        public static string HtmlEncode(this string value)
        {
            Validation.Assert.IsNotNull(value, "value");

            return string.Join("", value.ToCharArray().Select(c => (int)c > 127 ? "&#" + (int)c + ";" : c.ToString()).ToArray());
        }

        /// <summary>
        /// Removes all HTML tags from the string value.
        /// This method also HtmlDecodes the string value.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Parameter "value" cannot be null.</exception>
        /// <param name="value">String to parse HTML from</param>
        /// <returns>String value without HTML tags</returns>
        public static string RemoveHtml(this string value)
        {
            Validation.Assert.IsNotNull(value, "value");
            return System.Text.RegularExpressions.Regex.Replace(System.Net.WebUtility.HtmlDecode(value), @"<(.|\n)*?>", string.Empty);
        }

        /// <summary>
        /// Replaces a tag pair with the specified text.  First tag element is removed and the end tag is replaced.
        /// This method strips out those tags that have no content within them.
        /// This method also HtmlDecodes the string value.
        /// Use this method to replace 'P' with '\n'
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "htmlTag" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "value", and "htmlTag" cannot be null.</exception>
        /// <param name="value">String to parse</param>
        /// <param name="htmlTagName">HTML tag name (i.e. P).</param>
        /// <param name="text">Replacement text</param>
        /// <returns>String value with tag replaced with specified text</returns>
        public static string ReplaceTagsWith(this string value, string htmlTagName, string text)
        {
            Validation.Assert.IsNotNull(value, "value");
            Validation.Assert.IsNotNullOrEmpty(htmlTagName, "htmlTag");

            return Regex.Replace(
                Regex.Replace(
                    Regex.Replace(
                        Regex.Replace(
                            System.Net.WebUtility.HtmlDecode(value),
                            string.Format("<{0}>\\s?</{0}>", htmlTagName, RegexOptions.IgnoreCase), 
                            string.Empty),
                        @"<" + htmlTagName + ">", 
                        string.Empty, 
                        RegexOptions.IgnoreCase),
                    @"</" + htmlTagName + ">", 
                    text, 
                    RegexOptions.IgnoreCase),
                @"<" + htmlTagName + "/>", 
                text, 
                RegexOptions.IgnoreCase);
        }
        
#if !WINDOWS_PHONE
        /// <summary>
        /// Splits a string with the specified delimiter into an array of string.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "delimiter" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameters "value", and "delimiter" cannot be null.</exception>
        /// <param name="value">String value to split.</param>
        /// <param name="delimiter">Delimiter to use for splitting.</param>
        /// <param name="ignoreCase">Whether or not to ignore the case.  Default = false.</param>
        /// <returns>String array of split values.</returns>
        public static string[] Split(this string value, string delimiter, bool ignoreCase = false)
        {
            Validation.Assert.IsNotNull(value, "value");
            Validation.Assert.IsNotNullOrEmpty(delimiter, "delimiter");

            int start = 0;
#if WINDOWS_APP
            var values = new List<string>();
#else
            var values = new ArrayList();
#endif

            // Loop through each character and look for the delimiter.
            for (int i = 0; i < value.Length - 1; i++)
            {
                // Delimiter was found
#if WINDOWS_APP
                var string_comparison = StringComparison.CurrentCultureIgnoreCase;
#else
                var string_comparison = StringComparison.InvariantCultureIgnoreCase;
#endif
                if (string.Compare(value.Substring(i, delimiter.Length), delimiter, string_comparison) == 0)
                {
                    values.Add(value.Substring(start, i - start));
                    start = i + 1;
                    i += delimiter.Length;
                }
            }

            // Delimiter wasn't in the value, so return the string as a single array value.
            if (start == 0)
                return new string[] { value };
            // Add the last part of the value to the array.
            else if (start < value.Length)
                values.Add(value.Substring(start, value.Length - start));

            var results = new string[values.Count];
            values.CopyTo(results);
            return results;
        }
        
        /// <summary>
        /// Parses a simple string into a KeyValuePair based on the delimiter.
        /// The string value must contain only a single delimiter.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "delimiter" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameters "value", and "delimiter" cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameter "value" cannot have more than one delimiter in it.</exception>
        /// <param name="value">String value to convert into a KeyValuePair object.</param>
        /// <param name="delimiter">Delimiter that separates each key and value.</param>
        /// <param name="ignoreCase">Whether the case should be ignored when comparing the delimiters.</param>
        /// <returns>KeyValuePair object.</returns>
        public static KeyValuePair<string, string> ToKeyValuePair(this string value, string delimiter, bool ignoreCase = false)
        {
            Validation.Assert.IsNotNull(value, "value");
            Validation.Assert.IsNotNullOrEmpty(delimiter, "delimiter");

            var values = value.Split(delimiter, ignoreCase);

            Validation.Assert.MaxRange(values.Length, 2, "value", String.Format(Resources.Multilingual.Exception_Too_Many_Pairs, "value"));

            return new KeyValuePair<string, string>(values[0], values.Length == 2 ? values[1] : null);
        }

        /// <summary>
        /// Splits a string into KeyValuePairs based on the delimiters.
        /// </summary>
        /// <param name="value">String value to split into a list of KeyValuePair objects.</param>
        /// <param name="keyDelimiter">Delimiter that separates each KeyValuePair.</param>
        /// <param name="valueDelimiter">Delimiter that separates each key and value.</param>
        /// <param name="ignoreCase">Whether the case should be ignored when comparing the delimiters.</param>
        /// <returns>List of KeyValuePair objects.</returns>
        public static List<KeyValuePair<string, string>> SplitToKeyValuePair(this string value, string keyDelimiter, string valueDelimiter, bool ignoreCase = false)
        {
            var values = value.Split(keyDelimiter, ignoreCase);
            var results = new List<KeyValuePair<string, string>>();

            foreach (var key in values)
                results.Add(key.ToKeyValuePair(valueDelimiter, ignoreCase));

            return results;
        }
#endif

        #region Numbers
        #region decimal
        /// <summary>
        /// Attempts to convert the string value into a number.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "value" cannot be empty.</exception>
        /// <param name="value">Value to parse into a number.</param>
        /// <returns>Value as an number.</returns>
        public static decimal ToDecimal(this string value)
        {
            Validation.Assert.IsNotNullOrEmpty(value, "value");
            decimal result = 0;
            if (!decimal.TryParse(value, out result))
                throw new ArgumentOutOfRangeException("value");
            return result;
        }

        /// <summary>
        /// Attempts to convert the array of values into an array of numbers.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "values" cannot be empty.</exception>
        /// <param name="values">Array of values.</param>
        /// <returns>Array of numbers.</returns>
        public static decimal[] ToDecimalArray(this string[] values)
        {
            Validation.Assert.IsNotNullOrEmpty(values, "values");
            return values.Select(v => v.ToDecimal()).ToArray();
        }

        /// <summary>
        /// Attempts to convert the collection of values into an collection of numbers.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "values" cannot be empty.</exception>
        /// <param name="values">Collection of values.</param>
        /// <returns>Collection of numbers.</returns>
        public static IEnumerable<decimal> ToDecimalArray(this IEnumerable<string> values)
        {
            Validation.Assert.IsNotNullOrEmpty(values, "values");
            return values.Select(v => v.ToDecimal()); ;
        }
        #endregion

        #region double
        /// <summary>
        /// Attempts to convert the string value into a number.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "value" cannot be empty.</exception>
        /// <param name="value">Value to parse into a number.</param>
        /// <returns>Value as an number.</returns>
        public static double ToDouble(this string value)
        {
            Validation.Assert.IsNotNullOrEmpty(value, "value");
            double result = 0;
            if (!double.TryParse(value, out result))
                throw new ArgumentOutOfRangeException("value");
            return result;
        }

        /// <summary>
        /// Attempts to convert the array of values into an array of numbers.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "values" cannot be empty.</exception>
        /// <param name="values">Array of values.</param>
        /// <returns>Array of numbers.</returns>
        public static double[] ToDoubleArray(this string[] values)
        {
            Validation.Assert.IsNotNullOrEmpty(values, "values");
            return values.Select(v => v.ToDouble()).ToArray();
        }

        /// <summary>
        /// Attempts to convert the collection of values into an collection of numbers.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "values" cannot be empty.</exception>
        /// <param name="values">Collection of values.</param>
        /// <returns>Collection of numbers.</returns>
        public static IEnumerable<double> ToDoubleArray(this IEnumerable<string> values)
        {
            Validation.Assert.IsNotNullOrEmpty(values, "values");
            return values.Select(v => v.ToDouble()); ;
        }
        #endregion

        #region float
        /// <summary>
        /// Attempts to convert the string value into a number.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "value" cannot be empty.</exception>
        /// <param name="value">Value to parse into a number.</param>
        /// <returns>Value as an number.</returns>
        public static float ToFloat(this string value)
        {
            Validation.Assert.IsNotNullOrEmpty(value, "value");
            float result = 0;
            if (!float.TryParse(value, out result))
                throw new ArgumentOutOfRangeException("value");
            return result;
        }

        /// <summary>
        /// Attempts to convert the array of values into an array of numbers.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "values" cannot be empty.</exception>
        /// <param name="values">Array of values.</param>
        /// <returns>Array of numbers.</returns>
        public static float[] ToFloatArray(this string[] values)
        {
            Validation.Assert.IsNotNullOrEmpty(values, "values");
            return values.Select(v => v.ToFloat()).ToArray();
        }

        /// <summary>
        /// Attempts to convert the collection of values into an collection of numbers.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "values" cannot be empty.</exception>
        /// <param name="values">Collection of values.</param>
        /// <returns>Collection of numbers.</returns>
        public static IEnumerable<float> ToFloatArray(this IEnumerable<string> values)
        {
            Validation.Assert.IsNotNullOrEmpty(values, "values");
            return values.Select(v => v.ToFloat()); ;
        }
        #endregion

        #region int
        /// <summary>
        /// Attempts to convert the string value into a number.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "value" cannot be empty.</exception>
        /// <param name="value">Value to parse into a number.</param>
        /// <returns>Value as an number.</returns>
        public static int ToInt(this string value)
        {
            Validation.Assert.IsNotNullOrEmpty(value, "value");
            int result = 0;
            if (!int.TryParse(value, out result))
                throw new ArgumentOutOfRangeException("value");
            return result;
        }

        /// <summary>
        /// Attempts to convert the array of values into an array of numbers.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "values" cannot be empty.</exception>
        /// <param name="values">Array of values.</param>
        /// <returns>Array of numbers.</returns>
        public static int[] ToIntArray(this string[] values)
        {
            Validation.Assert.IsNotNullOrEmpty(values, "values");
            return values.Select(v => v.ToInt()).ToArray();
        }

        /// <summary>
        /// Attempts to convert the collection of values into an collection of numbers.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "values" cannot be empty.</exception>
        /// <param name="values">Collection of values.</param>
        /// <returns>Collection of numbers.</returns>
        public static IEnumerable<int> ToIntArray(this IEnumerable<string> values)
        {
            Validation.Assert.IsNotNullOrEmpty(values, "values");
            return values.Select(v => v.ToInt()); ;
        }
        #endregion

        #region long
        /// <summary>
        /// Attempts to convert the string value into a number.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "value" cannot be empty.</exception>
        /// <param name="value">Value to parse into a number.</param>
        /// <returns>Value as an number.</returns>
        public static long ToLong(this string value)
        {
            Validation.Assert.IsNotNullOrEmpty(value, "value");
            long result = 0;
            if (!long.TryParse(value, out result))
                throw new ArgumentOutOfRangeException("value");
            return result;
        }

        /// <summary>
        /// Attempts to convert the array of values into an array of numbers.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "values" cannot be empty.</exception>
        /// <param name="values">Array of values.</param>
        /// <returns>Array of numbers.</returns>
        public static long[] ToLongArray(this string[] values)
        {
            Validation.Assert.IsNotNullOrEmpty(values, "values");
            return values.Select(v => v.ToLong()).ToArray();
        }

        /// <summary>
        /// Attempts to convert the collection of values into an collection of numbers.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "values" cannot be empty.</exception>
        /// <param name="values">Collection of values.</param>
        /// <returns>Collection of numbers.</returns>
        public static IEnumerable<long> ToLongArray(this IEnumerable<string> values)
        {
            Validation.Assert.IsNotNullOrEmpty(values, "values");
            return values.Select(v => v.ToLong()); ;
        }
        #endregion

        #region short
        /// <summary>
        /// Attempts to convert the string value into a number.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "value" cannot be empty.</exception>
        /// <param name="value">Value to parse into a number.</param>
        /// <returns>Value as an number.</returns>
        public static short ToShort(this string value)
        {
            Validation.Assert.IsNotNullOrEmpty(value, "value");
            short result = 0;
            if (!short.TryParse(value, out result))
                throw new ArgumentOutOfRangeException("value");
            return result;
        }

        /// <summary>
        /// Attempts to convert the array of values into an array of numbers.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "values" cannot be empty.</exception>
        /// <param name="values">Array of values.</param>
        /// <returns>Array of numbers.</returns>
        public static short[] ToShortArray(this string[] values)
        {
            Validation.Assert.IsNotNullOrEmpty(values, "values");
            return values.Select(v => v.ToShort()).ToArray();
        }

        /// <summary>
        /// Attempts to convert the collection of values into an collection of numbers.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "values" cannot be empty.</exception>
        /// <param name="values">Collection of values.</param>
        /// <returns>Collection of numbers.</returns>
        public static IEnumerable<short> ToShortArray(this IEnumerable<string> values)
        {
            Validation.Assert.IsNotNullOrEmpty(values, "values");
            return values.Select(v => v.ToShort()); ;
        }
        #endregion
        #endregion

        #region WP7
#if WINDOWS_PHONE
        /// <summary>
        /// Parses a Uri string for the query parameters.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Parameter "uri" cannot be null.</exception>
        /// <param name="uri">Uri string.</param>
        /// <returns>Dictionary query parameters.</returns>
        public static Dictionary<string, string> ParseQueryString(this string uri)
        {
            Validation.Parameter.IsNotNull(uri, "uri");

            uri = HttpUtility.UrlDecode(uri);

            var parameters = new Dictionary<string, string>();

            // remove anything other than query string from url
            if (uri.Contains("?"))
                uri = uri.Substring(uri.IndexOf('?') + 1);

            foreach (string vp in Regex.Split(uri, "&"))
            {
                string[] singlePair = Regex.Split(vp, "=");
                if (singlePair.Length == 2)
                    parameters.Add(singlePair[0], singlePair[1]);
                else
                    // only one key with no value specified in query string
                    parameters.Add(singlePair[0], string.Empty);
            }

            return parameters;
        }
#endif
        #endregion

        #endregion
    }
}
