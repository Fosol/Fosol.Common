using Fosol.Common.Extensions.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Extensions.Reflection
{
    /// <summary>
    /// PropertyInfo extensin methods.
    /// </summary>
    public static class PropertyInfoExtensions
    {
        #region Methods

        /// <summary>
        /// If the property has a DefaultValueAttribute, apply the value to the property.
        /// </summary>
        /// <param name="item">Object whose property you want to update with the DefaultValueAttribute value.</param>
        /// <param name="itemProperty">The specific property you want to update.</param>
        /// <param name="converter">If required the TypeConverter to use to set the value.</param>
        public static void ApplyDefaultValue(this PropertyInfo itemProperty, object item, TypeConverter valueConverter = null)
        {
            var attr_default = itemProperty.GetCustomAttribute(typeof(DefaultValueAttribute), true) as DefaultValueAttribute;
            if (attr_default != null)
            {
                if (valueConverter != null)
                    SetValue2(itemProperty, item, attr_default.Value, valueConverter);
                else
                    itemProperty.SetValue(item, attr_default.Value);
            }
        }

        /// <summary>
        /// Sets the property value of the specified object.
        /// Handles basic conversion of the value to the object Type.
        /// Handles Nullable types.
        /// </summary>
        /// <param name="info">PropertyInfo object.</param>
        /// <param name="obj">The object whose property will be set.</param>
        /// <param name="value">The new property value.</param>
        public static void SetValue2(this PropertyInfo info, object obj, object value)
        {
            var target_type = info.PropertyType.IsNullableType()
                ? Nullable.GetUnderlyingType(info.PropertyType)
                : info.PropertyType;

            info.SetValue(obj, Convert.ChangeType(value, target_type));
        }

        /// <summary>
        /// Sets the property value of the specified object.
        /// Uses the converter if possible to convert the value to the appropriate Type.
        /// Handles basic conversion of the value to the object Type.
        /// Handles Nullable types.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Parameter "converter" cannot be null.</exception>
        /// <param name="info">PropertyInfo object.</param>
        /// <param name="obj">The object whose property will be set.</param>
        /// <param name="value">The new property value.</param>
        /// <param name="converter">TypeConverter object.</param>
        public static void SetValue2(this PropertyInfo info, object obj, object value, TypeConverter converter)
        {
            Validation.Assert.IsNotNull(converter, "converter");
            // Same type is easy.
            if (info.PropertyType == value.GetType())
                info.SetValue(obj, value);
            // Converter can converter, use it.
            else if (converter.CanConvertFrom(value.GetType()))
                info.SetValue(obj, converter.ConvertFrom(value));
            // Use the good ole college try.
            else
                SetValue2(info, obj, value);
        }
        #endregion
    }
}
