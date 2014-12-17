using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Exceptions
{
    /// <summary>
    /// PropertyException class provides a way to identify an exception that occurs due to an invalid property value.
    /// </summary>
    public class PropertyException
        : SystemException
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public PropertyException()
            : base()
        {

        }

        public PropertyException(string message)
            : base(message)
        {
        }

        public PropertyException(string message, Exception innerException)
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
