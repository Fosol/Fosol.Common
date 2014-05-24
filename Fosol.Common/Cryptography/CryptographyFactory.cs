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
    public class CryptographyFactory
        : IDisposable
    {
        #region Variables
        private SymmetricAlgorithm _Algorithm;
        #endregion

        #region Properties
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a CryptographyFactory object.
        /// </summary>
        /// <param name="algorithm">SymmetricAlgorithm object.</param>
        public CryptographyFactory(SymmetricAlgorithm algorithm)
        {
            Fosol.Common.Validation.Assert.IsNotNull(algorithm, "algorithm");
            _Algorithm = algorithm;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Encrypt the data.
        /// Uses Rfc2898DeriveBytes object to generate an algorithm hash.
        /// </summary>
        /// <param name="data">Data to be encrypted.</param>
        /// <param name="key">Key used to encrypt data.</param>
        /// <param name="salt">Salt to use for encrypting data.</param>
        /// <returns>Encrypted data.</returns>
        public byte[] Encrypt(byte[] data, string key, byte[] salt = null)
        {
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(key, "key");
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(salt, "salt");
            Fosol.Common.Validation.Assert.MinRange(salt.Length, 8, "Parameter 'salt' must be at least 8 bytes.");

            return Encrypt(data, new Rfc2898DeriveBytes(key, salt));
        }

        /// <summary>
        /// Encrypt the data.
        /// </summary>
        /// <param name="data">Data to be encrypted.</param>
        /// <param name="generator">DeriveBytes object used to populate the algorithm.</param>
        /// <returns></returns>
        public byte[] Encrypt(byte[] data, DeriveBytes generator)
        {
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(data, "data");
            Fosol.Common.Validation.Assert.IsNotNull(generator, "generator");

            MemoryStream memory_stream = null;
            CryptoStream crypto_stream = null;

            try
            {
                _Algorithm.Key = generator.GetBytes(32);
                _Algorithm.IV = generator.GetBytes(16);

                memory_stream = new MemoryStream();
                crypto_stream = new CryptoStream(memory_stream, _Algorithm.CreateEncryptor(), CryptoStreamMode.Write);

                crypto_stream.Write(data, 0, data.Length);
                return memory_stream.ToArray();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (crypto_stream != null)
                    crypto_stream.Close();

                if (memory_stream != null)
                    memory_stream.Close();
            }
        }

        /// <summary>
        /// Decrypt the data.
        /// Uses Rfc2898DeriveBytes object to generate an algorithm hash.
        /// </summary>
        /// <param name="data">Data to be decrypted.</param>
        /// <param name="key">Key used to decrypt data.</param>
        /// <param name="salt">Salt to use for decrypting data.</param>
        /// <returns>Decrypted data.</returns>
        public byte[] Decrypt(byte[] data, string key, byte[] salt = null)
        {
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(key, "key");
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(salt, "salt");
            Fosol.Common.Validation.Assert.MinRange(salt.Length, 8, "Parameter 'salt' must be at least 8 bytes.");

            return Decrypt(data, new Rfc2898DeriveBytes(key, salt));
        }

        /// <summary>
        /// Decrypt the data.
        /// </summary>
        /// <param name="data">Data to be decrypted.</param>
        /// <param name="generator">DeriveBytes object used to populate the algorithm.</param>
        /// <returns>Decrypted data.</returns>
        public byte[] Decrypt(byte[] data, DeriveBytes generator)
        {
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(data, "data");
            Fosol.Common.Validation.Assert.IsNotNull(generator, "generator");

            MemoryStream stream = null;
            CryptoStream crypto_stream = null;

            try
            {
                _Algorithm.Key = generator.GetBytes(32);
                _Algorithm.IV = generator.GetBytes(16);

                stream = new MemoryStream();
                crypto_stream = new CryptoStream(stream, _Algorithm.CreateDecryptor(), CryptoStreamMode.Write);

                crypto_stream.Write(data, 0, data.Length);
                crypto_stream.FlushFinalBlock();

                var result = stream.ToArray();

                return result;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (crypto_stream != null)
                    crypto_stream.Close();

                if (stream != null)
                    stream.Close();
            }
        }

        /// <summary>
        /// Clear that SymmetricAlgorithm.
        /// </summary>
        public void Dispose()
        {
            _Algorithm.Clear();
        }

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
            new RNGCryptoServiceProvider().GetBytes(array);
            return array;
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
