using Fosol.Common.Extensions.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Helpers
{
    /// <summary>
    /// Helper methods for PropertyInfo objects.
    /// </summary>
    public static class ReflectionHelper
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

        /// <summary>
        /// Sets the property value of the specified object.
        /// Uses the converter if possible to convert the value to the appropriate Type.
        /// Handles basic conversion of the value to the object Type.
        /// Handles Nullable types.
        /// </summary>
        /// <param name="info">PropertyInfo object.</param>
        /// <param name="obj">The object whose property will be set.</param>
        /// <param name="value">The new property value.</param>
        /// <param name="converter">TypeConverter object.</param>
        public static void SetValue(PropertyInfo info, object obj, object value, TypeConverter converter)
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
                SetValue(info, obj, value);
        }

        /// <summary>
        /// Sets the field value of the specifed object.
        /// Handles basic conversion of the value to the object Type.
        /// Handles Nullable types.
        /// </summary>
        /// <param name="info">FieldInfo object.</param>
        /// <param name="obj">The object whose field will be set.</param>
        /// <param name="value">The new field value.</param>
        public static void SetValue(FieldInfo info, object obj, object value)
        {
            var target_type = info.FieldType.IsNullableType()
                ? Nullable.GetUnderlyingType(info.FieldType)
                : info.FieldType;

            info.SetValue(obj, Convert.ChangeType(value, target_type));
        }

        /// <summary>
        /// Sets the field value of the specifed object.
        /// Uses the converter if possible to convert the value to the appropriate Type.
        /// Handles basic conversion of the value to the object Type.
        /// Handles Nullable types.
        /// </summary>
        /// <param name="info">FieldInfo object.</param>
        /// <param name="obj">The object whose field will be set.</param>
        /// <param name="value">The new field value.</param>
        /// <param name="converter">TypeConverter object.</param>
        public static void SetValue(FieldInfo info, object obj, object value, TypeConverter converter)
        {
            Validation.Assert.IsNotNull(converter, "converter");
            // Same type is easy.
            if (info.FieldType == value.GetType())
                info.SetValue(obj, value);
            // Converter can converter, use it.
            else if (converter.CanConvertFrom(value.GetType()))
                info.SetValue(obj, converter.ConvertFrom(value));
            // Use the good ole college try.
            else
                SetValue(info, obj, value);
        }
        #endregion
    }
}
