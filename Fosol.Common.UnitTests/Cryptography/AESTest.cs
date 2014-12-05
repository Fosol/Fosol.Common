﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography;

namespace Fosol.Common.UnitTests.Cryptography
{
    [TestClass]
    public class AESTest
    {
        #region Variables
        private const string _Text = "This is a sentence to test encryption.";
        private const string _Key = "4EC4C41A-F344-4CE8-9FB6-BB6C4E899839";
        private const int _SaltSize = 16;
        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Methods
        #region Functionality
        [TestMethod]
        public void CryptographyAesEncryptAndDecrypt256()
        {
            var factory = new Fosol.Common.Cryptography.SymmetricFactory(Aes.Create());
            var text_data = Encoding.UTF8.GetBytes(_Text);
            var key_data = Encoding.UTF8.GetBytes(_Key);
            var salt = Fosol.Common.Cryptography.RandomFactory.GenerateSalt(_SaltSize);

            var encrypted_data = factory.Encrypt(text_data, _Key, salt);

            var decrypted_data = factory.Decrypt(encrypted_data, _Key, salt);
            var decrypted_text = Encoding.UTF8.GetString(decrypted_data);

            Assert.AreEqual(_Text, decrypted_text, "Original text must be the identical to the decrypted text.");
        }

        [TestMethod]
        public void CryptographyAesEncryptAndDecrypt256Overload()
        {
            var factory = new Fosol.Common.Cryptography.SymmetricFactory(Aes.Create());
            var text_data = Encoding.UTF8.GetBytes(_Text);
            var key_data = Encoding.UTF8.GetBytes(_Key);
            var salt = Fosol.Common.Cryptography.RandomFactory.GenerateSalt(_SaltSize);

            var encrypted_data = factory.Encrypt(text_data, _Key, salt, 32, 16);

            var decrypted_data = factory.Decrypt(encrypted_data, _Key, salt, 32, 16);
            var decrypted_text = Encoding.UTF8.GetString(decrypted_data);

            Assert.AreEqual(_Text, decrypted_text, "Original text must be the identical to the decrypted text.");
        }

        [TestMethod]
        public void CryptographyAesEncryptAndDecrypt192()
        {
            var factory = new Fosol.Common.Cryptography.SymmetricFactory(Aes.Create());
            var text_data = Encoding.UTF8.GetBytes(_Text);
            var key_data = Encoding.UTF8.GetBytes(_Key);
            var salt = Fosol.Common.Cryptography.RandomFactory.GenerateSalt(_SaltSize);

            var encrypted_data = factory.Encrypt(text_data, _Key, salt, 24, 16);

            var decrypted_data = factory.Decrypt(encrypted_data, _Key, salt, 24, 16);
            var decrypted_text = Encoding.UTF8.GetString(decrypted_data);

            Assert.AreEqual(_Text, decrypted_text, "Original text must be the identical to the decrypted text.");
        }

        [TestMethod]
        public void CryptographyAesEncryptAndDecrypt128()
        {
            var factory = new Fosol.Common.Cryptography.SymmetricFactory(Aes.Create());
            var text_data = Encoding.UTF8.GetBytes(_Text);
            var key_data = Encoding.UTF8.GetBytes(_Key);
            var salt = Fosol.Common.Cryptography.RandomFactory.GenerateSalt(_SaltSize);

            var encrypted_data = factory.Encrypt(text_data, _Key, salt, 16, 16);

            var decrypted_data = factory.Decrypt(encrypted_data, _Key, salt, 16, 16);
            var decrypted_text = Encoding.UTF8.GetString(decrypted_data);

            Assert.AreEqual(_Text, decrypted_text, "Original text must be the identical to the decrypted text.");
        }

        [TestMethod]
        public void CryptographyAesEncryptAndDecryptDerivedBytes()
        {
            var factory = new Fosol.Common.Cryptography.SymmetricFactory(Aes.Create());
            var text_data = Encoding.UTF8.GetBytes(_Text);
            var key_data = Encoding.UTF8.GetBytes(_Key);
            var salt = Fosol.Common.Cryptography.RandomFactory.GenerateSalt(_SaltSize);
            var generator = new System.Security.Cryptography.Rfc2898DeriveBytes(_Key, salt);

            var encrypted_data = factory.Encrypt(text_data, generator);

            generator.Reset();
            var decrypted_data = factory.Decrypt(encrypted_data, generator);
            var decrypted_text = Encoding.UTF8.GetString(decrypted_data);

            Assert.AreEqual(_Text, decrypted_text, "Original text must be the identical to the decrypted text.");
        }

        [TestMethod]
        public void CryptographyAesEncryptAndDecryptDerivedBytesOverload()
        {
            var factory = new Fosol.Common.Cryptography.SymmetricFactory(Aes.Create());
            var text_data = Encoding.UTF8.GetBytes(_Text);
            var key_data = Encoding.UTF8.GetBytes(_Key);
            var salt = Fosol.Common.Cryptography.RandomFactory.GenerateSalt(_SaltSize);
            var generator = new System.Security.Cryptography.Rfc2898DeriveBytes(_Key, salt);

            var encrypted_data = factory.Encrypt(text_data, generator, 24, 16);

            generator.Reset();
            var decrypted_data = factory.Decrypt(encrypted_data, generator, 24, 16);
            var decrypted_text = Encoding.UTF8.GetString(decrypted_data);

            Assert.AreEqual(_Text, decrypted_text, "Original text must be the identical to the decrypted text.");
        }
        #endregion

        #region Validation
        [TestMethod]
        public void CryptographyAesEncryptAndDecryptInvalidKeySize()
        {
            var factory = new Fosol.Common.Cryptography.SymmetricFactory(Aes.Create());
            var text_data = Encoding.UTF8.GetBytes(_Text);
            var key_data = Encoding.UTF8.GetBytes(_Key);
            var salt = Fosol.Common.Cryptography.RandomFactory.GenerateSalt(_SaltSize);

            try
            {
                var encrypted_data = factory.Encrypt(text_data, _Key, salt, 2, 32);
            }
            catch (ArgumentException ex)
            {
                Assert.IsNotNull(ex, "Key size is invalid.");
            }
        }

        [TestMethod]
        public void CryptographyAesEncryptAndDecryptInvalidKeySize1()
        {
            var factory = new Fosol.Common.Cryptography.SymmetricFactory(Aes.Create());
            var text_data = Encoding.UTF8.GetBytes(_Text);
            var key_data = Encoding.UTF8.GetBytes(_Key);
            var salt = Fosol.Common.Cryptography.RandomFactory.GenerateSalt(_SaltSize);

            try
            {
                var encrypted_data = factory.Encrypt(text_data, _Key, salt, 32, 32);
            }
            catch (ArgumentException ex)
            {
                Assert.IsNotNull(ex, "Key size is invalid.");
            }
        }

        [TestMethod]
        public void CryptographyAesEncryptAndDecryptInvalidKeySize2()
        {
            var factory = new Fosol.Common.Cryptography.SymmetricFactory(Aes.Create());
            var text_data = Encoding.UTF8.GetBytes(_Text);
            var key_data = Encoding.UTF8.GetBytes(_Key);
            var salt = Fosol.Common.Cryptography.RandomFactory.GenerateSalt(_SaltSize);

            try
            {
                var encrypted_data = factory.Encrypt(text_data, _Key, salt, 66, 32);
            }
            catch (ArgumentException ex)
            {
                Assert.IsNotNull(ex, "Key size is invalid.");
            }
        }

        [TestMethod]
        public void CryptographyAesEncryptAndDecryptInvalidIVSize()
        {
            var factory = new Fosol.Common.Cryptography.SymmetricFactory(Aes.Create());
            var text_data = Encoding.UTF8.GetBytes(_Text);
            var key_data = Encoding.UTF8.GetBytes(_Key);
            var salt = Fosol.Common.Cryptography.RandomFactory.GenerateSalt(_SaltSize);

            try
            {
                var encrypted_data = factory.Encrypt(text_data, _Key, salt, 32, 2);
            }
            catch (ArgumentException ex)
            {
                Assert.IsNotNull(ex, "IV size is invalid.");
            }
        }
        #endregion
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
