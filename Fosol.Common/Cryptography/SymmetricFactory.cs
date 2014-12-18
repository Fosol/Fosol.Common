using Fosol.Common.Cryptography.Extensions;
using System;
using System.Security.Cryptography;

namespace Fosol.Common.Cryptography
{
    /// <summary>
    /// SymmetricFactory sealed class provides a simple interface to encrypt and decrypt data.
    /// </summary>
    public sealed class SymmetricFactory
        : ICryptography
    {
        #region Variables
        private SymmetricAlgorithm _Algorithm;
        #endregion

        #region Properties
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a SymmetricFactory class.
        /// 
        /// Standard SymmetricAlgorithm Types
        /// ---------------------------------
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
        /// <param name="symmetricAlgorithmType">SymmetricAlgorithm type name.</param>
        /// <param name="key">SymmetricAlgorithm key value.</param>
        /// <param name="iv">SymmetricAlgorithm IV value.</param>
        public SymmetricFactory(Type symmetricAlgorithmType, byte[] key, byte[] iv)
        {
            Fosol.Common.Validation.Argument.Assert.IsAssignable(symmetricAlgorithmType, typeof(SymmetricAlgorithm), "symmetricAlgorithmType");
            _Algorithm = Fosol.Common.Helpers.ReflectionHelper.ConstructObject(symmetricAlgorithmType) as SymmetricAlgorithm;

            Fosol.Common.Validation.Argument.Assert.WithinRange(_Algorithm.ValidKeySize(key), "key");
            Fosol.Common.Validation.Argument.Assert.WithinRange(_Algorithm.ValidIVSize(iv), "iv");
            _Algorithm.Key = key;
            _Algorithm.IV = iv;
        }

        /// <summary>
        /// Creates a new instance of a SymmetricFactory class.
        /// 
        /// Standard SymmetricAlgorithm Types
        /// ---------------------------------
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
        /// <param name="symmetricAlgorithmType">SymmetricAlgorithm type name.</param>
        /// <param name="password">Password used to encrypt/decrypt data.</param>
        /// <param name="salt">Salt used to encrypt/decrypt data.</param>
        /// <param name="iterations">Number of types the process should be executed.</param>
        public SymmetricFactory(Type symmetricAlgorithmType, string password, byte[] salt, int iterations = 1000)
        {
            Fosol.Common.Validation.Argument.Assert.IsAssignable(symmetricAlgorithmType, typeof(SymmetricAlgorithm), "symmetricAlgorithmType");
            Fosol.Common.Validation.Argument.Assert.IsNotNullOrWhiteSpace(password, "password");
            Fosol.Common.Validation.Argument.Assert.IsNotNullOrEmpty(salt, "salt");
            Fosol.Common.Validation.Argument.Assert.MinRange(iterations, 1, "iterations");
            _Algorithm = Fosol.Common.Helpers.ReflectionHelper.ConstructObject(symmetricAlgorithmType) as SymmetricAlgorithm;

            var derive = new Rfc2898DeriveBytes(password, salt, iterations);
            _Algorithm.Key = derive.GetBytes(_Algorithm.KeySize);
            _Algorithm.IV = derive.GetBytes(_Algorithm.BlockSize / 8);
        }

        /// <summary>
        /// Creates a new instance of a SymmetricFactory class.
        /// 
        /// Standard SymmetricAlgorithm Types
        /// ---------------------------------
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
        /// <param name="symmetricAlgorithmType"></param>
        /// <param name="password">Password used to encrypt/decrypt data.</param>
        /// <param name="salt">Salt used to encrypt/decrypt data.</param>
        /// <param name="keyBitLength">Length in bits of the key.</param>
        /// <param name="ivBitLength">Length in bits of the initialization vector.</param>
        /// <param name="iterations">Number of types the process should be executed.</param>
        public SymmetricFactory(Type symmetricAlgorithmType, string password, byte[] salt, int keyBitLength, int ivBitLength, int iterations = 1000)
        {
            Fosol.Common.Validation.Argument.Assert.IsAssignable(symmetricAlgorithmType, typeof(SymmetricAlgorithm), "symmetricAlgorithmType");
            Fosol.Common.Validation.Argument.Assert.IsNotNullOrWhiteSpace(password, "password");
            Fosol.Common.Validation.Argument.Assert.IsNotNullOrEmpty(salt, "salt");
            Fosol.Common.Validation.Argument.Assert.MinRange(iterations, 1, "iterations");
            _Algorithm = Fosol.Common.Helpers.ReflectionHelper.ConstructObject(symmetricAlgorithmType) as SymmetricAlgorithm;

            Fosol.Common.Validation.Argument.Assert.WithinRange(_Algorithm.ValidKeySize(keyBitLength), "keyBitLength");
            Fosol.Common.Validation.Argument.Assert.WithinRange(_Algorithm.ValidIVSize(ivBitLength), "ivBitLength");
            _Algorithm.KeySize = keyBitLength;
            _Algorithm.BlockSize = ivBitLength * 8;

            var derive = new Rfc2898DeriveBytes(password, salt, iterations);
            _Algorithm.Key = derive.GetBytes(_Algorithm.KeySize);
            _Algorithm.IV = derive.GetBytes(_Algorithm.BlockSize / 8);
        }

        /// <summary>
        /// Creates a new instance of a SymmetricFactory class.
        /// 
        /// Standard SymmetricAlgorithm Types
        /// ---------------------------------
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
        /// <param name="symmetricAlgorithmType">SymmetricAlgorithm type name.</param>
        /// <param name="key">SymmetricAlgorithm key value.</param>
        /// <param name="iv">SymmetricAlgorithm IV value.</param>
        public SymmetricFactory(string symmetricAlgorithmType, byte[] key, byte[] iv)
        {
            Fosol.Common.Validation.Argument.Assert.IsNotNullOrWhiteSpace(symmetricAlgorithmType, "symmetricAlgorithmType");
            _Algorithm = SymmetricAlgorithm.Create(symmetricAlgorithmType);
            Fosol.Common.Validation.Argument.Assert.IsAssignable(_Algorithm, typeof(SymmetricAlgorithm), "symmetricAlgorithmType");

            Fosol.Common.Validation.Argument.Assert.WithinRange(_Algorithm.ValidKeySize(key), "key");
            Fosol.Common.Validation.Argument.Assert.WithinRange(_Algorithm.ValidIVSize(iv), "iv");
            _Algorithm.Key = key;
            _Algorithm.IV = iv;
        }

        /// <summary>
        /// Creates a new instance of a SymmetricFactory class.
        /// 
        /// Standard SymmetricAlgorithm Types
        /// ---------------------------------
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
        /// <param name="symmetricAlgorithmType">SymmetricAlgorithm type name.</param>
        /// <param name="password">Password used to encrypt/decrypt data.</param>
        /// <param name="salt">Salt used to encrypt/decrypt data.</param>
        /// <param name="iterations">Number of types the process should be executed.</param>
        public SymmetricFactory(string symmetricAlgorithmType, string password, byte[] salt, int iterations = 1000)
        {
            Fosol.Common.Validation.Argument.Assert.IsNotNullOrWhiteSpace(symmetricAlgorithmType, "symmetricAlgorithmType");
            Fosol.Common.Validation.Argument.Assert.IsNotNullOrWhiteSpace(password, "password");
            Fosol.Common.Validation.Argument.Assert.IsNotNullOrEmpty(salt, "salt");
            Fosol.Common.Validation.Argument.Assert.MinRange(iterations, 1, "iterations");
            _Algorithm = SymmetricAlgorithm.Create(symmetricAlgorithmType);
            Fosol.Common.Validation.Argument.Assert.IsAssignable(_Algorithm, typeof(SymmetricAlgorithm), "symmetricAlgorithmType");

            var derive = new Rfc2898DeriveBytes(password, salt, iterations);
            _Algorithm.Key = derive.GetBytes(_Algorithm.KeySize);
            _Algorithm.IV = derive.GetBytes(_Algorithm.BlockSize / 8);
        }

        /// <summary>
        /// Creates a new instance of a SymmetricFactory class.
        /// 
        /// Standard SymmetricAlgorithm Types
        /// ---------------------------------
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
        /// <param name="symmetricAlgorithmType">SymmetricAlgorithm type name.</param>
        /// <param name="password">Password used to encrypt/decrypt data.</param>
        /// <param name="salt">Salt used to encrypt/decrypt data.</param>
        /// <param name="keyBitLength">Length in bits of the key.</param>
        /// <param name="ivBitLength">Length in bits of the initialization vector.</param>
        /// <param name="iterations">Number of types the process should be executed.</param>
        public SymmetricFactory(string symmetricAlgorithmType, string password, byte[] salt, int keyBitLength, int ivBitLength, int iterations = 1000)
        {
            Fosol.Common.Validation.Argument.Assert.IsNotNullOrWhiteSpace(symmetricAlgorithmType, "symmetricAlgorithmType");
            Fosol.Common.Validation.Argument.Assert.IsNotNullOrWhiteSpace(password, "password");
            Fosol.Common.Validation.Argument.Assert.IsNotNullOrEmpty(salt, "salt");
            Fosol.Common.Validation.Argument.Assert.MinRange(iterations, 1, "iterations");
            _Algorithm = SymmetricAlgorithm.Create(symmetricAlgorithmType);
            Fosol.Common.Validation.Argument.Assert.IsAssignable(_Algorithm, typeof(SymmetricAlgorithm), "symmetricAlgorithmType");

            Fosol.Common.Validation.Argument.Assert.WithinRange(_Algorithm.ValidKeySize(keyBitLength), "keyBitLength");
            Fosol.Common.Validation.Argument.Assert.WithinRange(_Algorithm.ValidIVSize(ivBitLength), "ivBitLength");
            _Algorithm.KeySize = keyBitLength;
            _Algorithm.BlockSize = ivBitLength * 8;

            var derive = new Rfc2898DeriveBytes(password, salt, iterations);
            _Algorithm.Key = derive.GetBytes(_Algorithm.KeySize);
            _Algorithm.IV = derive.GetBytes(_Algorithm.BlockSize / 8);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Encrypt the data.
        /// </summary>
        /// <param name="data">Data to encrypt</param>
        /// <returns>Encrypted data.</returns>
        public byte[] Encrypt(byte[] data)
        {
            Fosol.Common.Validation.Argument.Assert.IsNotNullOrEmpty(data, "data");

            return _Algorithm.Encrypt(data);
        }

        /// <summary>
        /// Decrypt the data.
        /// </summary>
        /// <param name="data">Data to decrypt.</param>
        /// <returns>Decrypted data.</returns>
        public byte[] Decrypt(byte[] data)
        {
            Fosol.Common.Validation.Argument.Assert.IsNotNullOrEmpty(data, "data");

            return _Algorithm.Decrypt(data);
        }

        /// <summary>
        /// Dispose the SymmetricAlgorithm and release all resources used by it.
        /// </summary>
        public void Dispose()
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
