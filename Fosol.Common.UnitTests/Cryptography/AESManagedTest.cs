using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography;

namespace Fosol.Common.UnitTests.Cryptography
{
    [TestClass]
    public class AESManagedTest
    {
        #region Variables
        private const string _Text = "This is a sentence to test encryption.";
        private const string _Key = "4EC4C41A-F344-4CE8-9FB6-BB6C4E899839";
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
            var salt = Fosol.Common.Cryptography.RandomFactory.GenerateSalt();
            var factory = new Fosol.Common.Cryptography.SymmetricFactory(typeof(AesManaged), _Key, salt);
            var text_data = Encoding.UTF8.GetBytes(_Text);

            var encrypted_data = factory.Encrypt(text_data);

            var decrypted_data = factory.Decrypt(encrypted_data);
            var decrypted_text = Encoding.UTF8.GetString(decrypted_data);

            Assert.AreEqual(_Text, decrypted_text, "Original text must be the identical to the decrypted text.");
        }

        [TestMethod]
        public void CryptographyAesEncryptAndDecrypt256Overload()
        {
            var salt = Fosol.Common.Cryptography.RandomFactory.GenerateSalt();
            var factory = new Fosol.Common.Cryptography.SymmetricFactory(typeof(AesManaged), _Key, salt, 256, 128);
            var text_data = Encoding.UTF8.GetBytes(_Text);

            var encrypted_data = factory.Encrypt(text_data);

            var decrypted_data = factory.Decrypt(encrypted_data);
            var decrypted_text = Encoding.UTF8.GetString(decrypted_data);

            Assert.AreEqual(_Text, decrypted_text, "Original text must be the identical to the decrypted text.");
        }

        [TestMethod]
        public void CryptographyAesEncryptAndDecrypt192()
        {
            var salt = Fosol.Common.Cryptography.RandomFactory.GenerateSalt();
            var factory = new Fosol.Common.Cryptography.SymmetricFactory(typeof(AesManaged), _Key, salt, 192, 128);
            var text_data = Encoding.UTF8.GetBytes(_Text);

            var encrypted_data = factory.Encrypt(text_data);

            var decrypted_data = factory.Decrypt(encrypted_data);
            var decrypted_text = Encoding.UTF8.GetString(decrypted_data);

            Assert.AreEqual(_Text, decrypted_text, "Original text must be the identical to the decrypted text.");
        }

        [TestMethod]
        public void CryptographyAesEncryptAndDecrypt128()
        {
            var salt = Fosol.Common.Cryptography.RandomFactory.GenerateSalt();
            var factory = new Fosol.Common.Cryptography.SymmetricFactory(typeof(AesManaged), _Key, salt, 128, 128);
            var text_data = Encoding.UTF8.GetBytes(_Text);

            var encrypted_data = factory.Encrypt(text_data);

            var decrypted_data = factory.Decrypt(encrypted_data);
            var decrypted_text = Encoding.UTF8.GetString(decrypted_data);

            Assert.AreEqual(_Text, decrypted_text, "Original text must be the identical to the decrypted text.");
        }
        #endregion

        #region Validation
        [TestMethod]
        public void CryptographyAesEncryptAndDecryptInvalidKeySize()
        {
            try
            {
                var salt = Fosol.Common.Cryptography.RandomFactory.GenerateSalt();
                var factory = new Fosol.Common.Cryptography.SymmetricFactory(typeof(AesManaged), _Key, salt, 2, 128);
            }
            catch (ArgumentException ex)
            {
                Assert.IsNotNull(ex, "Key size is invalid.");
            }
        }

        [TestMethod]
        public void CryptographyAesEncryptAndDecryptInvalidIVSize()
        {
            try
            {
                var salt = Fosol.Common.Cryptography.RandomFactory.GenerateSalt();
                var factory = new Fosol.Common.Cryptography.SymmetricFactory(typeof(AesManaged), _Key, salt, 256, 2);
            }
            catch (ArgumentException ex)
            {
                Assert.IsNotNull(ex, "Key size is invalid.");
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
