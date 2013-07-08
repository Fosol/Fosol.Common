﻿using Fosol.Common.Extensions.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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
        /// Try to convert the value to the specified conversionType.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <param name="conversionType">Type to convert to.</param>
        /// <param name="result">Result of conversion.</param>
        /// <returns>True if successful.</returns>
        public static bool TryConvert(object value, Type conversionType, ref object result)
        {
            if (value == null)
            {
                if (conversionType.IsNullableType())
                {
                    result = null;
                    return true;
                }
                return false;
            }

            try
            {
                result = Convert.ChangeType(value, conversionType);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Try to convert the value to type T.
        /// </summary>
        /// <typeparam name="T">Type to convert to.</typeparam>
        /// <param name="value">Value to convert.</param>
        /// <param name="result">Result of conversion.</param>
        /// <returns>True if successful.</returns>
        public static bool TryConvert<T>(object value, ref T result)
        {
            var type = typeof(T);
            if (value == null)
            {
                if (type.IsNullableType())
                {
                    result = default(T);
                    return true;
                }
                return false;
            }

            try
            {
                result = (T)Convert.ChangeType(value, type);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Attempts to create a new instance of the specified Type.
        /// This method ensures that the specified Type (type) is assignable from type T.
        /// If the args contain string values that can be converted to enums it will try to convert them.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "typeName" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "typeName" cannot be null, or be an invalid type.</exception>
        /// <exception cref="System.InvalidOperationException">Unable to create a new instance of the specified type.</exception>
        /// <typeparam name="T">Type of object to create.</typeparam>
        /// <param name="type">The specified type to create.</param>
        /// <param name="args">Arguments to pass to the constructor.</param>
        /// <returns>New instance of the specified type.</returns>
        public static T ConstructObject<T>(Type type, params object[] args)
            where T : class
        {
            Validation.Assert.IsNotNull(type, "type");
            Validation.Assert.IsAssignableFromType(type, typeof(T), "baseType");

            T result = null;
            Exception exception = null;
            try
            {
                if (args != null || args.Length > 0)
                {
                    // Attempt to construct the object with the supplied arguments.
                    var constructor = type.GetConstructor(args.Select(a => a.GetType()).ToArray());
                    if (constructor != null)
                        result = (T)constructor.Invoke(args);
                    else
                    {
                        // Check for constructors that have enumerators.
                        // Looking for a constructor that matches the args.
                        var constructors = (
                            from c in type.GetConstructors()
                            from p in c.GetParameters()
                            where p.ParameterType.IsEnum
                                && c.GetParameters().Count() == args.Count()
                            select c);

                        // Try each constructor to find a match.
                        foreach (var ctor in constructors)
                        {
                            var pars = ctor.GetParameters();
                            var c_args = new object[args.Length];
                            var use_ctor = true;

                            // Use the constructor that works with the supplied args.
                            // Attempt to convert the specific arg when the parameter is an enum.
                            for (var i = 0; i < pars.Count(); i++)
                            {
                                if (pars[i].ParameterType == args[i].GetType())
                                    c_args[i] = args[i];
                                else if (pars[i].ParameterType.IsEnum && args[i] is string)
                                {
                                    try
                                    {
                                        // Convert to enum.
                                        c_args[i] = Enum.Parse(pars[i].ParameterType, (string)args[i], false);
                                    }
                                    catch (TargetInvocationException ex)
                                    {
                                        use_ctor = false;
                                        exception = ex.InnerException;
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        use_ctor = false;
                                        exception = ex;
                                        break;
                                    }
                                }
                                else
                                {
                                    use_ctor = false;
                                    break;
                                }
                            }

                            // Try to use this constructor.
                            if (use_ctor)
                            {
                                result = (T)ctor.Invoke(c_args);
                                break;
                            }
                        }
                    }
                }

                if (result == null)
                {
                    // Attempt to construct the object without arguments.
                    var constructor = type.GetConstructor(new Type[0]);
                    if (constructor == null)
                        throw new InvalidOperationException();
                    result = (T)constructor.Invoke(new object[0]);
                }
            }
            catch (TargetInvocationException ex)
            {
                exception = ex.InnerException;
            }

            if (result != null)
                return result;

            if (exception != null)
                throw new InvalidOperationException("", exception);
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Attempts to create a new instance of the specified Type.
        /// This method ensures that the specified Type (typeName) is assignable from type T.
        /// If the args contain string values that can be converted to enums it will try to convert them.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "typeName" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "typeName" cannot be null, or be an invalid type.</exception>
        /// <exception cref="System.InvalidOperationException">Unable to create a new instance of the specified type.</exception>
        /// <typeparam name="T">Type of object to create.</typeparam>
        /// <param name="typeName">The specified type to create.</param>
        /// <param name="args">Arguments to pass to the constructor.</param>
        /// <returns>New instance of the specified type.</returns>
        public static T ConstructObject<T>(string typeName, params object[] args)
            where T : class
        {
            Validation.Assert.IsNotNullOrEmpty(typeName, "typeName");
            var type = Type.GetType(typeName);
            Validation.Assert.IsNotNull(type, "typeName");
            return ConstructObject<T>(type, args);
        }
        #endregion
    }
}
