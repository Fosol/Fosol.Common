using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Mathematics
{
    /// <summary>
    /// Helpful Math formulas.
    /// </summary>
    public static class Formula
    {
        #region Methods
        #region Ratio
        /// <summary>
        /// calculates the ratio difference between the two values.
        /// </summary>
        /// <param name="val1">Value one.</param>
        /// <param name="val2">Value two.</param>
        /// <returns>Ratio difference between two values.</returns>
        public static float Ratio(short val1, short val2)
        {
            return (float)val1 / val2;
        }

        /// <summary>
        /// calculates the ratio difference between the two values.
        /// </summary>
        /// <param name="val1">Value one.</param>
        /// <param name="val2">Value two.</param>
        /// <returns>Ratio difference between two values.</returns>
        public static float Ratio(int val1, int val2)
        {
            return (float)val1 / val2;
        }

        /// <summary>
        /// calculates the ratio difference between the two values.
        /// </summary>
        /// <param name="val1">Value one.</param>
        /// <param name="val2">Value two.</param>
        /// <returns>Ratio difference between two values.</returns>
        public static float Ratio(long val1, long val2)
        {
            return (float)val1 / val2;
        }
        #endregion

        #region MinRatio
        /// <summary>
        /// Determine which value is the smallest ratio difference from 1.
        /// </summary>
        /// <example>
        /// (3/4, 2/5) = (15/20, 8/20) = (0.75, 0.4) = 0.75
        /// 
        /// (0.75, 1.3) = (75/100, 1 3/10) = (3/4, 13/10) = (30/40, 52/40) = (10, 12) = 10
        /// 1 - 0.75 = 0.25
        /// 1.3 - 1 = 0.3
        /// </example>
        /// <param name="ratio1">Ratio value one.</param>
        /// <param name="ratio2">Ratio value two.</param>
        /// <returns>The smaller ratio difference.</returns>
        public static float MinRatio(float ratio1, float ratio2)
        {
            // These ratios are smaller, so which ever is closer to 1.
            if (ratio1 < 1 && ratio2 < 1)
                return Math.Max(ratio1, ratio2);
            // Ratio1 is less than 1, ratio2 is greater or equal to 1.
            else if (ratio1 < 1)
            {
                var r1 = 1 - ratio1;
                var r2 = ratio2 - 1;
                var r = Math.Min(r1, r2);
                return (r == r1) ? ratio1 : ratio2;
            }
            // Ratio1 is less than 1, ratio2 is greater or equal to 1.
            else if (ratio2 < 1)
            {
                var r1 = ratio1 - 1;
                var r2 = 1 - ratio2;
                var r = Math.Min(r1, r2);
                return (r == r1) ? ratio1 : ratio2;
            }
            // Both ratios are greater then or equal to 1.
            else
                return Math.Min(ratio1, ratio2);
        }

        /// <summary>
        /// Determine which value is the smallest ratio difference from 1.
        /// </summary>
        /// <example>
        /// (3/4, 2/5) = (15/20, 8/20) = (0.75, 0.4) = 0.75
        /// 
        /// (0.75, 1.3) = (75/100, 1 3/10) = (3/4, 13/10) = (30/40, 52/40) = (10, 12) = 10
        /// 1 - 0.75 = 0.25
        /// 1.3 - 1 = 0.3
        /// </example>
        /// <param name="ratio1">Ratio value one.</param>
        /// <param name="ratio2">Ratio value two.</param>
        /// <returns>The smaller ratio difference from 1.</returns>
        public static double MinRatio(double ratio1, double ratio2)
        {
            // These ratios are smaller, so which ever is closer to 1.
            if (ratio1 < 1 && ratio2 < 1)
                return Math.Max(ratio1, ratio2);
            // Ratio1 is less than 1, ratio2 is greater or equal to 1.
            else if (ratio1 < 1)
            {
                var r1 = 1 - ratio1;
                var r2 = ratio2 - 1;
                var r = Math.Min(r1, r2);
                return (r == r1) ? ratio1 : ratio2;
            }
            // Ratio1 is less than 1, ratio2 is greater or equal to 1.
            else if (ratio2 < 1)
            {
                var r1 = ratio1 - 1;
                var r2 = 1 - ratio2;
                var r = Math.Min(r1, r2);
                return (r == r1) ? ratio1 : ratio2;
            }
            // Both ratios are greater then or equal to 1.
            else
                return Math.Min(ratio1, ratio2);
        }
        #endregion

        #region MaxRatio
        /// <summary>
        /// Determine which value is the greatest ratio difference from 1.
        /// </summary>
        /// <param name="ratio1">Ratio value one.</param>
        /// <param name="ratio2">Ratio value two.</param>
        /// <returns>The largest ratio difference from 1.</returns>
        public static float MaxRatio(float ratio1, float ratio2)
        {
            return MinRatio(ratio1, ratio2) == ratio1 ? ratio2 : ratio1;
        }

        /// <summary>
        /// Determine which value is the greatest ratio difference from 1.
        /// </summary>
        /// <param name="ratio1">Ratio value one.</param>
        /// <param name="ratio2">Ratio value two.</param>
        /// <returns>The largest ratio difference from 1.</returns>
        public static double MaxRatio(double ratio1, double ratio2)
        {
            return MinRatio(ratio1, ratio2) == ratio1 ? ratio2 : ratio1;
        }
        #endregion

        #region Resize
        /// <summary>
        /// Calculate the new size based on the ratio.
        /// </summary>
        /// <param name="value">Value to resize.</param>
        /// <param name="ratio">Ratio to use to calculate the new size.</param>
        /// <returns>New size based on ratio.</returns>
        public static float Resize(float value, float ratio)
        {
            return value * ratio;
        }

        /// <summary>
        /// Calculate the new size based on the ratio.
        /// </summary>
        /// <param name="value">Value to resize.</param>
        /// <param name="ratio">Ratio to use to calculate the new size.</param>
        /// <returns>New size based on ratio.</returns>
        public static double Resize(double value, float ratio)
        {
            return (double)(value * ratio);
        }

        /// <summary>
        /// Calculate the new size based on the ratio.
        /// </summary>
        /// <param name="value">Value to resize.</param>
        /// <param name="ratio">Ratio to use to calculate the new size.</param>
        /// <returns>New size based on ratio.</returns>
        public static int Resize(int value, float ratio)
        {
            return (int)(value * ratio);
        }

        /// <summary>
        /// Calculate the new size based on the ratio.
        /// </summary>
        /// <param name="value">Value to resize.</param>
        /// <param name="ratio">Ratio to use to calculate the new size.</param>
        /// <returns>New size based on ratio.</returns>
        public static long Resize(long value, float ratio)
        {
            return (long)(value * ratio);
        }
        #endregion

        #region Center
        /// <summary>
        /// Calculate the center point of the value (50%).
        /// </summary>
        /// <param name="value">Value to split.</param>
        /// <returns>50% of the value.</returns>
        public static short Center(short value)
        {
            return (short)(value / 2);
        }

        /// <summary>
        /// Calculate the center point of the value (50%).
        /// </summary>
        /// <param name="value">Value to split.</param>
        /// <returns>50% of the value.</returns>
        public static float Center(float value)
        {
            return (value / 2);
        }

        /// <summary>
        /// Calculate the center point of the value (50%).
        /// </summary>
        /// <param name="value">Value to split.</param>
        /// <returns>50% of the value.</returns>
        public static double Center(double value)
        {
            return (value / 2);
        }

        /// <summary>
        /// Calculate the center point of the value (50%).
        /// </summary>
        /// <param name="value">Value to split.</param>
        /// <returns>50% of the value.</returns>
        public static int Center(int value)
        {
            return (int)((float)value / 2);
        }

        /// <summary>
        /// Calculate the center point of the value (50%).
        /// </summary>
        /// <param name="value">Value to split.</param>
        /// <returns>50% of the value.</returns>
        public static long Center(long value)
        {
            return (int)((double)value / 2);
        }

        /// <summary>
        /// Calculate the center point of the value (50%).
        /// </summary>
        /// <param name="value">Value to split.</param>
        /// <returns>50% of the value.</returns>
        public static int Center(string value)
        {
            return (int)((float)value.Length / 2);
        }
        #endregion

        #region OffsetCenter
        /// <summary>
        /// Calculate the offset of the center based on the offset point.
        /// The offset parameter is a coordinate ratio that places it within the value at the specified point.
        /// An offset of '-1' will always return a value of '1'.
        /// An offset of '1' will always return the value as it is.
        /// An offset of '0.5' will always return the center of the value.
        /// </summary>
        /// <param name="value">Value to offset.</param>
        /// <param name="offset">Offset ratio value.</param>
        /// <returns>The offset from center.</returns>
        public static float OffsetCenter(float value, float offset)
        {
            Validation.Assert.Range(offset, -1, 1, "offset");

            if (value == 0)
                return value;

            var center = Center(value);

            // Return the true center.
            if (offset == 0)
                return center;
            else if (offset == -1)
                return 0;
            else if (offset == 1)
                return value;
            // 100, 0 = 50
            // 100, 0.5 = (0.5 * 50) = 100 - 25 = 75
            else if (offset > 0)
                return value - Math.Abs(center * offset);
            // 100, -0.5 = (-0.5 * 50) = 50 - 25 = 25
            else
                return center - Math.Abs(center * offset);
        }

        /// <summary>
        /// Calculate the offset of the center based on the offset point.
        /// The offset parameter is a coordinate ratio that places it within the value at the specified point.
        /// An offset of '-1' will always return a value of '1'.
        /// An offset of '1' will always return the value as it is.
        /// An offset of '0.5' will always return the center of the value.
        /// </summary>
        /// <param name="value">Value to offset.</param>
        /// <param name="offset">Offset ratio value.</param>
        /// <returns>The offset from center.</returns>
        public static double OffsetCenter(double value, float offset)
        {
            Validation.Assert.Range(offset, -1, 1, "offset");

            if (value == 0)
                return value;

            var center = Center(value);
            if (offset == 0)
                return center;
            else if (offset == -1)
                return 0;
            else if (offset == 1)
                return value;
            // 100, 0 = 50
            // 100, 0.5 = (0.5 * 50) = 100 - 25 = 75
            else if (offset > 0)
                return value - Math.Abs(center * offset);
            // 100, -0.5 = (-0.5 * 50) = 50 - 25 = 25
            else
                return center - Math.Abs(center * offset);
        }
        #endregion

        #region Scale
        /// <summary>
        /// Calculate the destination rectangle that will host the object in the new size.
        /// </summary>
        /// <param name="size">Original size of the object.</param>
        /// <param name="resize">Desired size of the new object.</param>
        /// <param name="offset">Offset point within the new size.</param>
        /// <param name="allowWhitespace">
        ///     When 'true' it will ensure the object is not cropped.  
        ///     When 'false' the object will fill the new size and crop anything extending beyond the new size.
        /// </param>
        /// <returns>Destination rectangle.</returns>
        public static Rectangle Scale(Size size, Size resize, Fosol.Common.CenterPoint offset, bool allowWhitespace = true)
        {
            return Scale(size, resize, offset.X, offset.Y, allowWhitespace);
        }

        /// <summary>
        /// Calculate the destination rectangle that will host the object in the new size.
        /// </summary>
        /// <param name="size">Original size of the object.</param>
        /// <param name="resize">Desired size of the new object.</param>
        /// <param name="xOffset">Horizontal x-axis offset point within the new size.</param>
        /// <param name="yOffset">Vertical y-axis offset point within the new size.</param>
        /// <param name="allowWhitespace">
        ///     When 'true' it will ensure the object is not cropped.  
        ///     When 'false' the object will fill the new size and crop anything extending beyond the new size.
        /// </param>
        /// <returns>Destination rectangle.</returns>
        public static Rectangle Scale(Size size, Size resize, float xOffset = 0f, float yOffset = 0f, bool allowWhitespace = true)
        {
            Validation.Assert.IsNotNull(size, "size");
            Validation.Assert.IsNotNull(resize, "resize");
            Validation.Assert.Range(xOffset, -1, 1, "hOffset");
            Validation.Assert.Range(yOffset, -1, 1, "yOffset");

            var dest = new Rectangle(0, 0, resize.Width, resize.Height);

            if (size.Equals(resize))
                return dest;

            var wr = Ratio(resize.Width, size.Width);
            var hr = Ratio(resize.Height, size.Height);

            var min = Math.Min(wr, hr);
            var max = Math.Max(wr, hr);

            // Using the minimum ratio will result in whitespace.
            var ratio = allowWhitespace ? min : max;

            dest.Width = Resize(size.Width, ratio);
            dest.Height = Resize(size.Height, ratio);

            // Restricts scaling to remain the boundaries of resize.
            if (allowWhitespace)
            {
                // Center the object within the resize.
                dest.X = (int)OffsetCenter(resize.Width - dest.Width, xOffset);
                dest.Y = (int)OffsetCenter(resize.Height - dest.Height, yOffset * -1);
            }
            // The object will scale using the max ratio, which means it will be cropped.
            else
            {
                dest.X = (int)OffsetCenter(dest.Width - resize.Width, xOffset) * -1;
                dest.Y = (int)OffsetCenter(dest.Height - resize.Height, yOffset * -1) * -1;
            }

            return dest;
        }
        #endregion
        #endregion
    }
}
