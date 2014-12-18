using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Cryptography
{
    /// <summary>
    /// CyrptographyFactory provides a simple inteface to encrypt data with any SymmetricAlgorithm objects.
    /// </summary>
    public abstract class CryptographyFactoryOld
        : IDisposable, ICryptography
    {
        #region Variables
        private bool _IsSingleUse;
        #endregion

        #region Properties
        /// <summary>
        /// get - Whether this instance can only perform a single encryption/decryption before being reset.
        /// </summary>
        public bool IsSingleUse
        {
            get { return _IsSingleUse; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a CryptographyFactory.
        /// </summary>
        /// <param name="isSingleUse">Whether the encrypt and decrypt methods are single use.</param>
        public CryptographyFactoryOld(bool isSingleUse = true)
        {
            _IsSingleUse = isSingleUse;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Encrypt the data.
        /// </summary>
        /// <param name="data">Data to be encrypted.</param>
        /// <returns>Encrypted data.</returns>
        public abstract byte[] Encrypt(byte[] data);

        /// <summary>
        /// Decrypt the data.
        /// </summary>
        /// <param name="data">Data to be decrypted.</param>
        /// <returns>Decrypted data.</returns>
        public abstract byte[] Decrypt(byte[] data);

        /// <summary>
        /// Clear that SymmetricAlgorithm.
        /// </summary>
        public abstract void Dispose();
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
