using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Cryptography
{
    /// <summary>
    /// ICryptography interface provides a base interface for encryption and decryption.
    /// This will allow any form of algorithm to be calle with the based methods.
    /// </summary>
    public interface ICryptography
        : IDisposable
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Methods
        /// <summary>
        /// Encrypt the data.
        /// </summary>
        /// <param name="data">Data to be encrypted.</param>
        /// <returns>Encrypted data.</returns>
        byte[] Encrypt(byte[] data);

        /// <summary>
        /// Decrypt the data.
        /// </summary>
        /// <param name="data">Data to be decrypted.</param>
        /// <returns>Decrypted data.</returns>
        byte[] Decrypt(byte[] data);
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
