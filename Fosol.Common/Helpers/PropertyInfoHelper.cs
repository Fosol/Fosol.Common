using Fosol.Common.Extensions.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Helpers
{
    /// <summary>
    /// Helper methods for PropertyInfo objects.
    /// </summary>
    public static class PropertyInfoHelper
    {
        #region Methods
        /// <summary>
        /// Sets the property value of the specified object.
        /// Handles basic conversion of the value to the object Type.
        /// Handles Nullable types.
        /// </summary>
        /// <param name="info">PropertyInfo object.</param>
        /// <param name="obj">The object whose property will be set.</param>
        /// <param name="value">The new property value.</param>
        public static void SetValue(PropertyInfo info, object obj, object value)
        {
            var target_type = info.PropertyType.IsNullableType()
                ? Nullable.GetUnderlyingType(info.PropertyType)
                : info.PropertyType;

            info.SetValue(obj, Convert.ChangeType(value, target_type));
        }
        #endregion
    }
}
