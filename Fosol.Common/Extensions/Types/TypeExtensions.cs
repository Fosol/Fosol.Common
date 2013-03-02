using System;
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
        /// Checks if to see if the Type is of type Nullable<>.
        /// </summary>
        /// <param name="type">Type of object to test.</param>
        /// <returns>True if the Type is of type Nullable.</returns>
        public static bool IsNullableType(this Type type)
        {
            return type.IsGenericType
                && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }
        #endregion
    }
}
