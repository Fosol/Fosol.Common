using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Exceptions
{
    /// <summary>
    /// ValueException class provides a way to identify an exception that occurs due to an invalid value.
    /// </summary>
    public class ValueException
        : InvalidOperationException
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public ValueException()
            : base()
        {

        }

        public ValueException(string message)
            : base(message)
        {
        }

        public ValueException(string message, Exception innerException)
            : base(message, innerException)
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
