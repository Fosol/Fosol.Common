using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fosol.Common.Helpers
{
    /// <summary>
    /// Useful string methods.
    /// </summary>
    public static class StringHelper
    {
        #region Methods
        /// <summary>
        /// Escapes all the control characters within the specified string value.
        /// </summary>
        /// <param name="value">String value to change.</param>
        /// <param name="options">Regex control options.</param>
        /// <returns>New string value with escaped control characters.</returns>
        public static string EscapeRegexControlCharacters(string value, RegexOptions options = RegexOptions.None)
        {
            return Regex.Replace(value, @"[\\\[\]\*\.\(\)\?\+\{\}\^\$\<\>]", new MatchEvaluator(EscapeRegexControlCharactersEvaluator), options);
        }

        /// <summary>
        /// EscapeRegexControlCharacter MatchEvaluator method.
        /// Escapes the matched control characters.
        /// </summary>
        /// <param name="match">Match found that should be escaped.</param>
        /// <returns>New string value with escaped value.</returns>
        private static string EscapeRegexControlCharactersEvaluator(Match match)
        {
            return @"\" + match.Value;
        }

        /// <summary>
        /// Counts the number of words in the specified text.
        /// </summary>
        /// <param name="text">Text value to count.</param>
        /// <returns>Number of words in the specified text.</returns>
        public static int WordCount(string text)
        {
            var words = text.Split(new char[] { '.', '?', '!', ' ', ';', ':', ',', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);

            return words.Length;
        }
        #endregion
    }
}
