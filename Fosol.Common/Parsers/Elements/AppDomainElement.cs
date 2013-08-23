using Fosol.Common.Parsers.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Parsers.Elements
{
    /// <summary>
    /// Renders the application domain name.
    /// </summary>
    [Element("appDomain")]
    public sealed class AppDomainElement
        : DynamicElement
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Methods
        /// <summary>
        /// Renders the current application domain name.
        /// </summary>
        /// <param name="data">Information object containing data for the keyword.</param>
        /// <returns>The current application domain name.</returns>
        public override string Render(object data)
        {
            return AppDomain.CurrentDomain.FriendlyName;
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
