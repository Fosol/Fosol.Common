using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Fosol.Common.Extensions.ImageExtensions
{
    /// <summary>
    /// Extension methods for image objects.
    /// </summary>
    public static class ImageExtensions
    {
        #region Methods
        /// <summary>
        /// Converts an image to a byte array using the specified ImageFormat.
        /// </summary>
        /// <param name="image">Image to convert into a byte array.</param>
        /// <param name="format">ImageFormat of the image.  If null it will use the image.RawFormat.</param>
        /// <returns>Byte array.</returns>
        public static byte[] ToByteArray(this Image image, ImageFormat format = null)
        {
            Initialization.Parameter.AssertDefault(format, image.RawFormat);

            using (var stream = new MemoryStream())
            {
                image.Save(stream, format);
                return stream.ToArray();
            }
        }
        #endregion
    }
}
