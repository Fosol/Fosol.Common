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
    /// Utility methods to modify image sizes.
    /// Provides the following methods, Resize, Crop, Scale.
    /// 
    /// Autocrop:
    /// Provides a way to maintain scale while cropping to a specific size.
    /// It will resize the image to match the desired size.
    /// It also provides a way to control the cropping offset.
    /// 
    /// Canvas:
    /// The original image will stay the same size, but the output will have white space, or it will crop the image.
    /// 
    /// Crop:
    /// This will allow you to crop an image within the current dimensions.
    /// You cannot crop an image to be larger.
    /// 
    /// Resize:
    /// This will allow you to stretch or shrink an image to the desired size.
    /// If you set the fillColor it will maintain scale while resizing.
    /// 
    /// Scale:
    /// This will allow you enlarge or shrink an image but always keep the original scale ratio.
    /// 
    /// Optimize:
    /// This will degrade the quality of the image and make it a smaller file size in bytes.
    /// </summary>
    public class ImageHelper
    {
        #region Constants
        private static readonly Mathematics.CenterPoint _DefaultCropOffset = Mathematics.CenterOption.Center;
        private static readonly long _DefaultImageQuality = 100;
        #endregion

        #region Variables
        /// <summary>
        /// Caching dictionary for ImageCodecInfo.
        /// </summary>
        private static Dictionary<ImageFormat, ImageCodecInfo> _CachedImageCodecInfo;
        #endregion

        #region Properties
        /// <summary>
        /// get/set - The default cropping offset option.
        /// This provides a way to specify the center of the image to enforce a desired cropping behaviour.
        /// </summary>
        public Mathematics.CenterPoint CropOffset { get; set; }

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
        /// get/set - Collection of encoder parameters to use when generating the new image.
        /// </summary>
        public EncoderParameters EncoderParameters { get; set; }

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
        private ImageHelper()
        {
            this.CropOffset = _DefaultCropOffset;
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
        public ImageHelper(Stream imageStream, bool useEmbeddedColorManagement = false, bool validateImageData = false)
            : this()
        {
            Validation.Parameter.AssertIsNotNull(imageStream, "imageStream");
            Validation.Parameter.AssertIsValue(imageStream.CanRead, true, "imageStream.CanRead");

            this.Photo = Image.FromStream(imageStream, useEmbeddedColorManagement, validateImageData);
        }

        /// <summary>
        /// Creates a new instance of an Autocrop object.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Parameter "image" cannot be null.</exception>
        /// <param name="image">Source image that will be autocropped.</param>
        public ImageHelper(Image image)
            : this()
        {
            Validation.Parameter.AssertIsNotNull(image, "image");

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
        public ImageHelper(byte[] image, bool useEmbeddedColorManagement = false, bool validateImageData = false)
            : this()
        {
            Validation.Parameter.AssertIsNotNullOrEmpty(image, "image");

            this.Photo = image.ToImage(useEmbeddedColorManagement, validateImageData);
        }

        /// <summary>
        /// Creates a new instacne of an Autocrop object.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "filename" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "filename" cannot be null.</exception>
        /// <param name="filename">Name and path to the file of the image.</param>
        /// <param name="useEmbeddedColorManagement">Set to true to use color management information set in the file.</param>
        public ImageHelper(string filename, bool useEmbeddedColorManagement = false)
            : this()
        {
            Validation.Parameter.AssertIsNotNullOrEmpty(filename, "filename");

            this.Photo = Image.FromFile(filename, useEmbeddedColorManagement);
        }
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

        /// Autocrop the image.
        /// Autocrop cannot increase the size of the original image.
        /// Autocrop will always maintain scale, thus it will crop automatically.
        /// If you only specify one dimension the dimension not specified will use the original image dimension (width, height).
        /// </summary>
        /// <param name="destination">Stream to place the new image.</param>
        /// <param name="size">Size of the new image.</param>
        /// <param name="quality">Quality of the image.</param>
        /// <param name="graphicsUnit">GraphicsUnit option.</param>
        /// <returns>Size of the new image in bytes</returns>
        public long Autocrop(Stream destination, Size size, long quality = 0, GraphicsUnit graphicsUnit = GraphicsUnit.Pixel)
        {
            return Autocrop(destination, size, this.CropOffset, quality, graphicsUnit);
        }

        /// <summary>
        /// Autocrop the image.
        /// Autocrop cannot increase the size of the original image.
        /// Autocrop will always maintain scale, thus it will crop automatically.
        /// If you only specify one dimension the dimension not specified will use the original image dimension (width, height).
        /// </summary>
        /// <param name="destination">Stream to place the new image.</param>
        /// <param name="size">Size of the new image.</param>
        /// <param name="offset">Cropping offset control option.</param>
        /// <param name="quality">Quality of the image.</param>
        /// <param name="graphicsUnit">GraphicsUnit option.</param>
        /// <returns>Size of the new image in bytes</returns>
        public long Autocrop(Stream destination, Size size, Mathematics.CenterPoint offset, long quality = 0, GraphicsUnit graphicsUnit = GraphicsUnit.Pixel)
        {
            Validation.Parameter.AssertIsNotNull(size, "size");
            Validation.Parameter.AssertMinRange(size.Width, 0, "size.Width", Resources.Strings.Exception_ImageHelper_InvalidResizeWidth);
            Validation.Parameter.AssertMinRange(size.Height, 0, "size.Height", Resources.Strings.Exception_ImageHelper_InvalidResizeHeight);
            Validation.Parameter.AssertIsNotValue(size.Width + size.Height, 0, "size", Resources.Strings.Exception_InvalidSize);

            return Autocrop(destination, size.Width, size.Height, offset, quality, graphicsUnit);
        }
        
        /// <summary>
        /// Autocrop the image.
        /// Autocrop cannot increase the size of the original image.
        /// Autocrop will always maintain scale, thus it will crop automatically.
        /// If you only specify one dimension the dimension not specified will use the original image dimension (width, height).
        /// </summary>
        /// <param name="destination">Stream to place the new image.</param>
        /// <param name="width">Width of new image.</param>
        /// <param name="height">Height of new image.</param>
        /// <param name="quality">Quality of the image.</param>
        /// <param name="graphicsUnit">GraphicsUnit option.</param>
        /// <returns>Size of the new image in bytes</returns>
        public long Autocrop(Stream destination, int width, int height, long quality = 0, GraphicsUnit graphicsUnit = GraphicsUnit.Pixel)
        {
            return Autocrop(destination, width, height, this.CropOffset, quality, graphicsUnit);
        }

        /// <summary>
        /// Autocrop the image.
        /// Autocrop cannot increase the size of the original image.
        /// Autocrop will always maintain scale, thus it will crop automatically.
        /// If you only specify one dimension the dimension not specified will use the original image dimension (width, height).
        /// </summary>
        /// <param name="destination">Stream to place the new image.</param>
        /// <param name="width">Width of new image.</param>
        /// <param name="height">Height of new image.</param>
        /// <param name="quality">Quality of the image.</param>
        /// <param name="graphicsUnit">GraphicsUnit option.</param>
        /// <returns>Size of the new image in bytes</returns>
        public long Autocrop(Stream destination, int width, int height, Mathematics.CenterPoint offset, long quality = 0, GraphicsUnit graphicsUnit = GraphicsUnit.Pixel)
        {
            Validation.Parameter.AssertIsNotNull(destination, "destination");
            Validation.Parameter.AssertIsValue(destination.CanWrite, true, "destination.CanWrite", Resources.Strings.Exception_Stream_IsCanWrite);
            Validation.Parameter.AssertIsValue(destination.CanSeek, true, "destination.CanSeek", Resources.Strings.Exception_Stream_IsCanSeek);
            Validation.Parameter.AssertMinRange(width, 0, "width", Resources.Strings.Exception_ImageHelper_InvalidResizeWidth);
            Validation.Parameter.AssertMinRange(height, 0, "height", Resources.Strings.Exception_ImageHelper_InvalidResizeHeight);
            Validation.Parameter.AssertIsNotValue(width + height, 0, "width, height", Resources.Strings.Exception_ImageHelper_InvalidPlot);
            Validation.Parameter.AssertRange(quality, 0, 100, "quality", Resources.Strings.Exception_ImageHelper_InvalidQuality);

            // Initialize default values.
            Initialization.Parameter.AssertIsNotDefault(ref width, this.Photo.Width);
            Initialization.Parameter.AssertIsNotDefault(ref height, this.Photo.Height);
            Initialization.Parameter.AssertIsNotDefault(offset, this.CropOffset);

            // If the x and y coordinates are at 0.
            // If height and width are the same as the image, return the original image.
            if (width == this.Photo.Width && height == this.Photo.Height)
            {
                this.Photo.Save(destination, this.Photo.RawFormat);
                return destination.Length;
            }

            // Create the destination and source rectangles.
            // These rectangles are used by the Graphics object to modify the dimensions of the image.
            var size = new Size(width, height);
            var dest_rect = new Rectangle(0, 0, width, height);
            var source_rect = new Rectangle(0, 0, this.Photo.Width, this.Photo.Height);

            CalculateAutocrop(ref size, ref dest_rect, ref source_rect, offset);

            return Generate(destination, size, dest_rect, source_rect, null, quality, graphicsUnit);
        }

        /// <summary>
        /// Resizing an images canvas.
        /// You must specify at least one dimension (width or height).
        /// If you do not specify one dimension (width or height) that dimension will default to the original image dimension.
        /// You must specify a fillColor.
        /// </summary>
        /// <param name="destination">Stream to place the new image.</param>
        /// <param name="size">Size of the new image.</param>
        /// <param name="fillColor">Background color of image if the size of the new image is greater than the destination rectangle.</param>
        /// <param name="quality">Quality of the image.</param>
        /// <param name="graphicsUnit">GraphicsUnit option.</param>
        /// <returns>Size of the new image in bytes</returns>
        public long Canvas(Stream destination, Size size, Color fillColor, long quality = 0, GraphicsUnit graphicsUnit = GraphicsUnit.Pixel)
        {
            Validation.Parameter.AssertIsNotNull(size, "size");
            Validation.Parameter.AssertMinRange(size.Width, 0, "size.Width", Resources.Strings.Exception_ImageHelper_InvalidResizeWidth);
            Validation.Parameter.AssertMinRange(size.Height, 0, "size.Height", Resources.Strings.Exception_ImageHelper_InvalidResizeHeight);
            Validation.Parameter.AssertIsNotValue(size.Width + size.Height, 0, "size", Resources.Strings.Exception_InvalidSize);

            return Canvas(destination, size.Width, size.Height, fillColor, quality, graphicsUnit);
        }

        /// <summary>
        /// Resizing an images canvas.
        /// You must specify at least one dimension (width or height).
        /// If you do not specify one dimension (width or height) that dimension will default to the original image dimension.
        /// You must specify a fillColor.
        /// </summary>
        /// <param name="destination">Stream to place the new image.</param>
        /// <param name="width">Width of the new image.</param>
        /// <param name="height">Height of the new image.</param>
        /// <param name="fillColor">Background color of image if the size of the new image is greater than the destination rectangle.</param>
        /// <param name="quality">Quality of the image.</param>
        /// <param name="graphicsUnit">GraphicsUnit option.</param>
        /// <returns>Size of the new image in bytes</returns>
        public long Canvas(Stream destination, int width, int height, Color fillColor, long quality = 0, GraphicsUnit graphicsUnit = GraphicsUnit.Pixel)
        {
            Validation.Parameter.AssertIsNotNull(destination, "destination");
            Validation.Parameter.AssertIsValue(destination.CanWrite, true, "destination.CanWrite", Resources.Strings.Exception_Stream_IsCanWrite);
            Validation.Parameter.AssertIsValue(destination.CanSeek, true, "destination.CanSeek", Resources.Strings.Exception_Stream_IsCanSeek);
            Validation.Parameter.AssertMinRange(width, 0, "width", Resources.Strings.Exception_ImageHelper_InvalidResizeWidth);
            Validation.Parameter.AssertMinRange(height, 0, "height", Resources.Strings.Exception_ImageHelper_InvalidResizeHeight);
            Validation.Parameter.AssertIsNotValue(width + height, 0, "width,height", Resources.Strings.Exception_InvalidSize);
            Validation.Parameter.AssertIsNotNull(fillColor, "fillColor");
            Validation.Parameter.AssertRange(quality, 0, 100, "quality", Resources.Strings.Exception_ImageHelper_InvalidQuality);

            // Initialize default values.
            Initialization.Parameter.AssertIsNotDefault(ref width, this.Photo.Width);
            Initialization.Parameter.AssertIsNotDefault(ref height, this.Photo.Height);

            // If height and width are the same as the image, return the original image.
            if (width == this.Photo.Width && height == this.Photo.Height)
            {
                this.Photo.Save(destination, this.Photo.RawFormat);
                return destination.Length;
            }

            // Create the destination and source rectangles.
            // These rectangles are used by the Graphics object to modify the dimensions of the image.
            var size = new Size(width, height);
            var dest_rect = new Rectangle(0, 0, width, height);
            var source_rect = new Rectangle(0, 0, this.Photo.Width, this.Photo.Height);

            CalculateCanvas(ref size, ref dest_rect, ref source_rect, true);

            return Generate(destination, size, dest_rect, source_rect, fillColor, quality, graphicsUnit);
        }

        /// <summary>
        /// Crop the image.
        /// You must plot the crop by setting at least one property greater than '0' (X, Y, Width, Height).
        /// Crop cannot polt a larger crop than the size of the original image.
        /// If you want a larger image without resizing use the Canvas method.
        /// </summary>
        /// <param name="destination">Stream to place the new image.</param>
        /// <param name="plot">Plot dimensions to crop image.</param>
        /// <param name="quality">Quality of the image.</param>
        /// <param name="graphicsUnit">GraphicsUnit option.</param>
        /// <returns>Size of the new image in bytes</returns>
        public long Crop(Stream destination, Rectangle plot, long quality = 0, GraphicsUnit graphicsUnit = GraphicsUnit.Pixel)
        {
            Validation.Parameter.AssertIsNotNull(plot, "plot");
            Validation.Parameter.AssertRange(plot.X, 0, this.Photo.Width - 1, "plot.X", Resources.Strings.Exception_ImageHelper_InvalidX);
            Validation.Parameter.AssertRange(plot.Y, 0, this.Photo.Height - 1, "plot.Y", Resources.Strings.Exception_ImageHelper_InvalidY);
            Validation.Parameter.AssertRange(plot.Width, 0, this.Photo.Width, "plot.Width", Resources.Strings.Exception_ImageHelper_InvalidWidth);
            Validation.Parameter.AssertRange(plot.Height, 0, this.Photo.Height, "plot.Height", Resources.Strings.Exception_ImageHelper_InvalidHeight);
            Validation.Parameter.AssertRange(plot.X + plot.Width, 0, this.Photo.Width - plot.X, "plot.X, plot.Width", Resources.Strings.Exception_ImageHelper_InvalidXWidth);
            Validation.Parameter.AssertRange(plot.Y + plot.Height, 0, this.Photo.Height - plot.Y, "plot.Y, plot.Height", Resources.Strings.Exception_ImageHelper_InvalidYHeight);
            Validation.Parameter.AssertIsNotValue(plot.X + plot.Y + plot.Width + plot.Height, 0, "plot", Resources.Strings.Exception_ImageHelper_InvalidPlot);

            return Crop(destination, plot.X, plot.Y, plot.Width, plot.Height, quality, graphicsUnit);
        }

        /// <summary>
        /// Crop the image.
        /// You must plot the crop by setting at least one property greater than '0' (xPosition, yPosition, Width, Height).
        /// Crop cannot polt a larger crop than the size of the original image.
        /// If you want a larger image without resizing use the Canvas method.
        /// </summary>
        /// <param name="destination">Stream to place the new image.</param>
        /// <param name="xPosition">X-coordinate which is the upper left corner.</param>
        /// <param name="yPosition">Y-coordinate which is the upper left corner.</param>
        /// <param name="width">Width of new image.</param>
        /// <param name="height">Height of new image.</param>
        /// <param name="quality">Quality of the image.</param>
        /// <param name="graphicsUnit">GraphicsUnit option.</param>
        /// <returns>Size of the new image in bytes</returns>
        public long Crop(Stream destination, int xPosition, int yPosition, int width, int height, long quality = 0, GraphicsUnit graphicsUnit = GraphicsUnit.Pixel)
        {
            Validation.Parameter.AssertIsNotNull(destination, "destination");
            Validation.Parameter.AssertIsValue(destination.CanWrite, true, "destination.CanWrite", Resources.Strings.Exception_Stream_IsCanWrite);
            Validation.Parameter.AssertIsValue(destination.CanSeek, true, "destination.CanSeek", Resources.Strings.Exception_Stream_IsCanSeek);
            Validation.Parameter.AssertRange(xPosition, 0, this.Photo.Width - 1, "xPosition", Resources.Strings.Exception_ImageHelper_InvalidX);
            Validation.Parameter.AssertRange(yPosition, 0, this.Photo.Height - 1, "yPosition", Resources.Strings.Exception_ImageHelper_InvalidY);
            Validation.Parameter.AssertRange(width, 0, this.Photo.Width, "width", Resources.Strings.Exception_ImageHelper_InvalidWidth);
            Validation.Parameter.AssertRange(height, 0, this.Photo.Height, "height", Resources.Strings.Exception_ImageHelper_InvalidHeight);
            Validation.Parameter.AssertRange(xPosition + width, 0, this.Photo.Width - xPosition, "xPosition, width", Resources.Strings.Exception_ImageHelper_InvalidXWidth);
            Validation.Parameter.AssertRange(yPosition + height, 0, this.Photo.Height - yPosition, "yPosition, height", Resources.Strings.Exception_ImageHelper_InvalidYHeight);
            Validation.Parameter.AssertIsNotValue(width + height + xPosition + yPosition, 0, "xPosition, yPosition, width, height", Resources.Strings.Exception_ImageHelper_InvalidPlot);
            Validation.Parameter.AssertRange(quality, 0, 100, "quality", Resources.Strings.Exception_ImageHelper_InvalidQuality);

            // Calculate thee width based on the xPosition.
            if (xPosition > 0 && (width == 0 || (xPosition + width) > this.Photo.Width))
                width = this.Photo.Width - xPosition;
            // Default the width if it wasn't set to the source image width.
            else if (xPosition == 0 && width == 0)
                width = this.Photo.Width;

            // Calculate the height based on the yPosition.
            if (yPosition > 0 && (height == 0 || (yPosition + height) > this.Photo.Height))
                height = this.Photo.Height - yPosition;
            // Default the height if it wasn't set to the source image height.
            else if (yPosition == 0 & height == 0)
                height = this.Photo.Height;

            // If the x and y coordinates are at 0.
            // If height and width are the same as the image, return the original image.
            if (xPosition == 0 && yPosition == 0 && width == this.Photo.Width && height == this.Photo.Height)
            {
                this.Photo.Save(destination, this.Photo.RawFormat);
                return destination.Length;
            }

            // Create the destination and source rectangles.
            // These rectangles are used by the Graphics object to modify the dimensions of the image.
            var size = new Size(width, height);
            var dest_rect = new Rectangle(0, 0, width, height);
            var source_rect = new Rectangle(xPosition, yPosition, width, height);

            CalculateCrop(ref size, ref dest_rect, ref source_rect);

            return Generate(destination, size, dest_rect, source_rect, null, quality, graphicsUnit);
        }

        /// <summary>
        /// Resizing an image.
        /// You must specify at least one dimension (width or height).
        /// If you do not specify one dimension (width or height) that dimension will default to the original image dimension.
        /// Resizing an image will distort an image if a fillColor is not specified.  If you need to maintain scale, use the Sale method instead.
        /// Specify a fillColor if you want the image to allow for whitespace.
        /// </summary>
        /// <param name="destination">Stream to place the new image.</param>
        /// <param name="size">Size of the new image.</param>
        /// <param name="fillColor">
        ///     Background color of image if the size of the new image is greater than the destination rectangle.
        ///     If a fillColor is not specified it will not allow whitespace, which means an image will distort.
        ///     Specify a fillColor if you want the image to scale and allow for whitespace.
        /// </param>
        /// <param name="quality">Quality of the image.</param>
        /// <param name="graphicsUnit">GraphicsUnit option.</param>
        /// <returns>Size of the new image in bytes</returns>
        public long Resize(Stream destination, Size size, Color? fillColor = null, long quality = 0, GraphicsUnit graphicsUnit = GraphicsUnit.Pixel)
        {
            Validation.Parameter.AssertIsNotNull(size, "size");
            Validation.Parameter.AssertMinRange(size.Width, 0, "size.Width", Resources.Strings.Exception_ImageHelper_InvalidResizeWidth);
            Validation.Parameter.AssertMinRange(size.Height, 0, "size.Height", Resources.Strings.Exception_ImageHelper_InvalidResizeHeight);
            Validation.Parameter.AssertIsNotValue(size.Width + size.Height, 0, "size", Resources.Strings.Exception_InvalidSize);

            return Resize(destination, size.Width, size.Height, fillColor, quality, graphicsUnit);
        }

        /// <summary>
        /// Resizing an image.
        /// You must specify at least one dimension (width or height).
        /// If you do not specify one dimension (width or height) that dimension will default to the original image dimension.
        /// Resizing an image will distort an image if a fillColor is not specified.  If you need to maintain scale, use the Sale method instead.
        /// Specify a fillColor if you want the image to allow for whitespace.
        /// </summary>
        /// <param name="destination">Stream to place the new image.</param>
        /// <param name="width">Width of the new image.</param>
        /// <param name="height">Height of the new image.</param>
        /// <param name="fillColor">
        ///     Background color of image if the size of the new image is greater than the destination rectangle.
        ///     If a fillColor is not specified it will not allow whitespace, which means an image will distort.
        ///     Specify a fillColor if you want the image to scale and allow for whitespace.
        /// </param>
        /// <param name="quality">Quality of the image.</param>
        /// <param name="graphicsUnit">GraphicsUnit option.</param>
        /// <returns>Size of the new image in bytes</returns>
        public long Resize(Stream destination, int width, int height, Color? fillColor = null, long quality = 0, GraphicsUnit graphicsUnit = GraphicsUnit.Pixel)
        {
            Validation.Parameter.AssertIsNotNull(destination, "destination");
            Validation.Parameter.AssertIsValue(destination.CanWrite, true, "destination.CanWrite", Resources.Strings.Exception_Stream_IsCanWrite);
            Validation.Parameter.AssertIsValue(destination.CanSeek, true, "destination.CanSeek", Resources.Strings.Exception_Stream_IsCanSeek);
            Validation.Parameter.AssertMinRange(width, 0, "width", Resources.Strings.Exception_ImageHelper_InvalidResizeWidth);
            Validation.Parameter.AssertMinRange(height, 0, "height", Resources.Strings.Exception_ImageHelper_InvalidResizeHeight);
            Validation.Parameter.AssertIsNotValue(width + height, 0, "width,height", Resources.Strings.Exception_InvalidSize);
            Validation.Parameter.AssertRange(quality, 0, 100, "quality", Resources.Strings.Exception_ImageHelper_InvalidQuality);

            // Initialize default values.
            Initialization.Parameter.AssertIsNotDefault(ref width, this.Photo.Width);
            Initialization.Parameter.AssertIsNotDefault(ref height, this.Photo.Height);

            // If height and width are the same as the image, return the original image.
            if (width == this.Photo.Width && height == this.Photo.Height)
            {
                this.Photo.Save(destination, this.Photo.RawFormat);
                return destination.Length;
            }

            // Create the destination and source rectangles.
            // These rectangles are used by the Graphics object to modify the dimensions of the image.
            var size = new Size(width, height);
            var dest_rect = new Rectangle(0, 0, width, height);
            var source_rect = new Rectangle(0, 0, this.Photo.Width, this.Photo.Height);

            CalculateResize(ref size, ref dest_rect, ref source_rect, fillColor.HasValue);

            return Generate(destination, size, dest_rect, source_rect, fillColor, quality, graphicsUnit);
        }

        /// <summary>
        /// Resize an image but maintain the original images scale ratio.
        /// </summary>
        /// <param name="destination">Stream to place the new image.</param>
        /// <param name="option">Choose width or height to scale in that direction.</param>
        /// <param name="size">Size of the new image.</param>
        /// <param name="quality">Quality of the image.</param>
        /// <param name="graphicsUnit">GraphicsUnit option.</param>
        /// <returns>Size of the new image in bytes</returns>
        public long Scale(Stream destination, ImageScaleDirection option, int size, long quality = 0, GraphicsUnit graphicsUnit = GraphicsUnit.Pixel)
        {
            Validation.Parameter.AssertIsNotNull(destination, "destination");
            Validation.Parameter.AssertIsValue(destination.CanWrite, true, "destination.CanWrite", Resources.Strings.Exception_Stream_IsCanWrite);
            Validation.Parameter.AssertIsValue(destination.CanSeek, true, "destination.CanSeek", Resources.Strings.Exception_Stream_IsCanSeek);
            Validation.Parameter.AssertMinRange(size, 1, "size");
            Validation.Parameter.AssertRange(quality, 0, 100, "quality", Resources.Strings.Exception_ImageHelper_InvalidQuality);

            // If height and width are the same as the image, return the original image.
            if ((option == ImageScaleDirection.Width && size == this.Photo.Width)
                || (option == ImageScaleDirection.Height && size == this.Photo.Height))
            {
                this.Photo.Save(destination, this.Photo.RawFormat);
                return destination.Length;
            }

            // Create the destination and source rectangles.
            // These rectangles are used by the Graphics object to modify the dimensions of the image.
            var image_size = new Size(option == ImageScaleDirection.Width ? size : 0, option == ImageScaleDirection.Height ? size : 0);
            var dest_rect = new Rectangle(0, 0, option == ImageScaleDirection.Width ? size : 0, option == ImageScaleDirection.Height ? size : 0);
            var source_rect = new Rectangle(0, 0, this.Photo.Width, this.Photo.Height);

            CalculateScale(ref image_size, ref dest_rect, ref source_rect);

            return Generate(destination, image_size, dest_rect, source_rect, null, quality, graphicsUnit);
        }

        /// <summary>
        /// Optimize the image size by lowering the quality of the image.
        /// </summary>
        /// <param name="destination">Stream to place the new image.</param>
        /// <param name="quality">Quality of the image.</param>
        /// <returns>Size of the new image in bytes</returns>
        public long Optimize(Stream destination, long quality)
        {
            Validation.Parameter.AssertIsNotNull(destination, "destination");
            Validation.Parameter.AssertIsValue(destination.CanWrite, true, "destination.CanWrite", Resources.Strings.Exception_Stream_IsCanWrite);
            Validation.Parameter.AssertIsValue(destination.CanSeek, true, "destination.CanSeek", Resources.Strings.Exception_Stream_IsCanSeek);
            Validation.Parameter.AssertRange(quality, 1, 100, "quality", Resources.Strings.Exception_ImageHelper_InvalidQuality);

            // Create the destination and source rectangles.
            // These rectangles are used by the Graphics object to modify the dimensions of the image.
            var dest_rect = new Rectangle(0, 0, this.Photo.Width, this.Photo.Height);
            var source_rect = new Rectangle(0, 0, this.Photo.Width, this.Photo.Height);

            return Generate(destination, this.Photo.Size, dest_rect, source_rect, null, quality, GraphicsUnit.Pixel);
        }

        /// <summary>
        /// Generate the new image.
        /// </summary>
        /// <param name="destination">Stream to place the new image.</param>
        /// <param name="imageSize">Size of the new image.</param>
        /// <param name="destRect">Graphics destination rectangle.</param>
        /// <param name="sourceRect">Graphics source rectangle.</param>
        /// <param name="fillColor">Fill color if the new image is resized to larger dimensions.</param>
        /// <param name="quality">Quality of the image.</param>
        /// <param name="graphicsUnit">GraphicsUnit option.</param>
        /// <returns>Size of the new image in bytes</returns>
        private long Generate(Stream destination, Size imageSize, Rectangle destRect, Rectangle sourceRect, Color? fillColor = null, long quality = 0, GraphicsUnit graphicsUnit = GraphicsUnit.Pixel)
        {
            using (var bitmap = new Bitmap(imageSize.Width, imageSize.Height))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.InterpolationMode = this.InterpolationMode;

                    // Fill graphics with a background color.
                    if (fillColor.HasValue
                        && (this.Photo.Width < imageSize.Width || this.Photo.Height < imageSize.Height))
                        graphics.Clear(fillColor.Value);

                    graphics.DrawImage(this.Photo, destRect, sourceRect, graphicsUnit);

                    EncoderParameters encoder_params;
                    if (this.EncoderParameters == null)
                    {
                        // Initialize default values.
                        Initialization.Parameter.AssertRange(ref quality, 1, 100, _DefaultImageQuality);
                        encoder_params = new System.Drawing.Imaging.EncoderParameters(1);
                        encoder_params.Param[0] = new EncoderParameter(Encoder.Quality, quality);
                    }
                    else
                    {
                        // Use the overridden quality parameter and make sure there arent duplicate quality params in the EncoderParameters.
                        if (quality > 0)
                        {
                            var duplicate = this.EncoderParameters.Param.Count(p => p.Encoder == Encoder.Quality);
                            encoder_params = new EncoderParameters(this.EncoderParameters.Param.Length + 1 - duplicate);
                            // Make sure there isn't two Encoder.Quality parameters.
                            for (var i = 0; i < this.EncoderParameters.Param.Length; i++)
                            {
                                if (this.EncoderParameters.Param[i].Encoder == Encoder.Quality)
                                    encoder_params.Param[i] = new EncoderParameter(Encoder.Quality, quality);
                                else
                                    encoder_params.Param[i] = this.EncoderParameters.Param[i];
                            }
                        }
                        else
                            encoder_params = this.EncoderParameters;
                    }

                    bitmap.Save(destination, ImageHelper.GetEncoder(this.Photo.RawFormat), encoder_params);
                }
            }

            return destination.Length;
        }
        
        /// <summary>
        /// Calculate the size, destination rectangle and source rectangle that will be used to create the new image.
        /// </summary>
        /// <param name="size">The size of the new image.</param>
        /// <param name="dest">Rectangle for destination image.</param>
        /// <param name="source">Rectangle for source image.</param>
        /// <param name="allowWhitespace">Allow the image size be larger than the destination rectangle size.</param>
        protected void CalculateAutocrop(ref Size size, ref Rectangle dest, ref Rectangle source, Mathematics.CenterPoint offset)
        {
            dest = Mathematics.MathHelper.Scale(this.Photo.Size, size, offset);
        }

        /// <summary>
        /// Calculate the size, destination rectangle and source rectangle that will be used to create the new image.
        /// </summary>
        /// <param name="size">The size of the new image.</param>
        /// <param name="dest">Rectangle for destination image.</param>
        /// <param name="source">Rectangle for source image.</param>
        /// <param name="allowWhitespace">Allow the image size be larger than the destination rectangle size.</param>
        protected void CalculateCanvas(ref Size size, ref Rectangle dest, ref Rectangle source, bool allowWhitespace)
        {
            source.Width = this.Photo.Width;
            source.Height = this.Photo.Height;
            dest.Width = this.Photo.Width;
            dest.Height = this.Photo.Height;

            if (allowWhitespace)
            {
                // Center image inside new larger canvas.
                dest.X = (int)((size.Width - this.Photo.Width) / 2);
                dest.Y = (int)((size.Height - this.Photo.Height) / 2);
            }
            else
            {
                // Return original image.
                if (size.Width > this.Photo.Width && size.Height > this.Photo.Height)
                {
                    size.Width = this.Photo.Width;
                    size.Height = this.Photo.Height;
                }
                // Crop the height.
                else if (size.Width > this.Photo.Width)
                {
                    size.Width = this.Photo.Width;
                }
                // Crop the width.
                else
                {
                    size.Height = this.Photo.Height;
                }
            }
        }

        /// <summary>
        /// Calculate the size, destination rectangle and source rectangle that will be used to create the new image.
        /// </summary>
        /// <param name="size">The size of the new image.</param>
        /// <param name="dest">Rectangle for destination image.</param>
        /// <param name="source">Rectangle for source image.</param>
        protected void CalculateCrop(ref Size size, ref Rectangle dest, ref Rectangle source)
        {
            // At present this is not required, but if the class is overridden that class can calculate crop rectangles.
        }

        /// <summary>
        /// Calculate the size, destination rectangle and source rectangle that will be used to create the new image.
        /// </summary>
        /// <param name="size">The size of the new image.</param>
        /// <param name="dest">Rectangle for destination image.</param>
        /// <param name="source">Rectangle for source image.</param>
        /// <param name="allowWhitespace">Allow the image size be larger than the destination rectangle size.</param>
        protected void CalculateResize(ref Size size, ref Rectangle dest, ref Rectangle source, bool allowWhitespace)
        {
            // Less than '1' means we are resizing to a larger width.
            var ratio_width = (float)this.Photo.Width / size.Width;
            // Less than '1' means we are resizing to a larger height.
            var ratio_height = (float)this.Photo.Height / size.Height;

            // Distort the image.
            if (!allowWhitespace)
            {
                dest.Width = size.Width;
                dest.Height = size.Height;
            }
            else
            {
                // The new image is the same or larger in both directions.
                if (ratio_width <= 1 && ratio_height <= 1)
                {
                    // No change with width, only height.
                    if (ratio_width == 1)
                    {
                        dest.Width = this.Photo.Width;
                        dest.Height = this.Photo.Height;
                        dest.Y = (int)((size.Height - dest.Height) / 2);
                    }
                    // No change with height, only width.
                    else if (ratio_height == 1)
                    {
                        dest.Width = this.Photo.Width;
                        dest.Height = this.Photo.Height;
                        dest.X = (int)((size.Width - dest.Width) / 2);
                    }
                    // Keep the width ratio as it is less of a ratio change.
                    else if (ratio_width < ratio_height)
                    {
                        dest.Width = (int)(size.Width * ratio_height);
                        dest.Height = size.Height;
                        dest.X = (int)((size.Width - dest.Width) / 2);
                    }
                    // Keep the height ratio as it is less of a ratio change.
                    else
                    {
                        dest.Width = size.Width;
                        dest.Height = (int)(size.Height * ratio_width);
                        dest.Y = (int)((size.Height - dest.Height) / 2);
                    }
                }
                // Shrinking width, expanding height.
                else if (ratio_width > 1 && ratio_height <= 1)
                {
                    dest.Width = size.Width;
                    dest.Height = (int)(size.Width * ratio_height);
                    dest.Y = (int)((size.Height - dest.Height) / 2);
                }
                // Shrinking height, expanding width
                else if (ratio_height > 1 && ratio_width <= 1)
                {
                    dest.Width = (int)(this.Photo.Width / ratio_height);
                    dest.Height = size.Height;
                    dest.X = (int)((size.Width - dest.Width) / 2);
                }
                // Shrinking image.
                else
                {
                    // Keep the width ratio as it is less of a ratio change.
                    if (ratio_width < ratio_height)
                    {
                        dest.Width = (int)(size.Width / ratio_width);
                        dest.Height = size.Height;
                        dest.X = (int)((size.Width - dest.Width) / 2);
                    }
                    // Keep the height ratio as it is less of a ratio change.
                    else
                    {
                        dest.Width = size.Width;
                        dest.Height = (int)(size.Height / ratio_height);
                        dest.Y = (int)((size.Height - dest.Height) / 2);
                    }
                }
            }
        }

        /// <summary>
        /// Calculate the size, destination rectangle and source rectangle that will be used to create the new image.
        /// </summary>
        /// <param name="size">The size of the new image.</param>
        /// <param name="dest">Rectangle for destination image.</param>
        /// <param name="source">Rectangle for source image.</param>
        protected void CalculateScale(ref Size size, ref Rectangle dest, ref Rectangle source)
        {
            // Calculate the width based on the height.
            if (size.Width == 0)
            {
                var ratio = (float)dest.Height / (float)source.Height;
                dest.Width = (int)(source.Width * ratio);
                size.Width = dest.Width;
            }
            // Calculate the height based on the width.
            else if (size.Height == 0)
            {
                var ratio = (float)dest.Width / (float)source.Width;
                dest.Height = (int)(source.Height * ratio);
                size.Height = dest.Height;
            }
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
