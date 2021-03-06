﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Extensions.Types
{
    /// <summary>
    /// Extension methods for Type objects.
    /// </summary>
    public static class TypeExtensions
    {

#if WINDOWS_APP || WINDOWS_PHONE_APP
        #region Methods
        public static bool IsSubclassOf(this Type type, Type subclassType)
        {
            return type.GetTypeInfo().IsSubclassOf(subclassType);
        }

        public static MemberInfo[] GetMember(this Type type, string name)
        {
            return type.GetTypeInfo().DeclaredMembers.Where(m => m.Name == name).ToArray();
        }
        #endregion
#else
        #region Methods
        /// <summary>
        /// Checks to see if the Type has a empty constructor.
        /// </summary>
        /// <param name="type">Type of object to test.</param>
        /// <returns>True if the object has an empty constructor.</returns>
        public static bool HasEmptyConstructor(this Type type)
        {
            return (
                type.GetConstructor(Type.EmptyTypes) != null
                || type.GetConstructors(BindingFlags.Instance | BindingFlags.Public)
                    .Any(c => c.GetParameters().All(p => p.IsOptional))
                );
        }

        /// <summary>
        /// Checks to see if the Type only has an empty constructor.
        /// </summary>
        /// <param name="type">Type of object to test.</param>
        /// <returns>True if the object only has an empty constructor.</returns>
        public static bool OnlyHasEmptyConstructor(this Type type)
        {
            return (
                type.GetConstructor(Type.EmptyTypes) != null
                && type.GetConstructors(BindingFlags.Instance | BindingFlags.Public).Count() <= 1
                );
        }

        /// <summary>
        /// Checks to see if the Type has a constructor with the specified Type.
        /// </summary>
        /// <param name="type">Type of object to test.</param>
        /// <param name="paramType">Parameter Type to test for.</param>
        /// <returns>True if the constructor exists.</returns>
        public static bool HasTypeConstructor(this Type type, Type paramType)
        {
            return HasTypeConstructor(type, new Type[] { paramType });
        }

        /// <summary>
        /// Checks to see if the Type has a constructor with the specified parameters of the specified Type.
        /// </summary>
        /// <param name="type">Type of object to test.</param>
        /// <param name="paramType">Array of parameter Type to test for.</param>
        /// <returns>True if the constructor exists.</returns>
        public static bool HasTypeConstructor(this Type type, params Type[] paramType)
        {
            foreach (var con in type.GetConstructors(BindingFlags.Instance | BindingFlags.Public))
            {
                var cp = con.GetParameters();

                // This constructor does not match.
                if (cp.Length != paramType.Length)
                    continue;

                for (var i = 0; i < paramType.Length; i++)
                {
                    if (!(cp[i].ParameterType == paramType[i]))
                        return false;
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if to see if the Type is of type Nullable.
        /// </summary>
        /// <param name="type">Type of object to test.</param>
        /// <returns>True if the Type is of type Nullable.</returns>
        public static bool IsNullableType(this Type type)
        {
            return type.IsGenericType
                && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }
        /// <summary>
        /// Determines if the Type has the specified attribute type defined.
        /// </summary>
        /// <param name="type">Type to verify attribute against.</param>
        /// <param name="attributeType">Type of attribute to look for.</param>
        /// <param name="inherit">If true it will search ancestors of the object for the attribute.</param>
        /// <returns>True if the object has the attribute defined.</returns>
        public static bool HasAttribute(this Type type, Type attributeType, bool inherit = false)
        {
            return Attribute.IsDefined(type, attributeType, inherit);
        }

        /// <summary>
        /// Gets the custom attributes that have been defined for the object.
        /// </summary>
        /// <param name="type">Type to fetch attribute from.</param>
        /// <param name="attributeType">Type of attribute to look for.</param>
        /// <param name="inherit">If true it will search ancestors of the object for the attribute.</param>
        /// <returns>An array of Attribute.</returns>
        public static Attribute GetCustomAttribute(this Type type, Type attributeType, bool inherit = false)
        {
            return Attribute.GetCustomAttribute(type, attributeType, inherit);
        }

        /// <summary>
        /// Gets the custom attributes that have been defined for the object.
        /// </summary>
        /// <typeparam name="T">Type of attribute.</typeparam>
        /// <param name="type">Type to fetch attribute from.</param>
        /// <param name="attributeType">Type of attribute to look for.</param>
        /// <param name="inherit">If true it will search ancestors of the object for the attribute.</param>
        /// <returns>An array of Attribute.</returns>
        public static T GetCustomAttribute<T>(this Type type, T attributeType, bool inherit = false)
            where T : Attribute
        {
            return (T)Attribute.GetCustomAttribute(type, typeof(T), inherit);
        }

        /// <summary>
        /// Gets the custom attributes that have been defined for the object.
        /// </summary>
        /// <param name="type">Type to fetch attribute from.</param>
        /// <param name="attributeType">Type of attribute to look for.</param>
        /// <param name="inherit">If true it will search ancestors of the object for the attribute.</param>
        /// <returns>An array of Attribute.</returns>
        public static Attribute[] GetCustomAttributes(this Type type, Type attributeType, bool inherit = false)
        {
            return Attribute.GetCustomAttributes(type, attributeType, inherit);
        }

        /// <summary>
        /// Gets the custom attributes that have been defined for the object.
        /// </summary>
        /// <typeparam name="T">Type of attribute.</typeparam>
        /// <param name="type">Type to fetch attribute from.</param>
        /// <param name="attributeType">Type of attribute to look for.</param>
        /// <param name="inherit">If true it will search ancestors of the object for the attribute.</param>
        /// <returns>An array of Attribute.</returns>
        public static T[] GetCustomAttributes<T>(this Type type, T attributeType, bool inherit = false)
            where T : Attribute
        {
            return Attribute.GetCustomAttributes(type, typeof(T), inherit).Select(a => (T)a).ToArray();
        }

        /// <summary>
        /// Gets the custom attributes that have been defined for the object.
        /// </summary>
        /// <param name="type">Type to fetch attribute from.</param>
        /// <param name="inherit">If true it will search ancestors of the object for the attribute.</param>
        /// <returns>An array of Attribute.</returns>
        public static Attribute[] GetCustomAttributes(this Type type, bool inherit = false)
        {
            return Attribute.GetCustomAttributes(type, inherit);
        }

        /// <summary>
        /// Gets the custom attributes that have been defined for the object.
        /// </summary>
        /// <typeparam name="T">Type of attribute.</typeparam>
        /// <param name="type">Type to fetch attribute from.</param>
        /// <param name="inherit">If true it will search ancestors of the object for the attribute.</param>
        /// <returns>An array of Attribute.</returns>
        public static T[] GetCustomAttributes<T>(this Type type, bool inherit = false)
            where T : Attribute
        {
            return Attribute.GetCustomAttributes(type, inherit).Select(a => (T)a).ToArray();
        }

        /// <summary>
        /// Determines if the specified type is a static class.
        /// </summary>
        /// <param name="type">Type you want to check.</param>
        /// <returns>True if the type is a static class.</returns>
        public static bool IsStatic(this Type type)
        {
            return type.IsAbstract && type.IsSealed;
        }

        /// <summary>
        /// Get the generic element type of the specified Type.
        /// </summary>
        /// <param name="type">Type object to get the element type of.</param>
        /// <returns>The element type of the specified Type.</returns>
        public static Type GetElementType(this Type type)
        {
            var is_enum = type.FindIEnumerable();
            if (is_enum == null)
                return type;

            return is_enum.GetGenericArguments()[0];
        }

        /// <summary>
        /// When a Type is an IEnumerable we want to get the generic element type.
        /// </summary>
        /// <param name="type">Type to search for the generic type.</param>
        /// <returns>The IEnumerable generic element type.</returns>
        private static Type FindIEnumerable(this Type type)
        {
            if (type == null || type == typeof(string))
                return null;
            if (type.IsArray)
                return typeof(IEnumerable<>).MakeGenericType(type.GetElementType());
            if (type.IsGenericType)
            {
                foreach (var arg in type.GetGenericArguments())
                {
                    var is_enum = typeof(IEnumerable<>).MakeGenericType(arg);
                    if (is_enum.IsAssignableFrom(type))
                        return is_enum;
                }
            }

            var ifaces = type.GetInterfaces();
            if (ifaces != null && ifaces.Length > 0)
            {
                foreach (var iface in ifaces)
                {
                    var is_enum = iface.FindIEnumerable();
                    if (is_enum != null)
                        return is_enum;
                }
            }

            if (type.BaseType != null && type.BaseType != typeof(object))
                return type.BaseType.FindIEnumerable();

            return null;
        }
        #endregion
#endif
    }
}
