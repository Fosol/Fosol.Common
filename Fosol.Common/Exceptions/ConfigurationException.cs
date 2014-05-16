using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Exceptions
{
    /// <summary>
    /// Provides a way to capture generic configuration exceptions.
    /// </summary>
    public class ConfigurationException
        : Exception
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public ConfigurationException()
            : base()
        {

        }

        public ConfigurationException(string message)
            : base(message)
        {
        }

        public ConfigurationException(string message, Exception innerException)
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
