using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Initialization
{
    /// <summary>
    /// Methods to assist in initialization of parameters.
    /// </summary>
    public static class Assert
    {
        #region Reference Methods
        #region IsNotDefault
        /// <summary>
        /// Assert that if the value is null that it is instead the defaultValue.
        /// Use this method if the object is automatically passed by reference.
        /// </summary>
        /// <param name="value">The parameter value to set to the default value if it's null.</param>
        /// <param name="defaultValue">Default value to use if the original value is null.</param>
        public static void IsNotDefault(ref object value, object defaultValue)
        {
            if (value == null)
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is null that it is instead the defaultValue.
        /// Use this method if the object is automatically passed by reference.
        /// </summary>
        /// <typeparam name="T">Type of object value.</typeparam>
        /// <param name="value">The parameter value to set to the default value if it's null.</param>
        /// <param name="defaultValue">Default value to use if the original value is null.</param>
        public static void IsNotDefault<T>(ref T value, T defaultValue)
        {
            if (value == null)
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is default(decimal) then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="defaultValue">Default value to use if the original value is 0.</param>
        public static void IsNotDefault(ref decimal value, decimal defaultValue)
        {
            if (value == default(decimal))
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is default(double) then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="defaultValue">Default value to use if the original value is 0.</param>
        public static void IsNotDefault(ref double value, double defaultValue)
        {
            if (value == default(double))
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is default(float) then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="defaultValue">Default value to use if the original value is 0.</param>
        public static void IsNotDefault(ref float value, float defaultValue)
        {
            if (value == default(float))
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is default(int) then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="defaultValue">Default value to use if the original value is 0.</param>
        public static void IsNotDefault(ref int value, int defaultValue)
        {
            if (value == default(int))
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is default(uint) then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="defaultValue">Default value to use if the original value is 0.</param>
        public static void IsNotDefault(ref uint value, uint defaultValue)
        {
            if (value == default(uint))
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is default(long) then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="defaultValue">Default value to use if the original value is 0.</param>
        public static void IsNotDefault(ref long value, long defaultValue)
        {
            if (value == default(long))
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is default(ulong) then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="defaultValue">Default value to use if the original value is 0.</param>
        public static void IsNotDefault(ref ulong value, ulong defaultValue)
        {
            if (value == default(ulong))
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is default(short) then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="defaultValue">Default value to use if the original value is 0.</param>
        public static void IsNotDefault(ref short value, short defaultValue)
        {
            if (value == default(short))
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is default(ushort) then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="defaultValue">Default value to use if the original value is 0.</param>
        public static void IsNotDefault(ref ushort value, ushort defaultValue)
        {
            if (value == default(ushort))
                value = defaultValue;
        }
        #endregion

        #region IsNotEmpty
        /// <summary>
        /// Assert that if the value is empty that it is instead the defaultValue.
        /// Use this method if the object is automatically passed by reference.
        /// </summary>
        /// <param name="value">The parameter value to set to the default value if it's null.</param>
        /// <param name="defaultValue">Default value to use if the original value is null.</param>
        public static void IsNotEmpty(ref string value, string defaultValue)
        {
            if (value == String.Empty)
                value = defaultValue;
        }
        #endregion

        #region IsNotDefaultOrEmpty
        /// <summary>
        /// Assert that if the value is null or empty that it is instead the defaultValue.
        /// Use this method if the object is automatically passed by reference.
        /// </summary>
        /// <param name="value">The parameter value to set to the default value if it's null.</param>
        /// <param name="defaultValue">Default value to use if the original value is null.</param>
        public static void IsNotDefaultOrEmpty(ref string value, string defaultValue)
        {
            if (String.IsNullOrEmpty(value))
                value = defaultValue;
        }
        #endregion

        #region IsNotDefaultOrEmptyOrWhitespace
        /// <summary>
        /// Assert that if the value is null, empty or whitespace that it is instead the defaultValue.
        /// Use this method if the object is automatically passed by reference.
        /// </summary>
        /// <param name="value">The parameter value to set to the default value if it's null.</param>
        /// <param name="defaultValue">Default value to use if the original value is null.</param>
        public static void IsNotDefaultOrEmptyOrWhitespace(ref string value, string defaultValue)
        {
            if (String.IsNullOrWhiteSpace(value))
                value = defaultValue;
        }
        #endregion

        #region Range
        /// <summary>
        /// Assert that if the value is out of range then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="minValue">Minimum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="maxValue">Maximum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static void Range(ref decimal value, decimal minValue, decimal maxValue, decimal defaultValue)
        {
            if (value < minValue || value > maxValue)
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is out of range then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="minValue">Minimum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="maxValue">Maximum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static void Range(ref double value, double minValue, double maxValue, double defaultValue)
        {
            if (value < minValue || value > maxValue)
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is out of range then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="minValue">Minimum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="maxValue">Maximum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static void Range(ref float value, float minValue, float maxValue, float defaultValue)
        {
            if (value < minValue || value > maxValue)
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is out of range then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="minValue">Minimum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="maxValue">Maximum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static void Range(ref int value, int minValue, int maxValue, int defaultValue)
        {
            if (value < minValue || value > maxValue)
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is out of range then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="minValue">Minimum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="maxValue">Maximum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static void Range(ref uint value, uint minValue, uint maxValue, uint defaultValue)
        {
            if (value < minValue || value > maxValue)
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is out of range then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="minValue">Minimum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="maxValue">Maximum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static void Range(ref long value, long minValue, long maxValue, long defaultValue)
        {
            if (value < minValue || value > maxValue)
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is out of range then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="minValue">Minimum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="maxValue">Maximum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static void Range(ref ulong value, ulong minValue, ulong maxValue, ulong defaultValue)
        {
            if (value < minValue || value > maxValue)
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is out of range then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="minValue">Minimum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="maxValue">Maximum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static void Range(ref short value, short minValue, short maxValue, short defaultValue)
        {
            if (value < minValue || value > maxValue)
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is out of range then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="minValue">Minimum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="maxValue">Maximum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static void Range(ref ushort value, ushort minValue, ushort maxValue, ushort defaultValue)
        {
            if (value < minValue || value > maxValue)
                value = defaultValue;
        }
        #endregion

        #region MinRange
        /// <summary>
        /// Assert that if the value is less than the minValue then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="minValue">Minimum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static void MinRange(ref decimal value, decimal minValue, decimal defaultValue)
        {
            if (value < minValue)
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is less than the minValue then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="minValue">Minimum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static void MinRange(ref double value, double minValue, double defaultValue)
        {
            if (value < minValue)
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is less than the minValue then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="minValue">Minimum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static void MinRange(ref float value, float minValue, float defaultValue)
        {
            if (value < minValue)
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is less than the minValue then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="minValue">Minimum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static void MinRange(ref int value, int minValue, int defaultValue)
        {
            if (value < minValue)
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is less than the minValue then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="minValue">Minimum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static void MinRange(ref uint value, uint minValue, uint defaultValue)
        {
            if (value < minValue)
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is less than the minValue then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="minValue">Minimum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static void MinRange(ref long value, long minValue, long defaultValue)
        {
            if (value < minValue)
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is less than the minValue then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="minValue">Minimum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static void MinRange(ref ulong value, ulong minValue, ulong defaultValue)
        {
            if (value < minValue)
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is less than the minValue then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="minValue">Minimum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static void MinRange(ref short value, short minValue, short defaultValue)
        {
            if (value < minValue)
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is less than the minValue then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="minValue">Minimum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static void MinRange(ref ushort value, ushort minValue, ushort defaultValue)
        {
            if (value < minValue)
                value = defaultValue;
        }
        #endregion

        #region MaxRange
        /// <summary>
        /// Assert that if the value is greater than the maxValue set it to the defaultValue instead.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="maxValue">Maximum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static void MaxRange(ref decimal value, decimal maxValue, decimal defaultValue)
        {
            if (value > maxValue)
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is greater than the maxValue set it to the defaultValue instead.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="maxValue">Maximum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static void MaxRange(ref double value, double maxValue, double defaultValue)
        {
            if (value > maxValue)
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is greater than the maxValue set it to the defaultValue instead.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="maxValue">Maximum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static void MaxRange(ref float value, float maxValue, float defaultValue)
        {
            if (value > maxValue)
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is greater than the maxValue set it to the defaultValue instead.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="maxValue">Maximum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static void MaxRange(ref int value, int maxValue, int defaultValue)
        {
            if (value > maxValue)
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is greater than the maxValue set it to the defaultValue instead.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="maxValue">Maximum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static void MaxRange(ref uint value, uint maxValue, uint defaultValue)
        {
            if (value > maxValue)
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is greater than the maxValue set it to the defaultValue instead.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="maxValue">Maximum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static void MaxRange(ref long value, long maxValue, long defaultValue)
        {
            if (value > maxValue)
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is greater than the maxValue set it to the defaultValue instead.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="maxValue">Maximum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static void MaxRange(ref ulong value, ulong maxValue, ulong defaultValue)
        {
            if (value > maxValue)
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is greater than the maxValue set it to the defaultValue instead.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="maxValue">Maximum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static void MaxRange(ref short value, short maxValue, short defaultValue)
        {
            if (value > maxValue)
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is greater than the maxValue set it to the defaultValue instead.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="maxValue">Maximum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static void MaxRange(ref ushort value, ushort maxValue, ushort defaultValue)
        {
            if (value > maxValue)
                value = defaultValue;
        }
        #endregion
        #endregion

        #region Value Methods
        #region IsNotDefault
        /// <summary>
        /// Assert that if the value is null that it is instead the defaultValue.
        /// Use this method if the object is automatically passed by reference.
        /// </summary>
        /// <param name="value">The parameter value to set to the default value if it's null.</param>
        /// <param name="defaultValue">Default value to use if the original value is null.</param>
        public static object IsNotDefault(object value, object defaultValue)
        {
            if (value == null)
                return defaultValue;

            return value;
        }

        /// <summary>
        /// Assert that if the value is null that it is instead the defaultValue.
        /// Use this method if the object is automatically passed by reference.
        /// </summary>
        /// <typeparam name="T">Type of object value.</typeparam>
        /// <param name="value">The parameter value to set to the default value if it's null.</param>
        /// <param name="defaultValue">Default value to use if the original value is null.</param>
        public static T IsNotDefault<T>(T value, T defaultValue)
        {
            if (value == null)
                return defaultValue;

            return value;
        }

        /// <summary>
        /// Assert that if the value is default(decimal) then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="defaultValue">Default value to use if the original value is 0.</param>
        public static decimal IsNotDefault(decimal value, decimal defaultValue)
        {
            if (value == default(decimal))
                return defaultValue;

            return value;
        }

        /// <summary>
        /// Assert that if the value is default(double) then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="defaultValue">Default value to use if the original value is 0.</param>
        public static double IsNotDefault(double value, double defaultValue)
        {
            if (value == default(double))
                return defaultValue;

            return value;
        }

        /// <summary>
        /// Assert that if the value is default(float) then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="defaultValue">Default value to use if the original value is 0.</param>
        public static float IsNotDefault(float value, float defaultValue)
        {
            if (value == default(float))
                return defaultValue;

            return value;
        }

        /// <summary>
        /// Assert that if the value is default(int) then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="defaultValue">Default value to use if the original value is 0.</param>
        public static int IsNotDefault(int value, int defaultValue)
        {
            if (value == default(int))
                return defaultValue;

            return value;
        }

        /// <summary>
        /// Assert that if the value is default(uint) then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="defaultValue">Default value to use if the original value is 0.</param>
        public static uint IsNotDefault(uint value, uint defaultValue)
        {
            if (value == default(uint))
                return defaultValue;

            return value;
        }

        /// <summary>
        /// Assert that if the value is default(long) then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="defaultValue">Default value to use if the original value is 0.</param>
        public static long IsNotDefault(long value, long defaultValue)
        {
            if (value == default(long))
                return defaultValue;

            return value;
        }

        /// <summary>
        /// Assert that if the value is default(ulong) then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="defaultValue">Default value to use if the original value is 0.</param>
        public static ulong IsNotDefault(ulong value, ulong defaultValue)
        {
            if (value == default(ulong))
                return defaultValue;

            return value;
        }

        /// <summary>
        /// Assert that if the value is default(short) then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="defaultValue">Default value to use if the original value is 0.</param>
        public static short IsNotDefault(short value, short defaultValue)
        {
            if (value == default(short))
                return defaultValue;

            return value;
        }

        /// <summary>
        /// Assert that if the value is default(ushort) then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="defaultValue">Default value to use if the original value is 0.</param>
        public static ushort IsNotDefault(ushort value, ushort defaultValue)
        {
            if (value == default(ushort))
                return defaultValue;

            return value;
        }
        #endregion

        #region IsNotEmpty
        /// <summary>
        /// Assert that if the value is empty that it is instead the defaultValue.
        /// Use this method if the object is automatically passed by reference.
        /// </summary>
        /// <param name="value">The parameter value to set to the default value if it's null.</param>
        /// <param name="defaultValue">Default value to use if the original value is null.</param>
        public static string IsNotEmpty(string value, string defaultValue)
        {
            if (value == String.Empty)
                return defaultValue;

            return value;
        }
        #endregion

        #region IsNotDefaultOrEmpty
        /// <summary>
        /// Assert that if the value is null or empty that it is instead the defaultValue.
        /// Use this method if the object is automatically passed by reference.
        /// </summary>
        /// <param name="value">The parameter value to set to the default value if it's null.</param>
        /// <param name="defaultValue">Default value to use if the original value is null.</param>
        public static string IsNotDefaultOrEmpty(string value, string defaultValue)
        {
            if (String.IsNullOrEmpty(value))
                return defaultValue;

            return value;
        }
        #endregion

        #region IsNotDefaultOrEmptyOrWhitespace
        /// <summary>
        /// Assert that if the value is null, empty or whitespace that it is instead the defaultValue.
        /// Use this method if the object is automatically passed by reference.
        /// </summary>
        /// <param name="value">The parameter value to set to the default value if it's null.</param>
        /// <param name="defaultValue">Default value to use if the original value is null.</param>
        public static string IsNotDefaultOrEmptyOrWhitespace(string value, string defaultValue)
        {
            if (String.IsNullOrWhiteSpace(value))
                return defaultValue;

            return value;
        }
        #endregion

        #region Range
        /// <summary>
        /// Assert that if the value is out of range then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="minValue">Minimum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="maxValue">Maximum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static decimal Range(decimal value, decimal minValue, decimal maxValue, decimal defaultValue)
        {
            if (value < minValue || value > maxValue)
                return defaultValue;

            return value;
        }

        /// <summary>
        /// Assert that if the value is out of range then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="minValue">Minimum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="maxValue">Maximum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static double Range(double value, double minValue, double maxValue, double defaultValue)
        {
            if (value < minValue || value > maxValue)
                return defaultValue;

            return value;
        }

        /// <summary>
        /// Assert that if the value is out of range then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="minValue">Minimum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="maxValue">Maximum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static float Range(float value, float minValue, float maxValue, float defaultValue)
        {
            if (value < minValue || value > maxValue)
                return defaultValue;

            return value;
        }

        /// <summary>
        /// Assert that if the value is out of range then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="minValue">Minimum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="maxValue">Maximum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static int Range(int value, int minValue, int maxValue, int defaultValue)
        {
            if (value < minValue || value > maxValue)
                return defaultValue;

            return value;
        }

        /// <summary>
        /// Assert that if the value is out of range then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="minValue">Minimum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="maxValue">Maximum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static uint Range(uint value, uint minValue, uint maxValue, uint defaultValue)
        {
            if (value < minValue || value > maxValue)
                return defaultValue;

            return value;
        }

        /// <summary>
        /// Assert that if the value is out of range then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="minValue">Minimum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="maxValue">Maximum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static long Range(long value, long minValue, long maxValue, long defaultValue)
        {
            if (value < minValue || value > maxValue)
                return defaultValue;

            return value;
        }

        /// <summary>
        /// Assert that if the value is out of range then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="minValue">Minimum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="maxValue">Maximum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static ulong Range(ulong value, ulong minValue, ulong maxValue, ulong defaultValue)
        {
            if (value < minValue || value > maxValue)
                return defaultValue;

            return value;
        }

        /// <summary>
        /// Assert that if the value is out of range then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="minValue">Minimum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="maxValue">Maximum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static short Range(short value, short minValue, short maxValue, short defaultValue)
        {
            if (value < minValue || value > maxValue)
                return defaultValue;

            return value;
        }

        /// <summary>
        /// Assert that if the value is out of range then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="minValue">Minimum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="maxValue">Maximum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static ushort Range(ushort value, ushort minValue, ushort maxValue, ushort defaultValue)
        {
            if (value < minValue || value > maxValue)
                return defaultValue;

            return value;
        }
        #endregion

        #region MinRange
        /// <summary>
        /// Assert that if the value is less than the minValue then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="minValue">Minimum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static decimal MinRange(decimal value, decimal minValue, decimal defaultValue)
        {
            if (value < minValue)
                return defaultValue;

            return value;
        }

        /// <summary>
        /// Assert that if the value is less than the minValue then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="minValue">Minimum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static double MinRange(double value, double minValue, double defaultValue)
        {
            if (value < minValue)
                return defaultValue;

            return value;
        }

        /// <summary>
        /// Assert that if the value is less than the minValue then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="minValue">Minimum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static float MinRange(float value, float minValue, float defaultValue)
        {
            if (value < minValue)
                return defaultValue;

            return value;
        }

        /// <summary>
        /// Assert that if the value is less than the minValue then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="minValue">Minimum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static int MinRange(int value, int minValue, int defaultValue)
        {
            if (value < minValue)
                return defaultValue;

            return value;
        }

        /// <summary>
        /// Assert that if the value is less than the minValue then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="minValue">Minimum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static uint MinRange(uint value, uint minValue, uint defaultValue)
        {
            if (value < minValue)
                return defaultValue;

            return value;
        }

        /// <summary>
        /// Assert that if the value is less than the minValue then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="minValue">Minimum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static long MinRange(long value, long minValue, long defaultValue)
        {
            if (value < minValue)
                return defaultValue;

            return value;
        }

        /// <summary>
        /// Assert that if the value is less than the minValue then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="minValue">Minimum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static ulong MinRange(ulong value, ulong minValue, ulong defaultValue)
        {
            if (value < minValue)
                return defaultValue;

            return value;
        }

        /// <summary>
        /// Assert that if the value is less than the minValue then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="minValue">Minimum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static short MinRange(short value, short minValue, short defaultValue)
        {
            if (value < minValue)
                return defaultValue;

            return value;
        }

        /// <summary>
        /// Assert that if the value is less than the minValue then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="minValue">Minimum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static ushort MinRange(ushort value, ushort minValue, ushort defaultValue)
        {
            if (value < minValue)
                return defaultValue;

            return value;
        }
        #endregion

        #region MaxRange
        /// <summary>
        /// Assert that if the value is greater than the maxValue set it to the defaultValue instead.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="maxValue">Maximum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static decimal MaxRange(decimal value, decimal maxValue, decimal defaultValue)
        {
            if (value > maxValue)
                return defaultValue;

            return value;
        }

        /// <summary>
        /// Assert that if the value is greater than the maxValue set it to the defaultValue instead.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="maxValue">Maximum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static double MaxRange(double value, double maxValue, double defaultValue)
        {
            if (value > maxValue)
                return defaultValue;

            return value;
        }

        /// <summary>
        /// Assert that if the value is greater than the maxValue set it to the defaultValue instead.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="maxValue">Maximum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static float MaxRange(float value, float maxValue, float defaultValue)
        {
            if (value > maxValue)
                return defaultValue;

            return value;
        }

        /// <summary>
        /// Assert that if the value is greater than the maxValue set it to the defaultValue instead.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="maxValue">Maximum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static int MaxRange(int value, int maxValue, int defaultValue)
        {
            if (value > maxValue)
                return defaultValue;

            return value;
        }

        /// <summary>
        /// Assert that if the value is greater than the maxValue set it to the defaultValue instead.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="maxValue">Maximum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static uint MaxRange(uint value, uint maxValue, uint defaultValue)
        {
            if (value > maxValue)
                return defaultValue;

            return value;
        }

        /// <summary>
        /// Assert that if the value is greater than the maxValue set it to the defaultValue instead.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="maxValue">Maximum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static long MaxRange(long value, long maxValue, long defaultValue)
        {
            if (value > maxValue)
                return defaultValue;

            return value;
        }

        /// <summary>
        /// Assert that if the value is greater than the maxValue set it to the defaultValue instead.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="maxValue">Maximum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static ulong MaxRange(ulong value, ulong maxValue, ulong defaultValue)
        {
            if (value > maxValue)
                return defaultValue;

            return value;
        }

        /// <summary>
        /// Assert that if the value is greater than the maxValue set it to the defaultValue instead.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="maxValue">Maximum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static short MaxRange(short value, short maxValue, short defaultValue)
        {
            if (value > maxValue)
                return defaultValue;

            return value;
        }

        /// <summary>
        /// Assert that if the value is greater than the maxValue set it to the defaultValue instead.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="maxValue">Maximum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static ushort MaxRange(ushort value, ushort maxValue, ushort defaultValue)
        {
            if (value > maxValue)
                return defaultValue;

            return value;
        }
        #endregion
        #endregion

        #region Convert
        /// <summary>
        /// Attempts to convert the value to the desired type and return the result.
        /// If it fails it resubmits the exception with a valid message which will contain the parameter name.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Parameter 'value' cannot be null.</exception>
        /// <exception cref="System.FormatException">Parameter 'value' must have a valid format to covert.</exception>
        /// <exception cref="System.InvalidCastException">Parameter 'value' must be convertable to the desired type.</exception>
        /// <exception cref="System.OverflowException">Parameter 'value' must be within a valid range for the desired type.</exception>
        /// <typeparam name="T">Type you want to convert the value into.</typeparam>
        /// <param name="value">Value to convert from.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">Description of the error if it fails to convert the value.</param>
        public static T Convert<T>(object value, string paramName, string message = null)
        {
            try
            {
                return (T)System.Convert.ChangeType(value, typeof(T));
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException(string.Format(message ?? Resources.Multilingual.Initialization_Convert_ArgumentNullException, paramName), paramName);
            }
            catch (InvalidCastException ex)
            {
                throw new InvalidCastException(string.Format(message ?? Resources.Multilingual.Iniitialization_Convert_InvalidCastException, paramName, typeof(T).Name), ex);
            }
            catch (OverflowException ex)
            {
                throw new OverflowException(string.Format(message ?? Resources.Multilingual.Initialization_Convert_OverflowException, paramName, typeof(T).Name), ex);
            }
            catch (FormatException ex)
            {
                throw new FormatException(string.Format(message ?? Resources.Multilingual.Initialization_Convert_FormatException, paramName, typeof(T)), ex);
            }
        }
        #endregion

        #region TryParse
        /// <summary>
        /// Asserts that the value uses the defaultValue if it cannot be parsed.
        /// </summary>
        /// <param name="value">Value to parse.</param>
        /// <param name="defaultValue">Default value to use if the parse fails.</param>
        /// <returns>Parsed value or default value.</returns>
        public static bool TryParse(string value, bool defaultValue)
        {
            var result = defaultValue;
            bool.TryParse(value, out result);
            return result;
        }

        /// <summary>
        /// Asserts that the value uses the defaultValue if it cannot be parsed.
        /// </summary>
        /// <param name="value">Value to parse.</param>
        /// <param name="defaultValue">Default value to use if the parse fails.</param>
        /// <returns>Parsed value or default value.</returns>
        public static byte TryParse(string value, byte defaultValue)
        {
            var result = defaultValue;
            byte.TryParse(value, out result);
            return result;
        }

        /// <summary>
        /// Asserts that the value uses the defaultValue if it cannot be parsed.
        /// </summary>
        /// <param name="value">Value to parse.</param>
        /// <param name="defaultValue">Default value to use if the parse fails.</param>
        /// <returns>Parsed value or default value.</returns>
        public static sbyte TryParse(string value, sbyte defaultValue)
        {
            var result = defaultValue;
            sbyte.TryParse(value, out result);
            return result;
        }

        /// <summary>
        /// Asserts that the value uses the defaultValue if it cannot be parsed.
        /// </summary>
        /// <param name="value">Value to parse.</param>
        /// <param name="defaultValue">Default value to use if the parse fails.</param>
        /// <returns>Parsed value or default value.</returns>
        public static char TryParse(string value, char defaultValue)
        {
            var result = defaultValue;
            char.TryParse(value, out result);
            return result;
        }

        /// <summary>
        /// Asserts that the value uses the defaultValue if it cannot be parsed.
        /// </summary>
        /// <param name="value">Value to parse.</param>
        /// <param name="defaultValue">Default value to use if the parse fails.</param>
        /// <returns>Parsed value or default value.</returns>
        public static decimal TryParse(string value, decimal defaultValue)
        {
            var result = defaultValue;
            decimal.TryParse(value, out result);
            return result;
        }

        /// <summary>
        /// Asserts that the value uses the defaultValue if it cannot be parsed.
        /// </summary>
        /// <param name="value">Value to parse.</param>
        /// <param name="defaultValue">Default value to use if the parse fails.</param>
        /// <returns>Parsed value or default value.</returns>
        public static double TryParse(string value, double defaultValue)
        {
            var result = defaultValue;
            double.TryParse(value, out result);
            return result;
        }

        /// <summary>
        /// Asserts that the value uses the defaultValue if it cannot be parsed.
        /// </summary>
        /// <param name="value">Value to parse.</param>
        /// <param name="defaultValue">Default value to use if the parse fails.</param>
        /// <returns>Parsed value or default value.</returns>
        public static float TryParse(string value, float defaultValue)
        {
            var result = defaultValue;
            float.TryParse(value, out result);
            return result;
        }

        /// <summary>
        /// Asserts that the value uses the defaultValue if it cannot be parsed.
        /// </summary>
        /// <param name="value">Value to parse.</param>
        /// <param name="defaultValue">Default value to use if the parse fails.</param>
        /// <returns>Parsed value or default value.</returns>
        public static int TryParse(string value, int defaultValue)
        {
            var result = defaultValue;
            int.TryParse(value, out result);
            return result;
        }

        /// <summary>
        /// Asserts that the value uses the defaultValue if it cannot be parsed.
        /// </summary>
        /// <param name="value">Value to parse.</param>
        /// <param name="defaultValue">Default value to use if the parse fails.</param>
        /// <returns>Parsed value or default value.</returns>
        public static uint TryParse(string value, uint defaultValue)
        {
            var result = defaultValue;
            uint.TryParse(value, out result);
            return result;
        }

        /// <summary>
        /// Asserts that the value uses the defaultValue if it cannot be parsed.
        /// </summary>
        /// <param name="value">Value to parse.</param>
        /// <param name="defaultValue">Default value to use if the parse fails.</param>
        /// <returns>Parsed value or default value.</returns>
        public static long TryParse(string value, long defaultValue)
        {
            var result = defaultValue;
            long.TryParse(value, out result);
            return result;
        }

        /// <summary>
        /// Asserts that the value uses the defaultValue if it cannot be parsed.
        /// </summary>
        /// <param name="value">Value to parse.</param>
        /// <param name="defaultValue">Default value to use if the parse fails.</param>
        /// <returns>Parsed value or default value.</returns>
        public static ulong TryParse(string value, ulong defaultValue)
        {
            var result = defaultValue;
            ulong.TryParse(value, out result);
            return result;
        }

        /// <summary>
        /// Asserts that the value uses the defaultValue if it cannot be parsed.
        /// </summary>
        /// <param name="value">Value to parse.</param>
        /// <param name="defaultValue">Default value to use if the parse fails.</param>
        /// <returns>Parsed value or default value.</returns>
        public static short TryParse(string value, short defaultValue)
        {
            var result = defaultValue;
            short.TryParse(value, out result);
            return result;
        }

        /// <summary>
        /// Asserts that the value uses the defaultValue if it cannot be parsed.
        /// </summary>
        /// <param name="value">Value to parse.</param>
        /// <param name="defaultValue">Default value to use if the parse fails.</param>
        /// <returns>Parsed value or default value.</returns>
        public static ushort TryParse(string value, ushort defaultValue)
        {
            var result = defaultValue;
            ushort.TryParse(value, out result);
            return result;
        }
        #endregion
    }
}
