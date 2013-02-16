using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;

namespace Fosol.Common.Helpers
{
    /// <summary>
    /// Utility methods to help with images.
    /// </summary>
    public static class ImageHelper
    {
        #region Variables
        /// <summary>
        /// Caching dictionary for ImageCodecInfo.
        /// </summary>
        private static Dictionary<ImageFormat, ImageCodecInfo> _CachedImageCodecInfo;
        #endregion

        #region Methods
        /// <summary>
        /// Gets the encoder information for a specified ImageFormat value.
        /// </summary>
        /// <param name="format">ImageFormat to retrieve encoder information for.</param>
        /// <returns>ImageCodecInfo object.</returns>
        public static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            // Cached dictionary needs to be initialized.
            if (_CachedImageCodecInfo == null)
                _CachedImageCodecInfo = new Dictionary<ImageFormat, ImageCodecInfo>();

            // Add it to the cached dictionary.
            if (!_CachedImageCodecInfo.ContainsKey(format))
            {
                var codec = ImageCodecInfo.GetImageDecoders().First(d => d.FormatID == format.Guid);
                _CachedImageCodecInfo.Add(format, codec);
            }

            return _CachedImageCodecInfo[format];
        }
        #endregion
    }
}
