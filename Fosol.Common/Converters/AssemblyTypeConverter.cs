using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Converters
{
    /// <summary>
    /// AssemblyTypeConverter provides a way to convert a string value into the specified Type.
    /// </summary>
    public sealed class AssemblyTypeConverter
        : TypeConverter
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Methods
        /// <summary>
        /// Can convert from the following types;
        /// - string
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// Can convert to the following types;
        /// - string
        /// </summary>
        /// <param name="context"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
                return true;
            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// Convert from the current value to the specified Type.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value.GetType() == typeof(string) && !string.IsNullOrEmpty((string)value))
            {
                return Type.GetType((string)value, true, true);
            }

            return base.ConvertFrom(context, culture, value);
        }

        /// <summary>
        /// If the current value is a Type object it will return the full name.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (value.GetType() == typeof(Type))
            {
                return ((Type)value).FullName;
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
