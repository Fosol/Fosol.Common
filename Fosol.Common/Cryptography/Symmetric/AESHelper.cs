using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Cryptography
{
    /// <summary>
    /// AESHelper class uses the AesManaged symmetric algorithm for encryption.
    /// Generally Aes is used for data privacy.
    /// </summary>
    public sealed class AESHelper
        : SymmetricFactory
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of an AESHelper class.
        /// </summary>
        public AESHelper()
            : base(new AesManaged())
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
