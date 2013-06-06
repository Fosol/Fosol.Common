using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Formatters.Keywords
{
    /// <summary>
    /// A TextKeyword is a basic text value that may include parameters.
    /// </summary>
    [FormatKeyword("text")]
    public sealed class TextKeyword
        : FormatStaticKeyword
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a TextKeyword object.
        /// Remember to populate the Text property in your subclass.
        /// </summary>
        /// <param name="text">Original string value that created this keyword.</param>
        public TextKeyword(string text)
            : base(text)
        {
        }
        #endregion

        #region Methods
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
