using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Parsers
{
    /// <summary>
    /// Provides a simple and efficient way to parse a string into a collection of Phrases.
    /// Generally speaking this class is more efficient than a regular expression.
    /// 
    /// Any string values that are defined with a start and end boundary (i.e. ${keyword}) are captured as Keywords.
    /// Any string values outside of keyword boundaries are captured as a simple Phrase.
    /// 
    /// The Parser() method returns a collection of Phrases, which can be aggregated to recreate the original text.
    /// </summary>
    public class SimpleParser
    {
        #region Variables
        private readonly string _EndBoundaryEscape;
        #endregion

        #region Properties
        /// <summary>
        /// get - A keyword start syntax boundary (i.e. "${", without double-quotes).
        /// </summary>
        public string StartBoundary { get; private set; }

        /// <summary>
        /// get - A keyword end syntax boundary (i.e. "}", without double-quotes).
        /// </summary>
        public string EndBoundary { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new KeywordParser object.
        /// Defaults StartBoundary = "${".
        /// Defaults EndBoundary = "}".
        /// </summary>
        public SimpleParser()
            : this ("${", "}")
        {
        }

        /// <summary>
        /// Creates a new KeywordParser object.
        /// Initializes with specified keyword boundary values.
        /// </summary>
        /// <param name="startBoundary">Keyword start boundary syntax.</param>
        /// <param name="endBoundary">Keyword end boundary syntax.</param>
        public SimpleParser(string startBoundary, string endBoundary)
        {
            Validation.Parameter.AssertIsNotNullOrEmpty(startBoundary, "startBoundary");
            Validation.Parameter.AssertIsNotNullOrEmpty(endBoundary, "endBoundary");

            this.StartBoundary = startBoundary;
            this.EndBoundary = endBoundary;

            // Set the escape boundaries so that they don't have to be done every time.
            _EndBoundaryEscape = endBoundary + endBoundary;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Parse the text for all the keywords.
        /// String values outside keyword boundaries are considered keywords of KeywordType = Literal.
        /// </summary>
        /// <example>
        /// text = "${datetime?format={0:u}}&utc=true}${literal?text=some ${ text} - ${message} - ${newline}"
        /// keyword1 = ${datetime?format={0:u}&utc=true}
        /// keyword2 = ${literal?text=some ${ text}
        /// phrase_3 = " - "
        /// keyword4 = ${message}
        /// phrase_5 = " - "
        /// keyword6 = ${newline}
        /// </example>
        /// <param name="text">Text value you want to parse into a collection of phrases.</param>
        /// <param name="startIndex">Index position to start parsing the text at.</param>
        /// <returns>Collection of phrases that make up this text.</returns>
        public List<IPhrase> Parse(string text, int startIndex = 0)
        {
            var keywords = new List<IPhrase>();

            var length = text.Length - 1;
            while (startIndex < length)
            {
                keywords.AddRange(ParseFirst(text, startIndex, out startIndex));
            }

            return keywords;
        }

        /// <summary>
        /// Find the first phrase within the text starting at the startIndex position.
        /// If a keyword isn't found it will return a single phrase containing the whole text.
        /// If a keywords is found but does not being at the startIndex position it will return a phrase and the keyword.
        /// </summary>
        /// <example>
        /// var format = "some text ${datetime}";
        /// var end = -1;
        /// var keywords = ParseFirst(format, 0, out end);
        /// // keywords[0] = "some text ";
        /// // keywords[1] = "${datetime}";
        /// </example>
        /// <exception cref="System.ArgumentNullException">Parameter "text" cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameter "startIndex" must be within a valid range.</exception>
        /// <param name="text">Text to search for keywords.</param>
        /// <param name="startIndex">Index position to start search at within the text.</param>
        /// <param name="endIndex">The next position after the discovered phrase/keyword.</param>
        /// <returns>One or two phrases (depending on text).</returns>
        protected List<IPhrase> ParseFirst(string text, int startIndex, out int endIndex)
        {
            Validation.Parameter.AssertIsNotNull(text, "text");
            Validation.Parameter.AssertRange(startIndex, 0, text.Length - 1, "startIndex");

            var keywords = new List<IPhrase>();
            var start = ParseStart(text, startIndex);

            // A keyword was not found, so return the text as a literal keyword.
            if (start == -1)
            {
                endIndex = text.Length - 1;
                keywords.Add(CreatePhrase(text.Substring(startIndex, text.Length - startIndex)));
                return keywords;
            }

            var end = ParseEnd(text, start);

            // The closing syntax was not found, so return the text as a literal keyword.
            if (end == -1)
            {
                endIndex = text.Length - 1;
                keywords.Add(CreatePhrase(text.Substring(startIndex, text.Length - startIndex)));
                return keywords;
            }

            // There is a literal string before the keyword.
            if (start > startIndex)
            {
                keywords.Add(CreatePhrase(text.Substring(startIndex, start - startIndex)));
            }

            endIndex = end + 1;
            keywords.Add(CreateKeyword(text.Substring(start, endIndex - start)));
            return keywords;
        }

        /// <summary>
        /// Search for the index of a start boundary of a keyword.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Parameter "text" cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameter "startIndex" must be within a valid range.</exception>
        /// <param name="text">Text to search through for the start boundary.</param>
        /// <param name="startIndex">Index position to start searching at.</param>
        /// <returns>Index position of the start boundary, or -1 if not found.</returns>
        protected int ParseStart(string text, int startIndex = 0)
        {
            Validation.Parameter.AssertIsNotNull(text, "text");
            Validation.Parameter.AssertRange(startIndex, 0, text.Length - 1, "startIndex");

            return text.IndexOf(this.StartBoundary, startIndex);
        }

        /// <summary>
        /// Search for the index of an end boundary of a keyword.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Parameter "text" cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameter "startIndex" must be within a valid range.</exception>
        /// <param name="text">Text to search through for the end boundary.</param>
        /// <param name="startIndex">Index position to start searching at.</param>
        /// <returns>Index position of the start of the end boundary, or -1 if not found.</returns>
        protected int ParseEnd(string text, int startIndex = 0)
        {
            Validation.Parameter.AssertIsNotNull(text, "text");
            Validation.Parameter.AssertRange(startIndex, 0, text.Length - 1, "startIndex");

            var end = text.IndexOf(this.EndBoundary, startIndex);

            if (end == -1)
                return text.Length - 1;

            // Make sure this end wasn't escaped.
            // Escaping is done by doubling the syntax.
            var length = _EndBoundaryEscape.Length;
            if (end + length <= text.Length
                && text.Substring(end + this.EndBoundary.Length, this.EndBoundary.Length) == this.EndBoundary)
                return ParseEnd(text, end + length);

            return end;
        }

        /// <summary>
        /// Creates a new Keyword and initializes it.
        /// Correct escaped boundary values.
        /// </summary>
        /// <param name="text">Keyword text value.</param>
        /// <returns>A new Keyword object.</returns>
        protected Keyword CreateKeyword(string text)
        {
            // Strip the boundary syntax from the text.
            var start = this.StartBoundary.Length;
            var length = text.Length - start - this.EndBoundary.Length;
            // Updated escaped boundaries.
            return new Keyword(text.Substring(start, length).Replace(_EndBoundaryEscape, this.EndBoundary));
        }

        /// <summary>
        /// Creates a new Phrase and initializes it.
        /// </summary>
        /// <param name="text">Phrase text value.</param>
        /// <returns>A new Phrase object.</returns>
        protected Phrase CreatePhrase(string text)
        {
            return new Phrase(text.Replace(_EndBoundaryEscape, this.EndBoundary));
        }

        /// <summary>
        /// Aggregate the phrases together into an original text value.
        /// </summary>
        /// <param name="phrases">Collection of phrases.</param>
        /// <returns>String value with the aggregate of the phrases.</returns>
        public string Aggregate(IEnumerable<IPhrase> phrases)
        {
            return phrases
                .Select(p => 
                    (p is Keyword) ? 
                    string.Format("{0}{1}{2}", this.StartBoundary, p.Text, this.EndBoundary) : 
                    p.Text.Replace(this.EndBoundary, _EndBoundaryEscape))
                .Aggregate((a, b) => a + b);
        }
        #endregion

        #region Events
        #endregion
    }
}
