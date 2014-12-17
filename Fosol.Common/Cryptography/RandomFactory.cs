using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Cryptography
{
    /// <summary>
    /// RandomFactory is a static class that provides methods to generate random values.
    /// This class is primarily to remind me how to generate random values.
    /// </summary>
    public static class RandomFactory
    {
        #region Methods
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
            RandomFactory.Generate(array);
            return array;
        }

        /// <summary>
        /// Generates a random value and populates the byte array provided.
        /// Uses RNGCryptoServiceProvider formula to create the salt.
        /// Use Convert.ToBase64String() method to convert the byte array to a string.
        /// </summary>
        /// <param name="data">Byte array to populate with random values.</param>
        public static void Generate(byte[] data)
        {
            RandomFactory.Generate(new RNGCryptoServiceProvider(), data);
        }

        /// <summary>
        /// Generates a random value and populates the byte array provided.
        /// Use Convert.ToBase64String() method to convert the byte array to a string.
        /// </summary>
        /// <param name="generator">RandomNumberGenerator object.</param>
        /// <param name="data">Byte array to populate with random values.</param>
        public static void Generate(RandomNumberGenerator generator, byte[] data)
        {
            Fosol.Common.Validation.Argument.Assert.IsNotNull(generator, "generator");
            generator.GetBytes(data);
        }
        #endregion
    }
}
