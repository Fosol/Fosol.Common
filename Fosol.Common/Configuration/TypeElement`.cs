using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Configuration
{
    public class TypeElement<T>
        : TypeElement
        where T : class
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
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
