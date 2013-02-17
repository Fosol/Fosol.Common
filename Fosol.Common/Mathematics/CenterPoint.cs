using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Mathematics
{
    /// <summary>
    /// A CenterPoint is a reference to the center of an object.
    /// The Horizontal and Vertical locations are ratios from the true center of the object.
    /// Therefore a (0,0) means the center of an object.
    /// Negative horizontal value move left.
    /// Negative vertical values move up.
    /// </summary>
    public class CenterPoint
    {
        #region Variables
        private float _HorizontalOffset = 0;
        private float _VerticalOffset = 0;
        #endregion

        #region Properties
        /// <summary>
        /// get/set - Horizontal offset value from center.
        /// This is the x-axis point.  
        /// Negative numbers are left of center.
        /// Positive numbers are right of center.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value must be between -1 and 1.</exception>
        public float HorizontalOffset
        {
            get { return _HorizontalOffset; }
            set
            {
                Validation.Parameter.AssertRange(value, -1, 1, "HorizontalOffset");
                _HorizontalOffset = value;
            }
        }

        /// <summary>
        /// get/set - Vertical offset value from center.
        /// This is the y-axis point.
        /// Negative numbers are above center.
        /// Positive numbers are below center.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value must be between -1 and 1.</exception>
        public float VerticalOffset
        {
            get { return _VerticalOffset; }
            set
            {
                Validation.Parameter.AssertRange(value, -1, 1, "VerticalOffset");
                _VerticalOffset = value;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of a CenterPoint object.
        /// </summary>
        public CenterPoint()
        {
        }

        /// <summary>
        /// Creates a new instance of a CenterPoint object.
        /// </summary>
        /// <param name="horizontal">Horizontal offset position in the object [-1 to 1].</param>
        /// <param name="vertical">Vertical offset position in the object [-1 to 1].</param>
        public CenterPoint(float horizontal, float vertical)
        {
            this.HorizontalOffset = horizontal;
            this.VerticalOffset = vertical;
        }
        #endregion

        #region Methods
        /// <summary>
        /// If the CropPoint objects have the same Horizontal and Vertical value they are equal.
        /// </summary>
        /// <param name="obj">Object to compare.</param>
        /// <returns>True if they are equal.</returns>
        public override bool Equals(object obj)
        {
            var value = obj as CenterPoint;

            if (value == null)
                return false;

            if (value.HorizontalOffset == this.HorizontalOffset
                && value.VerticalOffset == this.VerticalOffset)
                return true;

            return false;
        }

        /// <summary>
        /// HashCode to identify this instance of the object.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("({0}, {1})", this.HorizontalOffset, this.VerticalOffset);
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
