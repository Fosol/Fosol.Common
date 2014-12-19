using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Exceptions
{
    /// <summary>
    /// PropertyOutOfRangeException class provides a way to identify an exception that occurs due to an null property value.
    /// </summary>
    public class PropertyOutOfRangeException
        : PropertyException
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public PropertyOutOfRangeException()
            : base()
        {

        }

        public PropertyOutOfRangeException(string message)
            : base(message)
        {
        }

        public PropertyOutOfRangeException(string message, string propertyName)
            : base(message, propertyName)
        {
        }

        public PropertyOutOfRangeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public PropertyOutOfRangeException(string message, string propertyName, Exception innerException)
            : base(message, propertyName, innerException)
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
