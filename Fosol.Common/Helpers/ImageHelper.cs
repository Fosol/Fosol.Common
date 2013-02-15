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
        #region Methods
        /// <summary>
        /// Gets the encoder information for a specified ImageFormat value.
        /// </summary>
        /// <param name="format">ImageFormat to retrieve encoder information for.</param>
        /// <returns>ImageCodecInfo object.</returns>
        public static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders().First(d => d.FormatID == format.Guid);
        }
        #endregion
    }
}
