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
        : IDisposable
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Methods
        /// <summary>
        /// Encrypt the data.
        /// </summary>
        /// <param name="data">Data to be encrypted.</param>
        /// <param name="password">Password used to encrypt data.</param>
        /// <param name="salt">Salt to use for encrypting data.</param>
        /// <param name="keySize">Size in bytes of the key.</param>
        /// <param name="ivSize">Size in bytes of the initialization vector.</param>
        /// <returns>Encrypted data.</returns>
        public abstract byte[] Encrypt(byte[] data, string password, byte[] salt, int keySize = 32, int ivSize = 16);

        /// <summary>
        /// Encrypt the data.
        /// </summary>
        /// <param name="data">Data to be encrypted.</param>
        /// <param name="generator">DeriveBytes object used to populate the algorithm.</param>
        /// <param name="keySize">Size in bytes of the key.</param>
        /// <param name="ivSize">Size in bytes of the initialization vector.</param>
        /// <returns>Encrypted data.</returns>
        public abstract byte[] Encrypt(byte[] data, DeriveBytes generator, int keySize = 32, int ivSize = 16);

        /// <summary>
        /// Encrypt data.
        /// </summary>
        /// <param name="data">Data to be encrypted.</param>
        /// <param name="key">Algorithm key.</param>
        /// <param name="iv">Algorithm initialization vector.</param>
        /// <returns>Encrypted data.</returns>
        public abstract byte[] Encrypt(byte[] data, byte[] key, byte[] iv);

        /// <summary>
        /// Decrypt the data.
        /// </summary>
        /// <param name="data">Data to be decrypted.</param>
        /// <param name="password">Password used to decrypt data.</param>
        /// <param name="salt">Salt to use for decrypting data.</param>
        /// <param name="keySize">Size in bytes of the key.</param>
        /// <param name="ivSize">Size in bytes of the initialization vector.</param>
        /// <returns>Decrypted data.</returns>
        public abstract byte[] Decrypt(byte[] data, string password, byte[] salt, int keySize = 32, int ivSize = 16);

        /// <summary>
        /// Decrypt the data.
        /// </summary>
        /// <param name="data">Data to be decrypted.</param>
        /// <param name="generator">DeriveBytes object used to populate the algorithm.</param>
        /// <param name="keySize">Size in bytes of the key.</param>
        /// <param name="ivSize">Size in bytes of the initialization vector.</param>
        /// <returns>Decrypted data.</returns>
        public abstract byte[] Decrypt(byte[] data, DeriveBytes generator, int keySize = 32, int ivSize = 16);

        /// <summary>
        /// Decrypt the data.
        /// </summary>
        /// <param name="data">Data to be decrypted.</param>
        /// <param name="key">Algorithm key.</param>
        /// <param name="iv">Algorithm initialization vector.</param>
        /// <returns>Decrypted data.</returns>
        public abstract byte[] Decrypt(byte[] data, byte[] key, byte[] iv);

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
