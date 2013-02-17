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
        /// <param name="ratio1">Ratio one.</param>
        /// <param name="ratio2">Ratio two.</param>
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
            else if (ratio2 < 1)
            {
                var r1 = ratio1 - 1;
                var r2 = 1 - ratio2;
                var r = Math.Min(r1, r2);
                return (r == r1) ? ratio1 : ratio2;
            }
            else
                return Math.Min(ratio1, ratio2);
        }

        public static float MaxRatio(float ratio1, float ratio2)
        {
            return MinRatio(ratio1, ratio2) == ratio1 ? ratio2 : ratio1;
        }

        public static float Center(float value)
        {
            return (value / 2);
        }

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
        /// Calculate the destination rectangle based on the current size and the new size.
        /// Maintains original scale.
        /// </summary>
        /// <param name="size">Size of the current object.</param>
        /// <param name="resize">Desired size to convert the oject into.</param>
        /// <param name="offset">Cropping offset control option. Zero represents the center point.</param>
        /// <param name="restrictSize">Determines whether the scaled image must keep one of the newSize dimensions (width, height).</param>
        /// <returns>A destination rectangle so that scale of the original object will be maintained.</returns>
        public static Rectangle Scale(Size size, Size resize, CenterPoint offset, bool restrictSize = true)
        {
            return Scale(size, resize, offset.HorizontalOffset, offset.VerticalOffset, restrictSize);
        }

        /// <summary>
        /// Calculate the destination rectangle based on the current size and the new size.
        /// Maintains the scale of the original object.
        /// </summary>
        /// <param name="size">Size of the current object.</param>
        /// <param name="resize">Desired size to convert the oject into.</param>
        /// <param name="hOffset">Horizontal cropping offset value [-1 to 1]. Zero represents the center point.</param>
        /// <param name="yOffset">Vertical cropping offset value [-1 to 1]. Zero represents the center point.</param>
        /// <param name="restrictSize">Determines whether the scaled image must keep one of the newSize dimensions (width, height).</param>
        /// <returns>A destination rectangle so that scale of the original object will be maintained.</returns>
        public static Rectangle Scale(Size size, Size resize, float hOffset = 0f, float yOffset = 0f, bool restrictSize = true)
        {
            Validation.Parameter.AssertRange(hOffset, -1, 1, "hOffset");
            Validation.Parameter.AssertRange(yOffset, -1, 1, "yOffset");

            var dest = new Rectangle(0, 0, resize.Width, resize.Height);

            // Same size as orginal.
            if (size.Equals(resize))
                return dest;

            var wr = (float)size.Width / resize.Width;
            var hr = (float)size.Height / resize.Height;
            var min = MinRatio(wr, hr);
            var max = MaxRatio(wr, hr);
            var ratio = min == 1 ? max : min;

            // Restrict result to the boundary of one of the newSize dimensions.
            if (restrictSize)
            {
                // Width ratio is smallest.  Use it to calculate height.
                if (wr == ratio)
                {
                    if (wr < 1)
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
                    if (hr < 1)
                    {
                        dest.Width = (int)(resize.Width / hr);
                    }
                    else
                    {
                        dest.Width = (int)(resize.Width * hr);
                    }

                    dest.X = (int)OffsetCenter(dest.Width - resize.Width, hOffset) * -1;
                }
            }
            else
            {
                // Make larger
                if (ratio < 1)
                {
                    dest.Width = (int)(resize.Width / ratio);
                    dest.Height = (int)(resize.Height / ratio);
                }
                // Make smaller
                else
                {
                    dest.Width = (int)(resize.Width * ratio);
                    dest.Height = (int)(resize.Height * ratio);
                }

                dest.X = (int)OffsetCenter(dest.Width - resize.Width, hOffset) * -1;
                dest.Y = (int)OffsetCenter(dest.Height - resize.Height, yOffset) * -1;
            }

            return dest;
        }


        #endregion
    }
}
