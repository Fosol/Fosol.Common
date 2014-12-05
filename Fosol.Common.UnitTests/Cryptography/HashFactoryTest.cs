using Fosol.Common.Cryptography;
using System;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fosol.Common.UnitTests.Cryptography
{
    [TestClass]
    public class HashFactoryTest
    {
        #region Methods
        /// <summary>
        /// Hash using the MD5 algorithm.
        /// Confirm that using the same data and salt you receive the same hash.
        /// </summary>
        [TestMethod]
        public void CryptographyHashFactoryHMACMD5()
        {
            var algorithm = new HashFactory(new HMACMD5());

            var data = new byte[32];
            RandomFactory.Generate(data);

            var salt = RandomFactory.GenerateSalt();

            var hash1 = algorithm.ComputeHash(data, salt);
            var hash2 = algorithm.ComputeHash(data, salt);

            Assert.IsTrue(hash1.SequenceEqual(hash2));
        }
        #endregion
    }
}
