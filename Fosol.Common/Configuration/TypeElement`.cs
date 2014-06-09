using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Configuration
{
    /// <summary>
    /// TypeElement provides a way to include an object type as a configurable option.
    /// </summary>
    public class TypeElement<T>
        : TypeElement
        where T : class
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a TypeElement object of type T.
        /// </summary>
        public TypeElement()
            : base(typeof(T))
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
