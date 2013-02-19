using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Parsers
{
    /// <summary>
    /// A Phrase is a part of a parsed syntax.
    /// A phrase is used within the SimpleParser.
    /// </summary>
    public class Phrase : Fosol.Common.Parsers.IPhrase
    {
        #region Variables
        #endregion

        #region Properties
        /// <summary>
        /// get - The text value of the keyword.
        /// </summary>
        public string Text { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a Phrase object.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Parameter "text" cannot be null.</exception>
        /// <param name="text">The full text value of the keyword.</param>
        public Phrase(string text)
        {
            Validation.Assert.IsNotNull(text, "text");

            this.Text = text;
        }
        #endregion

        #region Methods

        #endregion

        #region Events
        #endregion
    }
}
