using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Exceptions
{
    /// <summary>
    /// ValueOutOfRangeException class provides a way to identify an exception that occurs due to an invalid value.
    /// </summary>
    public class ValueOutOfRangeException
        : ValueException
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public ValueOutOfRangeException()
            : base()
        {

        }

        public ValueOutOfRangeException(string message)
            : base(message)
        {
        }

        public ValueOutOfRangeException(string message, Exception innerException)
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
