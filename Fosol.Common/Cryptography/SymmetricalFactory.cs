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
        protected SymmetricAlgorithm Algorithm
        {
            get { return _Algorithm; }
        }
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
        /// Use this method if you want the initialization vector to be appended to the encrypted data.
        /// </summary>
        /// <param name="data">Data to be encrypted.</param>
        /// <param name="password">Password used to encrypt data.</param>
        /// <param name="salt">Salt to use for encrypting data.</param>
        /// <returns>Encrypted data.</returns>
        public override byte[] Encrypt(byte[] data, string password, byte[] salt)
        {
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(password, "password");
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(salt, "salt");
            Fosol.Common.Validation.Assert.MinRange(salt.Length, 8, "Parameter 'salt' must be at least 8 bytes.");

            return Encrypt(data, new Rfc2898DeriveBytes(password, salt));
        }

        /// <summary>
        /// Encrypt the data.
        /// Uses Rfc2898DeriveBytes object to generate an algorithm hash.
        /// Use this method if you want to retain the initialization vector yourself.
        /// </summary>
        /// <param name="data">Data to be encrypted.</param>
        /// <param name="password">Password used to encrypt data.</param>
        /// <param name="iv">Algorithm initialization vector.</param>
        /// <param name="keySize">Size in bytes of the key.</param>
        /// <param name="ivSize">Size in bytes of the initialization vector.</param>
        /// <returns>Encrypted data.</returns>
        public override byte[] Encrypt(byte[] data, string password, byte[] salt, out byte[] iv, int keySize = 32, int ivSize = 16)
        {
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(data, "data");
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(password, "password");
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(salt, "salt");

            var generator = new Rfc2898DeriveBytes(password, salt);
            var key = generator.GetBytes(keySize);
            iv = generator.GetBytes(ivSize);

            return Encrypt(data, key, iv);
        }

        /// <summary>
        /// Encrypt the data.
        /// Initializeation vector is appended to the end of the data.
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

            var key = generator.GetBytes(keySize);
            var iv = generator.GetBytes(ivSize);

            var encrypted_data = Encrypt(data, key, iv);
            return encrypted_data.Concat(iv).ToArray();
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

            Fosol.Common.Validation.Assert.IsTrue(_Algorithm.ValidKeySize(key.Length * 8), "key", "Parameter 'key' size is invalid.");

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

            try
            {
                _Algorithm.Key = key;
                _Algorithm.IV = iv;
                var encryptor = _Algorithm.CreateEncryptor();

                using (var memory_stream = new MemoryStream())
                {
                    using (var crypto_stream = new CryptoStream(memory_stream, encryptor, CryptoStreamMode.Write))
                    {
                        crypto_stream.Write(data, 0, data.Length);
                    }
                    return memory_stream.ToArray();
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Decrypt the data.
        /// Uses Rfc2898DeriveBytes object to generate an algorithm hash.
        /// Use this method if the initialization vector has been appended to the encrypted data.
        /// </summary>
        /// <param name="data">Data to be decrypted.</param>
        /// <param name="password">Password used to decrypt data.</param>
        /// <param name="salt">Salt to use for decrypting data.</param>
        /// <returns>Decrypted data.</returns>
        public override byte[] Decrypt(byte[] data, string password, byte[] salt)
        {
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(password, "password");
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(salt, "salt");
            Fosol.Common.Validation.Assert.MinRange(salt.Length, 8, "Parameter 'salt' must be at least 8 bytes.");

            return Decrypt(data, new Rfc2898DeriveBytes(password, salt));
        }

        /// <summary>
        /// Decrypt the data.
        /// Uses Rfc2898DeriveBytes object to generate an algorithm hash.
        /// Use this method if you are managing the initialization vector and it has not been appended to the encrypted data.
        /// </summary>
        /// <param name="data">Data to be decrypted.</param>
        /// <param name="password">Password used to decrypt data.</param>
        /// <param name="salt">Salt to use for decrypting data.</param>
        /// <param name="iv">Algorithm initialization vector.</param>
        /// <param name="keySize">Size in bytes of the key.</param>
        /// <returns>Decrypted data.</returns>
        public override byte[] Decrypt(byte[] data, string password, byte[] salt, byte[] iv, int keySize = 32)
        {
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(data, "data");
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(password, "password");
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(salt, "salt");
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(iv, "iv");

            var generator = new Rfc2898DeriveBytes(password, salt);
            var key = generator.GetBytes(keySize);
            return Decrypt(data, key, iv);
        }

        /// <summary>
        /// Decrypt the data.
        /// Use this method if the initialization vector has been appended to the encrypted data.
        /// </summary>
        /// <param name="data">Data to be decrypted.  This data must have the IV appended to the end.</param>
        /// <param name="generator">DeriveBytes object used to populate the algorithm.</param>
        /// <param name="keySize">Size in bytes of the key.</param>
        /// <param name="ivSize">Size in bytes of the initialization vector.</param>
        /// <returns>Decrypted data.</returns>
        public override byte[] Decrypt(byte[] data, DeriveBytes generator, int keySize = 32, int ivSize = 16)
        {
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(data, "data");
            Fosol.Common.Validation.Assert.IsNotNull(generator, "generator");

            var key = generator.GetBytes(keySize);

            // Extract the IV from the data.  By default it is appended to the end of the data.
            var iv = new byte[ivSize];
            Array.Copy(data, data.Length - ivSize, iv, 0, ivSize);
            var extracted_data = new byte[data.Length - ivSize];
            Array.Copy(data, extracted_data, extracted_data.Length);

            return Decrypt(extracted_data, key, iv);
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

            Fosol.Common.Validation.Assert.IsTrue(_Algorithm.ValidKeySize(key.Length * 8), "key", "Parameter 'key' size is invalid.");

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

            try
            {
                _Algorithm.Key = key;
                _Algorithm.IV = iv;
                var decryptor = _Algorithm.CreateDecryptor();

                using (var stream = new MemoryStream())
                {
                    using (var crypto_stream = new CryptoStream(stream, decryptor, CryptoStreamMode.Write))
                    {
                        crypto_stream.Write(data, 0, data.Length);
                    }

                    return stream.ToArray();
                }
            }
            catch
            {
                throw;
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
