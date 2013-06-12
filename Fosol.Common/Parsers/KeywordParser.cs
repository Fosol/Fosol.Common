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
    public class KeywordParser
    {
        #region Variables
        private readonly string _StartBoundaryEscape;
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
        public KeywordParser()
            : this ("${", "}")
        {
        }

        /// <summary>
        /// Creates a new KeywordParser object.
        /// Initializes with specified keyword boundary values.
        /// </summary>
        /// <param name="startBoundary">Keyword start boundary syntax.</param>
        /// <param name="endBoundary">Keyword end boundary syntax.</param>
        public KeywordParser(string startBoundary, string endBoundary)
        {
            Validation.Assert.IsNotNullOrEmpty(startBoundary, "startBoundary");
            Validation.Assert.IsNotNullOrEmpty(endBoundary, "endBoundary");

            this.StartBoundary = startBoundary;
            this.EndBoundary = endBoundary;

            _StartBoundaryEscape = startBoundary + startBoundary;
            _EndBoundaryEscape = endBoundary + endBoundary;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Parse the text for all the keywords.
        /// String values outside keyword boundaries are considered keywords of KeywordType = Literal.
        /// If the text is null or empty it will return an empty collection of Phrases.
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
        public List<ISentencePart> Parse(string text, int startIndex = 0)
        {
            var keywords = new List<ISentencePart>();

            if (string.IsNullOrEmpty(text))
                return keywords;

            var length = text.Length - 1;
            while (startIndex <= length)
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
        /// // SentencePart[0] = "some text ";
        /// // SentencePart[1] = "${datetime}";
        /// </example>
        /// <exception cref="System.ArgumentNullException">Parameter "text" cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameter "startIndex" must be within a valid range.</exception>
        /// <param name="text">Text to search for keywords.</param>
        /// <param name="startIndex">Index position to start search at within the text.</param>
        /// <param name="endIndex">The next position after the discovered phrase/keyword.</param>
        /// <returns>One or two phrases (depending on text).</returns>
        protected List<ISentencePart> ParseFirst(string text, int startIndex, out int endIndex)
        {
            Validation.Assert.IsNotNull(text, "text");
            Validation.Assert.Range(startIndex, 0, text.Length - 1, "startIndex");

            var keywords = new List<ISentencePart>();
            var start = FindStartBoundary(text, startIndex);

            // A keyword was not found, so return the text as a literal keyword.
            if (start == -1)
            {
                endIndex = text.Length;
                keywords.Add(CreateText(text.Substring(startIndex, text.Length - startIndex)));
                return keywords;
            }

            var end = FindEndBoundary(text, start);

            // The closing syntax was not found, so return the text as a literal keyword.
            if (end == -1)
            {
                endIndex = text.Length;
                keywords.Add(CreateText(text.Substring(startIndex, text.Length - startIndex)));
                return keywords;
            }

            // There is a literal string before the keyword.
            if (start > startIndex)
            {
                keywords.Add(CreateText(text.Substring(startIndex, start - startIndex)));
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
        protected int FindStartBoundary(string text, int startIndex = 0)
        {
            Validation.Assert.IsNotNull(text, "text");
            Validation.Assert.Range(startIndex, 0, text.Length - 1, "startIndex");

            var start = text.IndexOf(this.StartBoundary, startIndex);

            // Make sure this start boundary hasn't been escaped.
            if (start != -1
                && text.Length > (start + 1)
                && text.Substring(start + 1, this.StartBoundary.Length) == this.StartBoundary)
            {
                if (text.Length > (start + 2))
                    return FindStartBoundary(text, start + 2);
                else
                    return -1;
            }

            return start;
        }

        /// <summary>
        /// Search for the index of an end boundary of a keyword.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Parameter "text" cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameter "startIndex" must be within a valid range.</exception>
        /// <param name="text">Text to search through for the end boundary.</param>
        /// <param name="startIndex">Index position to start searching at.</param>
        /// <returns>Index position of the start of the end boundary, or -1 if not found.</returns>
        protected int FindEndBoundary(string text, int startIndex = 0)
        {
            Validation.Assert.IsNotNull(text, "text");
            Validation.Assert.Range(startIndex, 0, text.Length - 1, "startIndex");

            var end = text.IndexOf(this.EndBoundary, startIndex);

            // There was no end boundary.
            if (end == -1)
                return text.Length - 1;

            // An end boundary was found but we need to check to see if it belongs to the start boundary.
            // We do this by backtracking and looking for a start boundary.
            var range = end - startIndex;
            if (range > 0)
            {
                var start = FindStartBoundary(text.Substring(startIndex + 1, range));

                // We found start boundary, which means our end boundary may belong to it.
                if (start != -1)
                {
                    return FindEndBoundary(text, end + 1);
                }
            }

            // Make sure this end wasn't escaped.
            // Escaping is done by doubling the syntax.
            if (text.Length > (end + 1)
                && text.Substring(end + 1, this.EndBoundary.Length) == this.EndBoundary)
            {
                if (text.Length > (end + 2))
                    return FindEndBoundary(text, end + 2);
                else
                    return text.Length - 1;
            }

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
            return new Keyword(text.Substring(start, length).Replace(_StartBoundaryEscape, this.StartBoundary).Replace(_EndBoundaryEscape, this.EndBoundary));
        }

        /// <summary>
        /// Creates a new Phrase and initializes it.
        /// </summary>
        /// <param name="text">Phrase text value.</param>
        /// <returns>A new Phrase object.</returns>
        protected SentencePart CreateText(string text)
        {
            return new SentencePart(text.Replace(_StartBoundaryEscape, this.StartBoundary).Replace(_EndBoundaryEscape, this.EndBoundary));
        }

        /// <summary>
        /// Aggregate the phrases together into an original text value.
        /// </summary>
        /// <param name="parts">Collection of phrases.</param>
        /// <returns>String value with the aggregate of the phrases.</returns>
        public string Aggregate(IEnumerable<ISentencePart> parts)
        {
            return parts
                .Select(p => 
                    (p is Keyword) ? 
                    string.Format("{0}{1}{2}", this.StartBoundary, p.Text, this.EndBoundary) :
                    p.Text.Replace(_StartBoundaryEscape, this.StartBoundary).Replace(this.EndBoundary, _EndBoundaryEscape))
                .Aggregate((a, b) => a + b);
        }
        #endregion

        #region Events
        #endregion
    }
}
