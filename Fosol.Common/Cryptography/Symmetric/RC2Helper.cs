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
        /// <summary>
        /// Creates a new instance of RC2Helper class.
        /// </summary>
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
