using Fosol.Common.Extensions.Bytes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Cryptography
{
    /// <summary>
    /// Provides a few generic methods to encrypt and decrypt string values.
    /// </summary>
    public static class CryptographyFactory
    {
        #region Methods
        /// <summary>
        /// Encrypt a string with AesManaged algorithm.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameters 'text', 'password' and 'salt' cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameters 'text', 'password' and 'salt' cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameter 'salt' must be at least 8 bytes long.</exception>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.FormatException"></exception>
        /// <exception cref="System.NotSupportedException"></exception>
        /// <exception cref="System.Security.Cryptography.CryptographicException"></exception>
        /// <exception cref="System.Text.DecoderFallbackException"></exception>
        /// <param name="text">String value to encrypt.</param>
        /// <param name="password">Password to encrypt with.</param>
        /// <param name="salt">Salt must be at least 8 bytes long.</param>
        /// <returns>Encrypted string value.</returns>
        public static string Encrypt(string text, string password, string salt)
        {
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(text, "text");
            Fosol.Common.Validation.Assert.IsNotNullOrWhiteSpace(password, "password");
            Fosol.Common.Validation.Assert.IsNotNullOrWhiteSpace(salt, "salt");
            Fosol.Common.Validation.Assert.MinRange(salt.Length, 8, "salt");

            AesManaged aes = null;
            MemoryStream stream = null;
            CryptoStream crypto_stream = null;

            try
            {
                var rfc2898 = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(salt), 10000);
                aes = new AesManaged();
                aes.Key = rfc2898.GetBytes(32);
                aes.IV = rfc2898.GetBytes(16);

                stream = new MemoryStream();
                crypto_stream = new CryptoStream(stream, aes.CreateEncryptor(), CryptoStreamMode.Write);

                var data = Encoding.UTF8.GetBytes(text);
                crypto_stream.Write(data, 0, data.Length);
                crypto_stream.FlushFinalBlock();

                return Convert.ToBase64String(stream.ToArray());
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

                if (aes != null)
                    aes.Clear();
            }
        }

        /// <summary>
        /// Decrypt a string with AesManaged algorithm.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameters 'encryptedText', 'password' and 'salt' cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameters 'encryptedText', 'password' and 'salt' cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameter 'salt' must be at least 8 bytes long.</exception>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.FormatException"></exception>
        /// <exception cref="System.NotSupportedException"></exception>
        /// <exception cref="System.Security.Cryptography.CryptographicException"></exception>
        /// <exception cref="System.Text.DecoderFallbackException"></exception>
        /// <param name="encryptedText">Decrypted string value to decrypt.</param>
        /// <param name="password">Password to decrypt with.</param>
        /// <param name="salt">Salt must be at least 8 bytes long.</param>
        /// <returns>Decrypted string value.</returns>
        public static string Decrypt(string encryptedText, string password, string salt)
        {
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(encryptedText, "encryptedText");
            Fosol.Common.Validation.Assert.IsNotNullOrWhiteSpace(password, "password");
            Fosol.Common.Validation.Assert.IsNotNullOrWhiteSpace(salt, "salt");
            Fosol.Common.Validation.Assert.MinRange(salt.Length, 8, "salt");

            AesManaged aes = null;
            MemoryStream stream = null;
            CryptoStream crypto_stream = null;

            try
            {
                // Generate a key based on the password and HMACSHA1 pseudo-random number generator.
                var rfc2898 = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(salt), 10000);

                aes = new AesManaged();
                aes.Key = rfc2898.GetBytes(32);
                aes.IV = rfc2898.GetBytes(16);

                stream = new MemoryStream();
                crypto_stream = new CryptoStream(stream, aes.CreateDecryptor(), CryptoStreamMode.Write);

                var data = Convert.FromBase64String(encryptedText);
                crypto_stream.Write(data, 0, data.Length);
                crypto_stream.FlushFinalBlock();

                var decrypted_data = stream.ToArray();

                return Encoding.UTF8.GetString(decrypted_data, 0, decrypted_data.Length);
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

                if (aes != null)
                    aes.Clear();
            }
        }

        /// <summary>
        /// Creates a hash based on the text and salt value.
        /// Uses SHA1 encryption formula.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameters 'text' and 'salt' cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameters 'text' and 'salt' cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameter 'salt' must be at least 8 bytes long.</exception>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.ObjectDisposedException"></exception>
        /// <exception cref="System.Text.EncoderFallbackException"></exception>
        /// <param name="text">Text value to hash.</param>
        /// <param name="salt">Salt value to use in creating a secure hash.</param>
        /// <returns>Hashed text value.</returns>
        public static string ComputeHash(string text, string salt)
        {
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(text, "text");
            Fosol.Common.Validation.Assert.IsNotNullOrWhiteSpace(salt, "salt");
            Fosol.Common.Validation.Assert.MinRange(salt.Length, 8, "salt");

            var text_data = System.Text.Encoding.UTF8.GetBytes(text);
            var salt_data = System.Text.Encoding.UTF8.GetBytes(salt);

            var data = new byte[text_data.Length + salt_data.Length];
            var offset = data.Append(text_data);

            var formula = new SHA1Managed();
            formula.ComputeHash(data);

            return Convert.ToBase64String(formula.Hash);
        }

        /// <summary>
        /// Generates a random salt value to use.
        /// </summary>
        /// <returns>A new salt value.</returns>
        public static string GenerateSalt()
        {
            var array = new byte[16];
            new RNGCryptoServiceProvider().GetBytes(array);
            return Convert.ToBase64String(array);
        }
        #endregion
    }
}
