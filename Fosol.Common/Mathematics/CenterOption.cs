using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Mathematics
{
    public static class CenterOption
    {
        #region Variables
        /// <summary>
        /// Centering options for the MathHelper.Scale function.
        /// </summary>
        public enum CenterOptionEnum
        {
            Center,
            Portrait,
            Landscape,
            Left,
            Right,
            Top,
            TopLeft,
            TopRight,
            Bottom,
            BottomLeft,
            BottomRight
        }
        #endregion

        #region Properties
        /// <summary>
        /// get - Center the object.
        /// </summary>
        public static readonly CenterPoint Center = new CenterPoint(0, 0);

        /// <summary>
        /// get - Center closer to the top.
        /// </summary>
        public static readonly CenterPoint Portrait = new CenterPoint(0, -0.5f);

        /// <summary>
        /// get - Center the object.
        /// </summary>
        public static readonly CenterPoint Landscape = new CenterPoint(0, 0);

        /// <summary>
        /// get - Center on the absolute left side.
        /// </summary>
        public static readonly CenterPoint Left = new CenterPoint(-1, 0);

        /// <summary>
        /// get - Center on the absolute right side.
        /// </summary>
        public static readonly CenterPoint Right = new CenterPoint(1, 0);

        /// <summary>
        /// get - Center on the absolute top.
        /// </summary>
        public static readonly CenterPoint Top = new CenterPoint(0, -1);

        /// <summary>
        /// get - Center on the absolute top left.
        /// </summary>
        public static readonly CenterPoint TopLeft = new CenterPoint(-1, -1);

        /// <summary>
        /// get - Center on the absolute top left.
        /// </summary>
        public static readonly CenterPoint TopRight = new CenterPoint(1, -1);

        /// <summary>
        /// get - Center on the absolute bottom.
        /// </summary>
        public static readonly CenterPoint Bottom = new CenterPoint(0, 1);

        /// <summary>
        /// get - Center on the absolute bottom.
        /// </summary>
        public static readonly CenterPoint BottomLeft = new CenterPoint(-1, 1);

        /// <summary>
        /// get - Center on the absolute bottom.
        /// </summary>
        public static readonly CenterPoint BottomRight = new CenterPoint(1, 1);

        #endregion

        #region Constructors
        #endregion

        #region Methods
        /// <summary>
        /// Return the corresponding CenterPoint that matches the specified option.
        /// </summary>
        /// <param name="option">CenterOptionEnum value.</param>
        /// <returns>CenterPoint object.</returns>
        public static CenterPoint FromOption(CenterOptionEnum option)
        {
            switch (option)
            {
                case (CenterOptionEnum.Center):
                    return Center;
                case (CenterOptionEnum.Portrait):
                    return Portrait;
                case (CenterOptionEnum.Landscape):
                    return Landscape;
                case (CenterOptionEnum.Left):
                    return Left;
                case (CenterOptionEnum.Right):
                    return Right;
                case (CenterOptionEnum.Top):
                    return Top;
                case (CenterOptionEnum.TopLeft):
                    return TopLeft;
                case (CenterOptionEnum.TopRight):
                    return TopRight;
                case (CenterOptionEnum.Bottom):
                    return Bottom;
                case (CenterOptionEnum.BottomLeft):
                    return BottomLeft;
                case (CenterOptionEnum.BottomRight):
                    return BottomRight;
                default:
                    return Center;
            }
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion

    }
}
