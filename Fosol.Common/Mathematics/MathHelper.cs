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
    public class MathHelper
    {
        #region Methods
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
            Validation.Parameter.AssertRange(offset, -1, 1, "offset");

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
            Validation.Parameter.AssertRange(offset, -1, 1, "offset");

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
        /// Calculate the destination rectangle based on the current size and the new size.
        /// Always maintains the scale of the original object.
        /// </summary>
        /// <param name="size">Size of the current object.</param>
        /// <param name="resize">Desired size to convert the oject into.</param>
        /// <param name="offset">Cropping offset control option. Zero represents the center point.</param>
        /// <param name="restrictSize">Determines whether the scaled image must keep one of the newSize dimensions (width, height).</param>
        /// <returns>A destination rectangle so that scale of the original object will be maintained.</returns>
        public static Rectangle FillAndScale(Size size, Size resize, CenterPoint offset, bool restrictSize = true)
        {
            return FillAndScale(size, resize, offset.X, offset.Y, restrictSize);
        }

        /// <summary>
        /// Calculate the destination rectangle based on the current size and the new size.
        /// Always maintains the scale of the original object.
        /// </summary>
        /// <param name="size">Size of the current object.</param>
        /// <param name="resize">Desired size to convert the oject into.</param>
        /// <param name="xOffset">Horizontal cropping offset value [-1 to 1]. Zero represents the center point.</param>
        /// <param name="yOffset">Vertical cropping offset value [-1 to 1]. Zero represents the center point.</param>
        /// <param name="restrictSize">Determines whether the scaled image must keep one of the newSize dimensions (width, height).</param>
        /// <returns>A destination rectangle so that scale of the original object will be maintained.</returns>
        public static Rectangle FillAndScale(Size size, Size resize, float xOffset = 0f, float yOffset = 0f, bool restrictSize = true)
        {
            Validation.Parameter.AssertRange(xOffset, -1, 1, "hOffset");
            Validation.Parameter.AssertRange(yOffset, -1, 1, "yOffset");

            var dest = new Rectangle(0, 0, resize.Width, resize.Height);

            // Same size as orginal.
            if (size.Equals(resize))
                return dest;

            var wr = (float)size.Width / resize.Width;
            var hr = (float)size.Height / resize.Height;
            var min = MinRatio(wr, hr);
            var max = MaxRatio(wr, hr);

            // Restrict destination rectangle to the boundary of smallest ratio change.
            if (restrictSize)
            {
                // Width ratio is smallest.  Use it to calculate height.
                if (wr == min)
                {
                    if (wr == 1)
                    {
                        dest.Height = resize.Width;
                    }
                    else if (wr < 1)
                    {
                        dest.Height = (int)(resize.Height / wr);
                    }
                    else
                    {
                        dest.Height = (int)(resize.Height * wr);
                    }

                    dest.Y = (int)OffsetCenter(dest.Height - resize.Height, yOffset) * -1;
                }
                // Height ratio is smallest.  Use it to calculate width.
                else
                {
                    if (hr == 1)
                    {
                        dest.Width = resize.Height;
                    }
                    else if (hr < 1)
                    {
                        dest.Width = (int)(resize.Width / hr);
                    }
                    else
                    {
                        dest.Width = (int)(resize.Width * hr);
                    }

                    dest.X = (int)OffsetCenter(dest.Width - resize.Width, xOffset) * -1;
                }
            }
            // Use the newSize dimensions to calculate the destination rectangle.
            else
            {
                if (min == 1)
                {
                    if (hr != min)
                    {
                        if (hr < 1)
                        {
                            dest.Height = (int)(resize.Height / hr);
                        }
                        else
                        {
                            dest.Height = (int)(resize.Height * hr);
                        }
                    }
                    else
                    {
                        if (wr < 1)
                        {
                            dest.Width = (int)(resize.Width / wr);
                        }
                        else
                        {
                            dest.Width = (int)(resize.Width * wr);
                        }
                    }
                }
                // Make larger
                else if (min < 1)
                {
                    dest.Width = (int)(resize.Width / min);
                    dest.Height = (int)(resize.Height / min);
                }
                // Make smaller
                else
                {
                    dest.Width = (int)(size.Width * min);
                    dest.Height = (int)(size.Height * min);
                }

                dest.X = (int)OffsetCenter(dest.Width - resize.Width, xOffset) * -1;
                dest.Y = (int)OffsetCenter(dest.Height - resize.Height, yOffset) * -1;
            }

            return dest;
        }


        public static Rectangle Scale(Size size, Size resize, CenterPoint offset, bool allowWhitespace = true)
        {
            return Scale(size, resize, offset.X, offset.Y, allowWhitespace);
        }

        public static Rectangle Scale(Size size, Size resize, float xOffset = 0f, float yOffset = 0f, bool allowWhitespace = true)
        {
            Validation.Parameter.AssertIsNotNull(size, "size");
            Validation.Parameter.AssertIsNotNull(resize, "resize");
            Validation.Parameter.AssertRange(xOffset, -1, 1, "hOffset");
            Validation.Parameter.AssertRange(yOffset, -1, 1, "yOffset");

            var dest = new Rectangle(0, 0, resize.Width, resize.Height);

            if (size.Equals(resize))
                return dest;

            var wr = (float)size.Width / resize.Width;
            var hr = (float)size.Height / resize.Height;

            // Use the min ratio when allowWhitespace = true.
            var min = MinRatio(wr, hr);

            // Use the max ratio when allowWhitespace = false.
            var max = MaxRatio(wr, hr);
            var ratio = allowWhitespace ? min : max;

            // Expand the object to fit the new larger size.
            if (resize.Width >= size.Width && resize.Height >= size.Height)
            {
                if (ratio <= 1)
                {
                    dest.Width = (int)(size.Width / ratio);
                    dest.Height = (int)(size.Height / ratio);
                }
                else
                {
                    dest.Width = (int)(size.Width * ratio);
                    dest.Height = (int)(size.Height * ratio);
                }
            }
            // Shrink the object to fit the new smaller size.
            else
            {
                if (ratio <= 1)
                {
                    dest.Width = (int)(size.Width * ratio);
                    dest.Height = (int)(size.Height * ratio);
                }
                else
                {
                    dest.Width = (int)(size.Width / ratio);
                    dest.Height = (int)(size.Height / ratio);
                }
            }

            // Restricts scaling to remain the boundaries of resize.
            if (allowWhitespace)
            {
                // Center the object within the resize.
                dest.X = (int)OffsetCenter(resize.Width - dest.Width, xOffset);
                dest.Y = (int)OffsetCenter(resize.Height - dest.Height, yOffset);
            }
            // The object will scale using the max ration, which means it will be cropped.
            else
            {
                dest.X = (int)OffsetCenter(dest.Width - resize.Width, xOffset) * -1;
                dest.Y = (int)OffsetCenter(dest.Height - resize.Height, yOffset) * -1;
            }

            return dest;
        }
        #endregion
        #endregion
    }
}
