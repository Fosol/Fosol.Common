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
        : SymmetricalFactory
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
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
