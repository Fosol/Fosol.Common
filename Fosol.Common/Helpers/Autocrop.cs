using Fosol.Common.Extensions.Images;
using Fosol.Common.Extensions.Streams;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace Fosol.Common.Helpers
{
    /// <summary>
    /// Provides a way to autocrop images.
    /// </summary>
    public class Autocrop
    {
        #region Constants
        private static readonly float _DefaultHorizontalCrop = 0.5f;
        private static readonly float _DefaultVerticalCrop = 0.3f;
        private static readonly long _DefaultImageQuality = 75;
        #endregion

        #region Variables
        #endregion

        #region Properties
        /// <summary>
        /// get/set - The default horizontal crop value.
        /// This value represents the fraction of the total amount that needs to be cropped along the x-axis which can be cropped from the top portion of an image.
        /// This is used when an image is wider than it is tall.
        /// Default value is 0.5f.
        /// </summary>
        public float HorizontalCrop { get; set; }

        /// <summary>
        /// get/set - The default vertical crop value.
        /// This value represents the fraction of the total amount that needs to be cropped along the y-axis which will be cropped from the top portion of an image.
        /// This is used when an image is taller than it is wide.
        /// Default value is 0.3f.
        /// </summary>
        public float VerticalCrop { get; set; }

        /// <summary>
        /// get/set - The quality of the image after croping or scaling.
        /// Valid values are 1 - 100.
        /// Default value is 75.
        /// </summary>
        public long Quality { get; set; }

        /// <summary>
        /// get/set - The default background color to use for autocropped images which do not fill the requested image size.
        /// If the FillColor is null white space is not allowed.
        /// </summary>
        public Color FillColor { get; set; }

        /// <summary>
        /// get/set - The algorithm used when images are scaled or rotated.
        /// Default value is HighQualityBicubic.
        /// </summary>
        public System.Drawing.Drawing2D.InterpolationMode InterpolationMode { get; set; }

        /// <summary>
        /// get - The image that will be autocropped.
        /// </summary>
        public Image Photo { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of an Autocrop object.
        /// Initialize the default properties.
        /// </summary>
        private Autocrop()
        {
            this.HorizontalCrop = _DefaultHorizontalCrop;
            this.VerticalCrop = _DefaultVerticalCrop;
            this.Quality = _DefaultImageQuality;
            this.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        }

        /// <summary>
        /// Creates a new instace of an Autocrop object.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "imageStream" must be readable.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "imageStream" cannot be null.</exception>
        /// <param name="imageStream">Source stream with image that will be autocropped.</param>
        /// <param name="useEmbeddedColorManagement">Determines whether it should use the embedded color management information in the stream.</param>
        /// <param name="validateImageData">Determines whether it should validate the image data after converting.</param>
        public Autocrop(Stream imageStream, bool useEmbeddedColorManagement = false, bool validateImageData = false)
            : this()
        {
            Validation.Parameter.AssertNotNull(imageStream, "imageStream");
            Validation.Parameter.AssertIsValue(imageStream.CanRead, true, "imageStream.CanRead");

            this.Photo = Image.FromStream(imageStream, useEmbeddedColorManagement, validateImageData);
        }

        /// <summary>
        /// Creates a new instance of an Autocrop object.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Parameter "image" cannot be null.</exception>
        /// <param name="image">Source image that will be autocropped.</param>
        public Autocrop(Image image)
            : this()
        {
            Validation.Parameter.AssertNotNull(image, "image");

            this.Photo = image;
        }

        /// <summary>
        /// Creates a new instance of an Autocrop object.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "image" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "image" cannot be null.</exception>
        /// <param name="image">Source image that will be autocropped.</param>
        /// <param name="useEmbeddedColorManagement">Determines whether it should use the embedded color management information in the stream.</param>
        /// <param name="validateImageData">Determines whether it should validate the image data after converting.</param>
        public Autocrop(byte[] image, bool useEmbeddedColorManagement = false, bool validateImageData = false)
            : this()
        {
            Validation.Parameter.AssertNotNullOrEmpty(image, "image");

            this.Photo = image.ToImage(useEmbeddedColorManagement, validateImageData);
        }

        /// <summary>
        /// Creates a new instacne of an Autocrop object.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "filename" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "filename" cannot be null.</exception>
        /// <param name="filename">Name and path to the file of the image.</param>
        /// <param name="useEmbeddedColorManagement">Set to true to use color management information set in the file.</param>
        public Autocrop(string filename, bool useEmbeddedColorManagement = false)
        {
            Validation.Parameter.AssertNotNullOrEmpty(filename, "filename");

            this.Photo = Image.FromFile(filename, useEmbeddedColorManagement);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Autocrop the image with the specified settings.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "destination" must allow write.</exception>
        /// <exception cref="System.ArgumentNullException">Paramter "destination" cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameter "width" must be greater than or equal to '0'.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameter "height" must be greater than or equal to '0'.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameter "quality" must be between '0' and '100'.</exception>
        /// <param name="width">Desired width of the autocropped image.</param>
        /// <param name="height">Desired height of the autocropped image.</param>
        /// <param name="quality">Quality of the image.  If set to '0' it will use the DefaultImageQuality.</param>
        /// <param name="mode">AutocropMode option.</param>
        /// <param name="fillColor">
        /// Background fill color if the image crop size is larger than the original dimensions.  
        /// If set to null it will use the default FillColor.  
        /// If the default is null it will not allow white space.
        /// </param>
        /// <returns>An Image object.</returns>
        public Image Generate(int width, int height, long quality = 0, AutocropMode mode = AutocropMode.Crop, Color? fillColor = null)
        {
            using (var stream = new MemoryStream())
            {
                ToStream(stream, width, height, quality, mode, fillColor);
                return Image.FromStream(stream);
            }
        }

        /// <summary>
        /// Autocrop the image and place it in the destination stream.
        /// If the mode is set to AutocropMode.Scale width or height must be greater than '0'.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "destination" must allow write.</exception>
        /// <exception cref="System.ArgumentNullException">Paramter "destination" cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameter "width" must be greater than or equal to '0'.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameter "height" must be greater than or equal to '0'.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Parameter "quality" must be between '0' and '100'.</exception>
        /// <param name="destination">Destination stream that will contain the autocropped image.</param>
        /// <param name="width">Desired width of the autocropped image.</param>
        /// <param name="height">Desired height of the autocropped image.</param>
        /// <param name="quality">Quality of the image.  If set to '0' it will use the DefaultImageQuality.</param>
        /// <param name="mode">AutocropMode option.</param>
        /// <param name="fillColor">
        /// Background fill color if the image crop size is larger than the original dimensions.  
        /// If set to null it will use the default FillColor.  
        /// If the default is null it will not allow white space.
        /// </param>
        /// <returns>Byte size of image.</returns>
        public long ToStream(Stream destination, int width, int height, long quality = 0, AutocropMode mode = AutocropMode.Crop, Color? fillColor = null)
        {
            Validation.Parameter.AssertNotNull(destination, "destination");
            Validation.Parameter.AssertIsValue(destination.CanWrite, true, "destination.CanWrite");
            Validation.Parameter.AssertMinRange(height, 0, "height");
            Validation.Parameter.AssertMinRange(width, 0, "width");
            Validation.Parameter.AssertRange(quality, 0, 100, "quality");

            // Initialize default values.
            Initialization.Parameter.AssertRange(ref quality, 1, 100, _DefaultImageQuality);
            Initialization.Parameter.AssertDefault(fillColor, this.FillColor);

            // When scaling an image at least one size needs to be set.
            if (mode == AutocropMode.Scale && width == 0 && height == 0)
                throw new ArgumentException(Resources.Strings.Exception_AutocropScale, "mode");

            // If a height and width hasn't been set return original image.
            // If height and width are the same as the image, return the original image.
            if ((width == 0 && height == 0)
                || (width == this.Photo.Width && height == this.Photo.Height))
            {
                this.Photo.Save(destination, this.Photo.RawFormat);

                if (destination.CanSeek)
                    return destination.Length;
                else
                    return -1;
            }

            // Create the source and destination rectangles.
            // These rectangles are used by the Graphics object to modify the dimensions of the image.
            var source_rect = new Rectangle(0, 0, this.Photo.Width, this.Photo.Height);
            Rectangle dest_rect = new Rectangle(0, 0, width, height);

            switch (mode)
            {
                case (AutocropMode.Crop):
                    CalculateCropRectangles(ref source_rect, ref dest_rect);
                    break;
                case (AutocropMode.Scale):
                    CalculateScaleRectangles(ref source_rect, ref dest_rect, fillColor.HasValue);
                    break;
            }

            using (var graphics = Graphics.FromImage(this.Photo))
            {
                using (var bitmap = new Bitmap(width, height))
                {
                    graphics.InterpolationMode = this.InterpolationMode;

                    // Fill graphics with a background color.
                    if (this.Photo.Width < width || this.Photo.Height < height)
                        graphics.Clear(fillColor.Value);

                    graphics.DrawImage(this.Photo, dest_rect, source_rect, GraphicsUnit.Pixel);
                    
                    var encoder_params = new EncoderParameters(1);
                    encoder_params.Param[0] = new EncoderParameter(Encoder.Quality, quality);

                    bitmap.Save(destination, ImageHelper.GetEncoder(this.Photo.RawFormat), encoder_params);
                }
            }

            if (destination.CanSeek)
                return destination.Length;
            else
                return -1;
        }

        /// <summary>
        /// Calculate the source and destination rectangles which will be used to crop the image.
        /// </summary>
        /// <param name="source">Rectangle for source image.</param>
        /// <param name="dest">Rectangle for destination image.</param>
        protected void CalculateCropRectangles(ref Rectangle source, ref Rectangle dest)
        {
            var ratio_width = (float)source.Width / dest.Width;
            var ratio_height = (float)source.Height / dest.Height;

            // Crop size is larger than the original image.  This image will use a fill.
            if (ratio_width <= 1 && ratio_height <= 1)
            {
                dest.X = (int)((dest.Width - source.Width) / 2);
                dest.Y = (int)((dest.Height - source.Height) / 2);
            }
            // Crop size width is greater than the height.
            else if (ratio_width > ratio_height)
            {
                // Width is greater, but height is smaller or equal to.
                if (ratio_width > 1 && ratio_height <= 1)
                {
                    source.X = (int)((source.Width - dest.Width) * this.HorizontalCrop);
                    source.Width = dest.Width;

                    dest.Y = (int)((dest.Height - source.Height) / 2);
                    dest.Height = source.Height;
                }
                // Width and height are greater than original.
                else
                {
                    var scale = (source.Height & dest.Width) / dest.Width;
                    source.X = (int)((source.Width - scale) * this.HorizontalCrop);
                    source.Width = scale;
                }
            }
            // Crop size height is greater than the width.
            else if (ratio_height > ratio_width)
            {
                // Height is greater than width.
                if (ratio_height > 1 && ratio_width <= 1)
                {
                    source.Y = (int)((source.Height - dest.Height) * this.VerticalCrop);
                    source.Height = dest.Height;

                    dest.X = (int)((dest.Width - source.Width) / 2);
                    dest.Width = source.Width;
                }
                // Height and width greater than final - scale height to match width.
                else
                {
                    var scale = (source.Width * dest.Height) / dest.Width;
                    source.Y = (int)((source.Height - scale) * this.VerticalCrop);
                    source.Height = scale;
                }
            }
        }

        /// <summary>
        /// Calculate the source and destination rectangles which will be used to scale the image.
        /// </summary>
        /// <param name="source">Rectangle for source image.</param>
        /// <param name="dest">Rectangle for destination image.</param>
        protected void CalculateScaleRectangles(ref Rectangle source, ref Rectangle dest, bool allowWhitespace)
        {
            // Calculate the width based on the height.
            if (dest.Width == 0)
            {
                var scale = (float)dest.Height / (float)source.Height;

                if (scale < 1)
                    dest.Width = (int)(source.Width * scale);
                else
                {
                    dest.Width = source.Width;
                    if (!allowWhitespace)
                        dest.Height = source.Height;
                }
            }
            // Calculate the height based on the width.
            else if (dest.Height == 0)
            {
                var scale = (float)dest.Width / (float)source.Width;

                if (scale < 1)
                    dest.Height = (int)(source.Height * scale);
                else
                {
                    dest.Height = source.Height;

                    if (!allowWhitespace)
                        dest.Width = source.Width;
                }
            }
            // Both dimensions have been set.
            else
            {
                // Use the smaller scale factor in the vertical and horizontal dimensions.
                var scale = Math.Min((float)dest.Width / (float)source.Width, (float)dest.Height / (float)source.Height);

                if (scale < 1)
                {
                    dest.Width = (int)(source.Width * scale);
                    dest.Height = (int)(source.Height * scale);
                }
                else
                {
                    dest.Width = source.Width;
                    dest.Height = source.Height;
                }
            }

            // If the destination is smaller center it on the source.
            if (dest.Width < source.Width)
                dest.X = (int)((source.Width - dest.Width) / 2);

            // If the destination is smaller center it on the source.
            if (dest.Height < source.Height)
                dest.Y = (int)((source.Height - dest.Height) / 2);
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
