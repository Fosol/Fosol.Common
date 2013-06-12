using System;
namespace Fosol.Common.Parsers
{
    /// <summary>
    /// Denotes a phrase found within a parsed text value.
    /// This interface is used by the SimpleParser class.
    /// </summary>
    public interface ISentencePart
    {
        /// <summary>
        /// get - The text value representing the phrase.
        /// </summary>
        string Text { get; }
    }
}
