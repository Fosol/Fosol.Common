using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Extensions.Enums
{
    /// <summary>
    /// Extension methods for enums.
    /// </summary>
    public static class EnumExtensions
    {
        #region Methods
        /// <summary>
        /// Creates a Dictionary with the specified Enum type and the descriptions for each enum value.
        /// The descriptions must be specified with the DescriptionAttribute keyword within the Enum's declaration.
        /// </summary>
        /// <example>
        /// public enum Transmission
        /// {
        ///     [DescriptionAttribute("None")]
        ///     None = 0,
        ///     
        ///     [DescriptionAttribute("Telephone")]
        ///     Phone = 2,
        ///     
        ///     [DescriptionAttribute("E-Mail Attachement")]
        ///     Mail = 4
        /// }
        /// </example>
        /// <exception cref="System.ArgumentException">Parameter "enumType" must be of type enum.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "enumType" cannot be null.</exception>
        /// <param name="enumType">Type of Enum you want to create a Dictionary for.</param>
        /// <param name="includeDescription">Determines whether description information is included in the dictionary.</param>
        /// <returns>Dictionary with each enum value and description.</returns>
        public static IDictionary<object, string> ToDictionary(this Type enumType, bool includeDescription = true)
        {
            Validation.Argument.Assert.IsNotNull(enumType, "enumType");
            Validation.Argument.Assert.IsValue(enumType.IsEnum, true, "enumType");

            var type_list = new Dictionary<object, string>();

            foreach (var value in Enum.GetValues(enumType))
            {
                if (includeDescription)
                {
                    var field_info = enumType.GetField(value.ToString());
                    var attr = (DescriptionAttribute)Attribute.GetCustomAttribute(field_info, typeof(DescriptionAttribute));

                    if (attr != null)
                        type_list.Add(value, attr.Description);
                    else
                        type_list.Add(value, null);
                }
                else
                    type_list.Add(value, null);
            }

            return type_list;
        }

        /// <summary>
        /// Returns a collection of string values that represent the Enum.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "enumType" must be of type enum.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "enumType" cannot be null.</exception>
        /// <param name="enumType">Type of Enum you want to create a collection from.</param>
        /// <returns>Collection of string values.</returns>
        public static IEnumerable<string> GetNames(this Type enumType)
        {
            Validation.Argument.Assert.IsNotNull(enumType, "enumType");
            Validation.Argument.Assert.IsValue(enumType.IsEnum, true, "enumType");
            
            return (
                from f in enumType.GetFields(BindingFlags.Public | BindingFlags.Static)
                where f.IsLiteral
                select f.Name);
        }

        /// <summary>
        /// Returns a collection of enum values from the specified Enum.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "enumType" must be of type enum.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "enumType" cannot be null.</exception>
        /// <param name="enumType">Type of Enum you want to create a collection from.</param>
        /// <returns>Collection of enum values.</returns>
        public static IEnumerable<object> GetEnums(this Type enumType)
        {
            Validation.Argument.Assert.IsNotNull(enumType, "enumType");
            Validation.Argument.Assert.IsValue(enumType.IsEnum, true, "enumType");

            return (
                from f in enumType.GetFields(BindingFlags.Public | BindingFlags.Static)
                where f.IsLiteral
                select f);
        }
        #endregion
    }
}
