using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Formatters.Keywords
{
    /// <summary>
    /// A StaticKeyword will not change, it is simply static text within a formatted string value.
    /// </summary>
    public abstract class FormatStaticKeyword
        : FormatKeyword
    {
        #region Variables
        private string _Text;
        #endregion

        #region Properties
        /// <summary>
        /// get - The static text that this Keyword will return.
        /// </summary>
        public string Text
        {
            get
            {
                return _Text;
            }
            protected set
            {
                _Text = value;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a StaticKeyword object.
        /// Remember to populate the Text property in your subclass.
        /// </summary>
        public FormatStaticKeyword()
            : base()
        {
        }

        /// <summary>
        /// Creates a new instance of a StaticKeyword object.
        /// Remember to populate the Text property in your subclass.
        /// </summary>
        /// <param name="text">Original string value that created this keyword.</param>
        public FormatStaticKeyword(string text)
            : base()
        {
            this.Text = text;
        }

        /// <summary>
        /// Creates a new instance of a StaticKeyword object.
        /// Remember to populate the Text property in your subclass.
        /// </summary>
        /// <param name="attributes">StringDictionary of attributes to include with this keyword.</param>
        public FormatStaticKeyword(StringDictionary attributes)
            : base(attributes)
        {
        }

        /// <summary>
        /// Creates a new instance of a StaticKeyword object.
        /// Remember to populate the Text property in your subclass.
        /// </summary>
        /// <param name="value">Original string value that created this keyword.</param>
        /// <param name="attributes">StringDictionary of attributes to include with this keyword.</param>
        public FormatStaticKeyword(string text, StringDictionary attributes)
            : base(attributes)
        {
            this.Text = text;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns a formatted string value to create this keyword.
        /// </summary>
        /// <returns>String value.</returns>
        public override string ToString()
        {
            return this.Text;
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
