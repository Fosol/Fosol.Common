using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Extensions.Attributes
{
    /// <summary>
    /// Extension methods for attributes.
    /// </summary>
    public static class AttributeExtensions
    {
        #region Methods

        /// <summary>
        /// Determines if the object has the specified attribute type defined.
        /// </summary>
        /// <param name="element">Object to verify attribute against.</param>
        /// <param name="attributeType">Type of attribute to look for.</param>
        /// <param name="inherit">If true it will search ancestors of the object for the attribute.</param>
        /// <returns>True if the object has the attribute defined.</returns>
        public static bool HasAttribute(this object element, Type attributeType, bool inherit = false)
        {
            return Attribute.IsDefined(element.GetType(), attributeType, inherit);
        }

        /// <summary>
        /// Gets the custom attributes that have been defined for the object.
        /// </summary>
        /// <param name="element">Object to fetch attribute from.</param>
        /// <param name="attributeType">Type of attribute to look for.</param>
        /// <param name="inherit">If true it will search ancestors of the object for the attribute.</param>
        /// <returns>An array of Attribute.</returns>
        public static Attribute GetCustomAttribute(this object element, Type attributeType, bool inherit = false)
        {
            return Attribute.GetCustomAttribute(element.GetType(), attributeType, inherit);
        }

        /// <summary>
        /// Gets the custom attributes that have been defined for the object.
        /// </summary>
        /// <typeparam name="T">Type of attribute.</typeparam>
        /// <param name="element">Object to fetch attribute from.</param>
        /// <param name="inherit">If true it will search ancestors of the object for the attribute.</param>
        /// <returns>An array of Attribute.</returns>
        public static T GetCustomAttribute<T>(this object element, bool inherit = false)
            where T : Attribute
        {
            return (T)Attribute.GetCustomAttribute(element.GetType(), typeof(T), inherit);
        }

        /// <summary>
        /// Gets the custom attributes that have been defined for the object.
        /// </summary>
        /// <param name="element">Object to fetch attribute from.</param>
        /// <param name="attributeType">Type of attribute to look for.</param>
        /// <param name="inherit">If true it will search ancestors of the object for the attribute.</param>
        /// <returns>An array of Attribute.</returns>
        public static Attribute[] GetCustomAttributes(this object element, Type attributeType, bool inherit = false)
        {
            return Attribute.GetCustomAttributes(element.GetType(), attributeType, inherit);
        }

        /// <summary>
        /// Gets the custom attributes that have been defined for the object.
        /// </summary>
        /// <typeparam name="T">Type of attribute.</typeparam>
        /// <param name="element">Object to fetch attribute from.</param>
        /// <param name="attributeType">Type of attribute to look for.</param>
        /// <param name="inherit">If true it will search ancestors of the object for the attribute.</param>
        /// <returns>An array of Attribute.</returns>
        public static T[] GetCustomAttributes<T>(this object element, T attributeType, bool inherit = false)
            where T : Attribute
        {
            return Attribute.GetCustomAttributes(element.GetType(), typeof(T), inherit).Select(a => (T)a).ToArray();
        }

        /// <summary>
        /// Gets the custom attributes that have been defined for the object.
        /// </summary>
        /// <param name="element">Object to fetch attribute from.</param>
        /// <param name="inherit">If true it will search ancestors of the object for the attribute.</param>
        /// <returns>An array of Attribute.</returns>
        public static Attribute[] GetCustomAttributes(this object element, bool inherit = false)
        {
            return Attribute.GetCustomAttributes(element.GetType(), inherit);
        }

        /// <summary>
        /// Gets the custom attributes that have been defined for the object.
        /// </summary>
        /// <typeparam name="T">Type of attribute.</typeparam>
        /// <param name="element">Object to fetch attribute from.</param>
        /// <param name="inherit">If true it will search ancestors of the object for the attribute.</param>
        /// <returns>An array of Attribute.</returns>
        public static T[] GetCustomAttributes<T>(this object element, bool inherit = false)
            where T : Attribute
        {
            return Attribute.GetCustomAttributes(element.GetType(), inherit).Select(a => (T)a).ToArray();
        }
        #endregion
    }
}
