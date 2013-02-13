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
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Methods
        /// <summary>
        /// Encrypt a string with AesManaged algorithm.
        /// </summary>
        /// <param name="text">String value to encrypt.</param>
        /// <param name="password">Password to encrypt with.</param>
        /// <param name="salt">Salt must be at least 8 bytes long.</param>
        /// <returns>Encrypted string value.</returns>
        public static string Encrypt(string text, string password, string salt)
        {
            AesManaged aes = null;
            MemoryStream stream = null;
            CryptoStream crypto_stream = null;

            try
            {
                var rfc2898 = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(salt), 10000);
                aes = new AesManaged();
                aes.Key = rfc2898.GetBytes(2);
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
        /// 
        /// </summary>
        /// <param name="encryptedText"></param>
        /// <param name="password"></param>
        /// <param name="salt">Salt must be at least 8 bytes long.</param>
        /// <returns></returns>
        public static string Decrypt(string encryptedText, string password, string salt)
        {
            AesManaged aes = null;
            MemoryStream stream = null;
            CryptoStream crypto_stream = null;

            try
            {
                // Generate a key based on the password and HMACSHA1 pseudo-random number generator.
                var rfc2898 = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(salt), 10000);

                aes = new AesManaged();
                aes.Key = rfc2898.GetBytes(2);
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
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
