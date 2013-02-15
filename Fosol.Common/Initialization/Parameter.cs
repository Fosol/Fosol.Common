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
    public static class Parameter
    {
        #region Methods
        #region AssertDefault
        /// <summary>
        /// Assert that if the value is null that it is instead the defaultValue.
        /// Use this method if the object is automatically passed by reference.
        /// </summary>
        /// <param name="value">The parameter value to set to the default value if it's null.</param>
        /// <param name="defaultValue">Default value to use if the original value is null.</param>
        public static void AssertDefault(object value, object defaultValue)
        {
            if (value == null)
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is default(decimal) then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="defaultValue">Default value to use if the original value is 0.</param>
        public static void AssertDefault(ref decimal value, decimal defaultValue)
        {
            if (value == default(decimal))
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is default(double) then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="defaultValue">Default value to use if the original value is 0.</param>
        public static void AssertDefault(ref double value, double defaultValue)
        {
            if (value == default(double))
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is default(float) then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="defaultValue">Default value to use if the original value is 0.</param>
        public static void AssertDefault(ref float value, float defaultValue)
        {
            if (value == default(float))
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is default(int) then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="defaultValue">Default value to use if the original value is 0.</param>
        public static void AssertDefault(ref int value, int defaultValue)
        {
            if (value == default(int))
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is default(uint) then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="defaultValue">Default value to use if the original value is 0.</param>
        public static void AssertDefault(ref uint value, uint defaultValue)
        {
            if (value == default(uint))
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is default(long) then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="defaultValue">Default value to use if the original value is 0.</param>
        public static void AssertDefault(ref long value, long defaultValue)
        {
            if (value == default(long))
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is default(ulong) then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="defaultValue">Default value to use if the original value is 0.</param>
        public static void AssertDefault(ref ulong value, ulong defaultValue)
        {
            if (value == default(ulong))
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is default(short) then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="defaultValue">Default value to use if the original value is 0.</param>
        public static void AssertDefault(ref short value, short defaultValue)
        {
            if (value == default(short))
                value = defaultValue;
        }

        /// <summary>
        /// Assert that if the value is default(ushort) then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="defaultValue">Default value to use if the original value is 0.</param>
        public static void AssertDefault(ref ushort value, ushort defaultValue)
        {
            if (value == default(ushort))
                value = defaultValue;
        }
        #endregion

        #region AssertRange
        /// <summary>
        /// Assert that if the value is out of range then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="minValue">Minimum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="maxValue">Maximum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static void AssertRange(ref decimal value, decimal minValue, decimal maxValue, decimal defaultValue)
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
        public static void AssertRange(ref double value, double minValue, double maxValue, double defaultValue)
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
        public static void AssertRange(ref float value, float minValue, float maxValue, float defaultValue)
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
        public static void AssertRange(ref int value, int minValue, int maxValue, int defaultValue)
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
        public static void AssertRange(ref uint value, uint minValue, uint maxValue, uint defaultValue)
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
        public static void AssertRange(ref long value, long minValue, long maxValue, long defaultValue)
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
        public static void AssertRange(ref ulong value, ulong minValue, ulong maxValue, ulong defaultValue)
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
        public static void AssertRange(ref short value, short minValue, short maxValue, short defaultValue)
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
        public static void AssertRange(ref ushort value, ushort minValue, ushort maxValue, ushort defaultValue)
        {
            if (value < minValue || value > maxValue)
                value = defaultValue;
        }
        #endregion

        #region AssertMinRange
        /// <summary>
        /// Assert that if the value is less than the minValue then it should be set to the defaultValue.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="minValue">Minimum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static void AssertMinRange(ref decimal value, decimal minValue, decimal defaultValue)
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
        public static void AssertMinRange(ref double value, double minValue, double defaultValue)
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
        public static void AssertMinRange(ref float value, float minValue, float defaultValue)
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
        public static void AssertMinRange(ref int value, int minValue, int defaultValue)
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
        public static void AssertMinRange(ref uint value, uint minValue, uint defaultValue)
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
        public static void AssertMinRange(ref long value, long minValue, long defaultValue)
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
        public static void AssertMinRange(ref ulong value, ulong minValue, ulong defaultValue)
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
        public static void AssertMinRange(ref short value, short minValue, short defaultValue)
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
        public static void AssertMinRange(ref ushort value, ushort minValue, ushort defaultValue)
        {
            if (value < minValue)
                value = defaultValue;
        }
        #endregion

        #region AssertMaxRange
        /// <summary>
        /// Assert that if the value is greater than the maxValue set it to the defaultValue instead.
        /// </summary>
        /// <param name="value">The parameter value.</param>
        /// <param name="maxValue">Maximum value that the parameter is allowed to be before it uses the defaultValue.</param>
        /// <param name="defaultValue">Default value to use if the original value is out of the desired range.</param>
        public static void AssertMaxRange(ref decimal value, decimal maxValue, decimal defaultValue)
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
        public static void AssertMaxRange(ref double value, double maxValue, double defaultValue)
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
        public static void AssertMaxRange(ref float value, float maxValue, float defaultValue)
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
        public static void AssertMaxRange(ref int value, int maxValue, int defaultValue)
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
        public static void AssertMaxRange(ref uint value, uint maxValue, uint defaultValue)
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
        public static void AssertMaxRange(ref long value, long maxValue, long defaultValue)
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
        public static void AssertMaxRange(ref ulong value, ulong maxValue, ulong defaultValue)
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
        public static void AssertMaxRange(ref short value, short maxValue, short defaultValue)
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
        public static void AssertMaxRange(ref ushort value, ushort maxValue, ushort defaultValue)
        {
            if (value > maxValue)
                value = defaultValue;
        }
        #endregion

        #region AssertTryParse
        /// <summary>
        /// Asserts that the value uses the defaultValue if it cannot be parsed.
        /// </summary>
        /// <param name="value">Value to parse.</param>
        /// <param name="defaultValue">Default value to use if the parse fails.</param>
        /// <returns>Parsed value or default value.</returns>
        public static bool AssertTryParse(string value, bool defaultValue)
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
        public static byte AssertTryParse(string value, byte defaultValue)
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
        public static sbyte AssertTryParse(string value, sbyte defaultValue)
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
        public static char AssertTryParse(string value, char defaultValue)
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
        public static decimal AssertTryParse(string value, decimal defaultValue)
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
        public static double AssertTryParse(string value, double defaultValue)
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
        public static float AssertTryParse(string value, float defaultValue)
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
        public static int AssertTryParse(string value, int defaultValue)
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
        public static uint AssertTryParse(string value, uint defaultValue)
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
        public static long AssertTryParse(string value, long defaultValue)
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
        public static ulong AssertTryParse(string value, ulong defaultValue)
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
        public static short AssertTryParse(string value, short defaultValue)
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
        public static ushort AssertTryParse(string value, ushort defaultValue)
        {
            var result = defaultValue;
            ushort.TryParse(value, out result);
            return result;
        }
        #endregion
        #endregion
    }
}
