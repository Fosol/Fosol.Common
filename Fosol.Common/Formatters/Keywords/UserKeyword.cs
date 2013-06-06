using Fosol.Common.Formatters.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Formatters.Keywords
{
    /// <summary>
    /// Renders the current username.
    /// </summary>
    [FormatKeyword("user")]
    public sealed class UserKeyword
        : FormatDynamicKeyword
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Methods
        /// <summary>
        /// Outputs the username and domain.
        /// </summary>
        /// <param name="data">Information object containing data for the keyword.</param>
        /// <returns>The username currently logged in.</returns>
        public override string Render(object data)
        {
            return Environment.UserDomainName + "\\" + Environment.UserName;
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
