﻿using Fosol.Common.Extensions.Bytes;
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
    /// SymmetricFactory class provides a base for encrypting and decrypting data with a symmetric algorithm.
    /// </summary>
    public sealed class SymmetricFactory
        : CryptographyFactory
    {
        #region Variables
        private SymmetricAlgorithm _Algorithm;
        private byte[] _Key;
        private byte[] _IV;
        private bool _NeedKeyReset = true;
        private bool _NeedIVReset = true;
        #endregion

        #region Properties
        /// <summary>
        /// set - The key that will be used to encrypt and decrypt.
        /// </summary>
        public byte[] Key
        {
            set
            {
                Fosol.Common.Validation.Assert.IsNotNull(value, "Key");

                byte[] val = new byte[value.Length];
                Buffer.BlockCopy(value, 0, val, 0, value.Length);

                if (!this.ValidKeySize(val))
                    throw new ArgumentOutOfRangeException("The key value specified is not a valid size.");

                // Only reset the value if it has changed.
                if (!val.IsMatch(_Key, 0))
                {
                    _Key = val;
                    _NeedKeyReset = false;
                }
            }
        }

        /// <summary>
        /// set - The initialization vector the symmetric algorithm will use.
        /// </summary>
        public byte[] IV
        {
            set
            {
                Fosol.Common.Validation.Assert.IsNotNull(value, "IV");

                byte[] val = new byte[value.Length];
                Buffer.BlockCopy(value, 0, val, 0, value.Length);

                if (!this.ValidIVSize(val))
                    throw new ArgumentOutOfRangeException("The IV value specified is not a valid size.");

                // Only reset the value if it has changed.
                if (!val.IsMatch(_IV, 0))
                {
                    _IV = val;
                    _NeedIVReset = false;
                }
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a SymmetricFactory object.
        /// 
        /// Prebuild SymmetricAlgorithms;
        /// Aes
        /// AesManaged
        /// AesCryptoServiceProvider
        /// DES
        /// DESCryptoServiceProvider
        /// RC2
        /// RC2CryptoServiceProvider
        /// Rijndael
        /// RijndaelManaged
        /// TripleDES
        /// TripleDESCryptoServiceProvider
        /// </summary>
        /// <param name="algorithm">SymmetricAlgorithm object.</param>
        /// <param name="isSingleUse">Whether the encrypt and decrypt methods can be used only once before the key and IV values being reset.</param>
        public SymmetricFactory(SymmetricAlgorithm algorithm, bool isSingleUse = true)
            : base(isSingleUse)
        {
            Fosol.Common.Validation.Assert.IsNotNull(algorithm, "algorithm");
            _Algorithm = algorithm;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Encrypt the data.
        /// This method uses the Key and IV properties.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">If this SymmetricFactory is single use you will only be able to call this method once before reseting those values.</exception>
        /// <param name="data">Data to be encrypted.</param>
        /// <returns>Encrypted data.</returns>
        public override byte[] Encrypt(byte[] data)
        {
            if (this.IsSingleUse && (_NeedKeyReset || _NeedIVReset))
                throw new InvalidOperationException("This SymmetricFactory instance is single use.  Reset the Key and IV values before encrypting.");

            var result = this.Encrypt(data, _Key, _IV);
            _NeedKeyReset = true;
            return result;
        }

        /// <summary>
        /// Decrypt the data.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">If this SymmetricFactory is single use you will only be able to call this method once before reseting those values.</exception>
        /// <param name="data">Data to be decrypted.</param>
        /// <returns>Decrypted data.</returns>
        public override byte[] Decrypt(byte[] data)
        {
            if (this.IsSingleUse && (_NeedKeyReset || _NeedIVReset))
                throw new InvalidOperationException("This SymmetricFactory instance is single use.  Reset the Key and IV values before decrypting.");

            var result = this.Decrypt(data, _Key, _IV);
            _NeedIVReset = true;
            return result;
        }

        /// <summary>
        /// Encrypt the data.
        /// Uses Rfc2898DeriveBytes object to generate an algorithm hash key.
        /// </summary>
        /// <param name="data">Data to be encrypted.</param>
        /// <param name="password">Password used to encrypt data.</param>
        /// <param name="salt">Salt to use for encrypting data.</param>
        /// <param name="keySize">Size in bytes of the key.</param>
        /// <param name="ivSize">Size in bytes of the initialization vector.</param>
        /// <returns>Encrypted data.</returns>
        public byte[] Encrypt(byte[] data, string password, byte[] salt, int keySize = 32, int ivSize = 16)
        {
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(password, "password");
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(salt, "salt");
            Fosol.Common.Validation.Assert.MinRange(salt.Length, 8, "Parameter 'salt' must be at least 8 bytes.");

            return Encrypt(data, new Rfc2898DeriveBytes(password, salt), keySize, ivSize);
        }

        /// <summary>
        /// Encrypt the data.
        /// </summary>
        /// <param name="data">Data to be encrypted.</param>
        /// <param name="generator">DeriveBytes object used to populate the algorithm.</param>
        /// <param name="keySize">Size in bytes of the key.</param>
        /// <param name="ivSize">Size in bytes of the initialization vector.</param>
        /// <returns>Encrypted data.</returns>
        public byte[] Encrypt(byte[] data, DeriveBytes generator, int keySize = 32, int ivSize = 16)
        {
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(data, "data");
            Fosol.Common.Validation.Assert.IsNotNull(generator, "generator");

            var key = generator.GetBytes(keySize);
            var iv = generator.GetBytes(ivSize);

            return Encrypt(data, key, iv);
        }

        /// <summary>
        /// Encrypt data.
        /// </summary>
        /// <param name="data">Data to be encrypted.</param>
        /// <param name="key">Algorithm key.</param>
        /// <param name="iv">Algorithm initialization vector.</param>
        /// <returns>Encrypted data.</returns>
        public byte[] Encrypt(byte[] data, byte[] key, byte[] iv)
        {
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(data, "data");
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(key, "key");
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(iv, "iv");

            Fosol.Common.Validation.Assert.IsTrue(this.ValidKeySize(key), "key", "Parameter 'key' size is invalid.");
            Fosol.Common.Validation.Assert.IsTrue(this.ValidIVSize(iv), "iv.Length", "Parameter 'iv' length is not a legal initialization vector size.");

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
        /// Validate the IV size for the cryptography algorithm.
        /// </summary>
        /// <param name="iv">Initialization vector value.</param>
        /// <returns>True if the IV is a valid size.</returns>
        public bool ValidIVSize(byte[] iv)
        {
            return SymmetricFactory.ValidIVSize(_Algorithm, iv);
        }

        /// <summary>
        /// Validate the IV size for the specified SymmetricAlgorithm.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Parameters 'algorithm' and 'iv' cannot be null.</exception>
        /// <param name="algorithm">SymmetricAlgorithm object.</param>
        /// <param name="iv">Initialization vector value.</param>
        /// <returns>True if the IV is a valid size.</returns>
        public static bool ValidIVSize(SymmetricAlgorithm algorithm, byte[] iv)
        {
            Fosol.Common.Validation.Assert.IsNotNull(algorithm, "algorithm");
            Fosol.Common.Validation.Assert.IsNotNull(iv, "iv");

            var legal_block_sizes = algorithm.LegalBlockSizes;
            foreach (var bs in legal_block_sizes)
            {
                // Apparently a legal IV size is the same as a BlockSize divided by 8.
                if (iv.Length >= (bs.MinSize / 8) && iv.Length <= (bs.MaxSize / 8))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Validate the key size for the cryptography algorithm.
        /// </summary>
        /// <param name="key">Key value.</param>
        /// <returns>True if the key is a valid size.</returns>
        public bool ValidKeySize(byte[] key)
        {
            return SymmetricFactory.ValidKeySize(_Algorithm, key);
        }

        /// <summary>
        /// Validate the key size for the SymmetricAlgorithm.
        /// </summary>
        /// <param name="algorithm">SymmetricAlgorithm object.</param>
        /// <param name="key">Key value.</param>
        /// <returns>True if the key is a valid size.</returns>
        public static bool ValidKeySize(SymmetricAlgorithm algorithm, byte[] key)
        {
            Fosol.Common.Validation.Assert.IsNotNull(algorithm, "algorithm");
            Fosol.Common.Validation.Assert.IsNotNull(key, "key");
            return algorithm.ValidKeySize(key.Length * 8);
        }

        /// <summary>
        /// Decrypt the data.
        /// Uses Rfc2898DeriveBytes object to generate an algorithm hash.
        /// </summary>
        /// <param name="data">Data to be decrypted.</param>
        /// <param name="password">Password used to decrypt data.</param>
        /// <param name="salt">Salt to use for decrypting data.</param>
        /// <param name="keySize">Size in bytes of the key.</param>
        /// <param name="ivSize">Size in bytes of the initialization vector.</param>
        /// <returns>Decrypted data.</returns>
        public byte[] Decrypt(byte[] data, string password, byte[] salt, int keySize = 32, int ivSize = 16)
        {
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(password, "password");
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(salt, "salt");
            Fosol.Common.Validation.Assert.MinRange(salt.Length, 8, "Parameter 'salt' must be at least 8 bytes.");

            return Decrypt(data, new Rfc2898DeriveBytes(password, salt), keySize, ivSize);
        }

        /// <summary>
        /// Decrypt the data.
        /// </summary>
        /// <param name="data">Data to be decrypted.  This data must have the IV appended to the end.</param>
        /// <param name="generator">DeriveBytes object used to populate the algorithm.</param>
        /// <param name="keySize">Size in bytes of the key.</param>
        /// <param name="ivSize">Size in bytes of the initialization vector.</param>
        /// <returns>Decrypted data.</returns>
        public byte[] Decrypt(byte[] data, DeriveBytes generator, int keySize = 32, int ivSize = 16)
        {
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(data, "data");
            Fosol.Common.Validation.Assert.IsNotNull(generator, "generator");

            var key = generator.GetBytes(keySize);
            var iv = generator.GetBytes(ivSize);

            return Decrypt(data, key, iv);
        }

        /// <summary>
        /// Decrypt the data.
        /// </summary>
        /// <param name="data">Data to be decrypted.</param>
        /// <param name="key">Algorithm key.</param>
        /// <param name="iv">Algorithm initialization vector.</param>
        /// <returns>Decrypted data.</returns>
        public byte[] Decrypt(byte[] data, byte[] key, byte[] iv)
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
