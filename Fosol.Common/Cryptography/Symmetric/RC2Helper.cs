using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Cryptography
{
    public sealed class RC2Helper
        : SymmetricFactory
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public RC2Helper()
            : base(new RC2CryptoServiceProvider())
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
