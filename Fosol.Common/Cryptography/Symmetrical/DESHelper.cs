using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Cryptography
{
    public sealed class DESHelper
        : SymmetricalFactory
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public DESHelper()
            : base(new DESCryptoServiceProvider())
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
