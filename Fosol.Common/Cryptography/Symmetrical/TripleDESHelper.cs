using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Cryptography
{
    public sealed class TripleDESHelper
        : SymmetricalFactory
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public TripleDESHelper()
            : base(new TripleDESCryptoServiceProvider())
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
