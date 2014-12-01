using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Cryptography
{
    public sealed class RijndaelHelper
        : SymmetricFactory
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a RijndaelHelper class.
        /// </summary>
        public RijndaelHelper()
            : base(Rijndael.Create())
        {
        }
        #endregion

        #region Methods
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
