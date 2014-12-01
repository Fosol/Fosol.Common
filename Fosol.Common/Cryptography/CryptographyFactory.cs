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
    public abstract class CryptographyFactory
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
        public CryptographyFactory(bool isSingleUse = true)
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

        /// <summary>
        /// Generates a random salt value to use.
        /// Uses RNGCryptoServiceProvider formula to create the salt.
        /// Use Convert.ToBase64String() method to convert the byte array to a string.
        /// </summary>
        /// <param name="size">Size of the salt.</param>
        /// <returns>A new salt value.</returns>
        public static byte[] GenerateSalt(int size = 16)
        {
            var array = new byte[size];
            var generator = new RNGCryptoServiceProvider();
            generator.GetBytes(array);
            return array;
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
