using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Cryptography
{
    public sealed class AesHelper
        : CryptographyFactory
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public AesHelper()
            : base(new AesManaged())
        {
        }

        public AesHelper(DeriveBytes generator)
            : base(new AesManaged())
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
