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
    public abstract class SymmetricalFactory
        : CryptographyFactory
    {
        #region Variables
        private SymmetricAlgorithm _Algorithm;
        #endregion

        #region Properties
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a SymmetricalFactory object.
        /// </summary>
        /// <param name="algorithm">SymmetricAlgorithm object.</param>
        public SymmetricalFactory(SymmetricAlgorithm algorithm)
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
        public override byte[] Encrypt(byte[] data, string key, byte[] salt = null)
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
        /// <param name="keySize">Size in bytes of the key.</param>
        /// <param name="ivSize">Size in bytes of the initialization vector.</param>
        /// <returns>Encrypted data.</returns>
        public override byte[] Encrypt(byte[] data, DeriveBytes generator, int keySize = 32, int ivSize = 16)
        {
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(data, "data");
            Fosol.Common.Validation.Assert.IsNotNull(generator, "generator");

            return Encrypt(data, generator.GetBytes(keySize), generator.GetBytes(ivSize));
        }

        /// <summary>
        /// Encrypt data.
        /// </summary>
        /// <param name="data">Data to be encrypted.</param>
        /// <param name="key">Algorithm key.</param>
        /// <param name="iv">Algorithm initialization vector.</param>
        /// <returns>Encrypted data.</returns>
        public override byte[] Encrypt(byte[] data, byte[] key, byte[] iv)
        {
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(data, "data");
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(key, "key");
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(iv, "iv");

            var legal_key_sizes = _Algorithm.LegalKeySizes;
            var valid_key_size = false;
            foreach (var ks in legal_key_sizes)
            {
                if (key.Length >= ks.MinSize && key.Length <= ks.MaxSize)
                {
                    valid_key_size = true;
                    break;
                }
            }
            Fosol.Common.Validation.Assert.IsTrue(valid_key_size, "key.Length", "Parameter 'key' length is not a legal key size.");

            var legal_block_sizes = _Algorithm.LegalBlockSizes;
            var valid_iv_size = false;
            foreach (var bs in legal_block_sizes)
            {
                // Apparently a legal IV size is the same as a BlockSize divided by 8.
                if (iv.Length >= (bs.MinSize / 8) && iv.Length <= (bs.MaxSize / 8))
                {
                    valid_iv_size = true;
                    break;
                }
            }
            Fosol.Common.Validation.Assert.IsTrue(valid_iv_size, "iv.Length", "Parameter 'iv' length is not a legal initialization vector size.");

            MemoryStream memory_stream = null;
            CryptoStream crypto_stream = null;

            try
            {
                _Algorithm.Key = key;
                _Algorithm.IV = iv;

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
        public override byte[] Decrypt(byte[] data, string key, byte[] salt = null)
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
        /// <param name="keySize">Size in bytes of the key.</param>
        /// <param name="ivSize">Size in bytes of the initialization vector.</param>
        /// <returns>Decrypted data.</returns>
        public override byte[] Decrypt(byte[] data, DeriveBytes generator, int keySize = 32, int ivSize = 16)
        {
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(data, "data");
            Fosol.Common.Validation.Assert.IsNotNull(generator, "generator");

            return Encrypt(data, generator.GetBytes(keySize), generator.GetBytes(ivSize));
        }

        /// <summary>
        /// Decrypt the data.
        /// </summary>
        /// <param name="data">Data to be decrypted.</param>
        /// <param name="key">Algorithm key.</param>
        /// <param name="iv">Algorithm initialization vector.</param>
        /// <returns>Decrypted data.</returns>
        public override byte[] Decrypt(byte[] data, byte[] key, byte[] iv)
        {
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(data, "data");
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(key, "key");
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(iv, "iv");

            var legal_key_sizes = _Algorithm.LegalKeySizes;
            var valid_key_size = false;
            foreach (var ks in legal_key_sizes)
            {
                if (key.Length >= ks.MinSize && key.Length <= ks.MaxSize)
                {
                    valid_key_size = true;
                    break;
                }
            }
            Fosol.Common.Validation.Assert.IsTrue(valid_key_size, "key.Length", "Parameter 'key' length is not a legal key size.");

            var legal_block_sizes = _Algorithm.LegalBlockSizes;
            var valid_iv_size = false;
            foreach (var bs in legal_block_sizes)
            {
                // Apparently a legal IV size is the same as a BlockSize divided by 8.
                // http://msdn.microsoft.com/en-us/library/system.security.cryptography.symmetricalgorithm.iv(v=vs.110).aspx
                if (iv.Length >= (bs.MinSize / 8) && iv.Length <= (bs.MaxSize / 8))
                {
                    valid_iv_size = true;
                    break;
                }
            }
            Fosol.Common.Validation.Assert.IsTrue(valid_iv_size, "iv.Length", "Parameter 'iv' length is not a legal initialization vector size.");

            MemoryStream stream = null;
            CryptoStream crypto_stream = null;

            try
            {
                _Algorithm.Key = key;
                _Algorithm.IV = iv;

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
        public override void Dispose()
        {
            _Algorithm.Clear();
            _Algorithm.Dispose();
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
