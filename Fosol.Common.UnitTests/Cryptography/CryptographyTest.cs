using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fosol.Common.UnitTests.Cryptography
{
    [TestClass]
    public sealed class CryptographyTest
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Methods
        [TestMethod]
        public void GenerateSaltDefault()
        {
            var salt = Fosol.Common.Cryptography.CryptographyFactory.GenerateSalt();

            Assert.IsTrue(salt.Length == 16, "Salt length should be 16 bytes.");
        }

        [TestMethod]
        public void GenerateSalt32()
        {
            var salt = Fosol.Common.Cryptography.CryptographyFactory.GenerateSalt(32);

            Assert.IsTrue(salt.Length == 32, "Salt length should be 32 bytes.");
        }

        [TestMethod]
        public void GenerateSalt8()
        {
            var salt = Fosol.Common.Cryptography.CryptographyFactory.GenerateSalt(8);

            Assert.IsTrue(salt.Length == 8, "Salt length should be 8 bytes.");
        }

        [TestMethod]
        public void GenerateSalt4()
        {
            var salt = Fosol.Common.Cryptography.CryptographyFactory.GenerateSalt(4);

            Assert.IsTrue(salt.Length == 4, "Salt length should be 4 bytes.");
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
