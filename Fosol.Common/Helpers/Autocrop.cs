using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Helpers
{
    /// <summary>
    /// Provides a way to autocrop images.
    /// </summary>
    public class Autocrop
    {
        #region Constants
        private const float _DefaultHorizontalCrop = 0.5f;
        private const float _DefaultVerticalCrop = 0.3f;
        private const long _DefaultImageQuality = 75;
        private const int _AutocropCacheLength = 30;
        private static readonly Color _DefaultFillColor = Color.White;

        public const string DefaultMaxHeightKey = "AutocropMaxHeight";
        protected readonly int _MaxHeight = GetMaxHeight();
        public const string DefaultMaxWidthKey = "AutocropMaxWidth";
        protected readonly int _MaxWidth = GetMaxWidth();
        #endregion

        #region Variables
        #endregion

        #region Properties
        /// <summary>
        /// get - The default horizontal crop value.
        /// </summary>
        public static float DefaultHorizontalCrop { get; private set; }

        /// <summary>
        /// get - The default vertical crop value.
        /// This value represents the fraction of the total amount that needs to be cropped along the y-axis which will be cropped from the top portion of an image.
        /// This is used when an image is taller than it is wide.
        /// </summary>
        public static float DefaultVerticalCrop { get; private set; }

        /// <summary>
        /// get - The default background color to use for copped images which do not fill the requested image size.
        /// </summary>
        public static ConsoleColor DefaultFillColor { get; private set; }
        #endregion

        #region Constructors
        #endregion

        #region Methods
        /// <summary>
        /// Read from the AppSetting configuration file and return the value for the specified key.
        /// </summary>
        /// <param name="key">Key name.</param>
        /// <returns>Value of the key.</returns>
        protected static string GetAppSetting(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// Get the configured max height from the AppSettings.
        /// </summary>
        /// <returns>If the AppSetting key "AutocropMaxHeight" has been set it will return that value.</returns>
        protected static int GetMaxHeight()
        {
            var value = 0;
            int.TryParse(GetAppSetting(DefaultMaxHeightKey), out value);
            return value;
        }

        /// <summary>
        /// Get the configured max width from the AppSettings.
        /// </summary>
        /// <returns>If the AppSetting key "AutocropMaxWidth" has been set it will return that value.</returns>
        protected static int GetMaxWidth()
        {
            var value = 0;
            int.TryParse(GetAppSetting(DefaultMaxWidthKey), out value);
            return value;
        }

        public void Crop(Stream destination, int? height, int? width, AutocropMode mode = AutocropMode.Crop, long? quality = _DefaultImageQuality, Color fillColor = new Color())
        {
            System.Drawing.Image image = null;
            fillColor.
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
