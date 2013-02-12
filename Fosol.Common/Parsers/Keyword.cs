using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Parsers
{
    public class Keyword : Phrase, IPhrase
    {
        #region Variables
        private const char __Parameter_Delimiter = '?';
        #endregion

        #region Properties
        /// <summary>
        /// get - The name to identify this specific keyword.
        /// </summary>
        public string Name { get; private set; }

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
            var param_start = text.IndexOf(__Parameter_Delimiter);

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
