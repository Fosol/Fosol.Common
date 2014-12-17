using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Exceptions
{
    /// <summary>
    /// PropertyNullException class provides a way to identify an exception that occurs due to an null property value.
    /// </summary>
    public class PropertyNullException
        : PropertyException
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public PropertyNullException()
            : base()
        {

        }

        public PropertyNullException(string message)
            : base(message)
        {
        }

        public PropertyNullException(string message, Exception innerException)
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
