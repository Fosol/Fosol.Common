using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Parsers
{
    /// <summary>
    /// A keyword is a special phrase found within a parsed text value.
    /// The Keywords is used by the SimpleParser to represent a special phrase within the text.
    /// A keyword can be used to dynamically replace values within text.
    /// 
    /// A keyword can be expressed within text with special start and end boundaries (i.e. ${keyword}).
    /// A keyword can contain parameters to help define its dynamic behaviour (i.e. ${keyword?parm1=value&param2=value}).
    /// Start and end boundaries are defined within the SimpleParser.
    /// </summary>
    public class Keyword : Phrase, IPhrase
    {
        #region Variables
        private const char _Parameter_Delimiter = '?';
        #endregion

        #region Properties
        /// <summary>
        /// get - The name to identify this specific keyword.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// get - A collection of parameters included in the keyword.
        /// </summary>
        public NameValueCollection Params { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a Keyword object.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Parameter "text" cannot be null.</exception>
        /// <exception cref="System.ArgumentException">Parameter "text" must start and end with boundary values.</exception>
        /// <param name="text">The text value of the keyword.</param>
        public Keyword(string text)
            : base(text)
        {
            var param_start = text.IndexOf(_Parameter_Delimiter);

            if (param_start == -1)
                this.Name = text;
            else
            {
                this.Name = text.Substring(0, param_start);

                this.Params = System.Web.HttpUtility.ParseQueryString(text.Substring(param_start + 1));
            }
        }
        #endregion

        #region Methods

        #endregion

        #region Events
        #endregion
    }
}
