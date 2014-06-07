using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        [TestMethod]
        public void EncryptAndDecryptTest()
        {
            var factory = new Fosol.Common.Cryptography.AESHelper();
            var text_data = Encoding.UTF8.GetBytes(_Text);
            var key_data = Encoding.UTF8.GetBytes(_Key);
            var salt = Fosol.Common.Cryptography.CryptographyFactory.GenerateSalt(_SaltSize);
            var encrypted_data = factory.Encrypt(text_data, _Key, salt);

            var decrypted_data = factory.Decrypt(encrypted_data, _Key, salt);
            var decrypted_text = Encoding.UTF8.GetString(decrypted_data);

            Assert.AreEqual(_Text, decrypted_text);
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
