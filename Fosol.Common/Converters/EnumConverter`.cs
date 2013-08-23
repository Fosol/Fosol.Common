using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Converters
{
    /// <summary>
    /// Provides a way to define an EnumConverter in an Attribute.
    /// </summary>
    /// <typeparam name="T">Type of enum.</typeparam>
    public sealed class EnumConverter<T>
        : System.ComponentModel.EnumConverter
        where T : struct, IConvertible
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of an EnumConverter object.
        /// </summary>
        public EnumConverter()
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
