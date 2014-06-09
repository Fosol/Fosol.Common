﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common
{
    /// <summary>
    /// A CenterPoint is a reference to the center of an object.
    /// The X and Y locations are ratios from the true center of the object.
    /// Therefore a (0,0) means the center of an object.
    /// Negative X value move left.
    /// Negative Y values move down.
    /// </summary>
    public class CenterPoint
    {
        #region Variables
        private float _X = 0;
        private float _Y = 0;
        #endregion

        #region Properties
        /// <summary>
        /// get/set - X offset value from center.
        /// This is the x-axis point.  
        /// Negative numbers are left of center.
        /// Positive numbers are right of center.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value must be between -1 and 1.</exception>
        public float X
        {
            get { return _X; }
            set
            {
                Validation.Assert.Range(value, -1, 1, "X");
                _X = value;
            }
        }

        /// <summary>
        /// get/set - Y offset value from center.
        /// This is the y-axis point.
        /// Negative numbers are below center.
        /// Positive numbers are above center.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value must be between -1 and 1.</exception>
        public float Y
        {
            get { return _Y; }
            set
            {
                Validation.Assert.Range(value, -1, 1, "Y");
                _Y = value;
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
        /// <exception cref="System.ArgumentOutOfRangeException">Parameters "X", and "Y" must be between -1 and 1.</exception>
        /// <param name="X">X offset position in the object [-1 to 1].</param>
        /// <param name="Y">Y offset position in the object [-1 to 1].</param>
        public CenterPoint(float X, float Y)
        {
            Validation.Assert.Range(X, -1, 1, "X");
            Validation.Assert.Range(Y, -1, 1, "Y");

            this.X = X;
            this.Y = Y;
        }
        #endregion

        #region Methods
        /// <summary>
        /// If the CenterPoint objects have the same X and Y value they are equal.
        /// </summary>
        /// <param name="obj">Object to compare.</param>
        /// <returns>True if they are equal.</returns>
        public override bool Equals(object obj)
        {
            var value = obj as CenterPoint;

            if (value == null)
                return false;

            if (value.X == this.X
                && value.Y == this.Y)
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

        /// <summary>
        /// Returns the string expression that represents the center point ratio as (X, Y).
        /// </summary>
        /// <returns>String value representing the center point.</returns>
        public override string ToString()
        {
            return string.Format("({0}, {1})", this.X, this.Y);
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
