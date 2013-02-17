using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Drawing;

namespace Fosol.Common.UnitTests
{
    [TestClass]
    public class ImageHelperTest
    {
        #region AutoAutocrop
        [TestMethod]
        public void AutocropLarge()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "AutocropLarge");

            var image_helper = new Helpers.ImageHelper(filename);
            var width = 800;
            var height = 600;

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Autocrop(stream, width, height, 75);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Width == width);
                Assert.IsTrue(image.Height == height);
            }

            Assert.IsTrue(File.Exists(image_path));
        }

        [TestMethod]
        public void AutocropSmall()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "AutocropSmall");

            var image_helper = new Helpers.ImageHelper(filename);
            var width = 500;
            var height = 200;

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Autocrop(stream, width, height, 75);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Width == width);
                Assert.IsTrue(image.Height == height);
            }

            Assert.IsTrue(File.Exists(image_path));
        }

        [TestMethod]
        public void AutocropTall()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "AutocropTall");

            var image_helper = new Helpers.ImageHelper(filename);
            var width = 0;
            var height = 800;

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Autocrop(stream, width, height, 75);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Width == image_helper.Photo.Width);
                Assert.IsTrue(image.Height == height);
            }

            Assert.IsTrue(File.Exists(image_path));
        }

        [TestMethod]
        public void AutocropThin()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "AutocropThin");

            var image_helper = new Helpers.ImageHelper(filename);
            var width = 500;
            var height = 0;

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Autocrop(stream, width, height, 75);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Width == width);
                Assert.IsTrue(image.Height == image_helper.Photo.Height);
            }

            Assert.IsTrue(File.Exists(image_path));
        }

        [TestMethod]
        public void AutocropShort()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "AutocropShort");

            var image_helper = new Helpers.ImageHelper(filename);
            var width = 0;
            var height = 350;

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Autocrop(stream, width, height, 75);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Width == image_helper.Photo.Width);
                Assert.IsTrue(image.Height == height);
            }

            Assert.IsTrue(File.Exists(image_path));
        }

        [TestMethod]
        public void AutocropLargePortrait()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "AutocropLargePortrait");

            var image_helper = new Helpers.ImageHelper(filename);
            var width = 800;
            var height = 600;

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Autocrop(stream, width, height, Mathematics.CenterOption.Portrait, 75);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Width == width);
                Assert.IsTrue(image.Height == height);
            }

            Assert.IsTrue(File.Exists(image_path));
        }

        [TestMethod]
        public void AutocropSmallPortrait()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "AutocropSmallPortrait");

            var image_helper = new Helpers.ImageHelper(filename);
            var width = 400;
            var height = 300;

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Autocrop(stream, width, height, Mathematics.CenterOption.Portrait, 75);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Width == width);
                Assert.IsTrue(image.Height == height);
            }

            Assert.IsTrue(File.Exists(image_path));
        }
        #endregion

        #region Canvas
        [TestMethod]
        public void CanvasWide()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "CanvasWide");

            var image_helper = new Helpers.ImageHelper(filename);
            var width = 800;
            var height = 0;

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Canvas(stream, width, height, Color.Black, 75);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Width == width);
                Assert.IsTrue(image.Height == image_helper.Photo.Height);
            }

            Assert.IsTrue(File.Exists(image_path));
        }

        [TestMethod]
        public void CanvasTall()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "CanvasTall");

            var image_helper = new Helpers.ImageHelper(filename);
            var width = 0;
            var height = 800;

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Canvas(stream, width, height, Color.Black, 75);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Width == image_helper.Photo.Width);
                Assert.IsTrue(image.Height == height);
            }

            Assert.IsTrue(File.Exists(image_path));
        }

        [TestMethod]
        public void CanvasLarge()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "CanvasLarge");

            var image_helper = new Helpers.ImageHelper(filename);
            var width = 800;
            var height = 800;

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Canvas(stream, width, height, Color.Black, 75);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Width == width);
                Assert.IsTrue(image.Height == width);
            }

            Assert.IsTrue(File.Exists(image_path));
        }

        [TestMethod]
        public void CanvasSmall()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "CanvasSmall");

            var image_helper = new Helpers.ImageHelper(filename);
            var width = 200;
            var height = 200;

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Canvas(stream, width, height, Color.Black, 75);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Width == width);
                Assert.IsTrue(image.Height == width);
            }

            Assert.IsTrue(File.Exists(image_path));
        }
        #endregion

        #region Crop
        [TestMethod]
        public void CropInvalidLarge()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "CropLarge");

            var image_helper = new Helpers.ImageHelper(filename);
            var x = 0;
            var y = 0;
            var width = 800;
            var height = 500;

            using (var stream = new MemoryStream())
            {
                try
                {
                    var size = image_helper.Crop(stream, x, y, width, height, 75);
                }
                catch (Exception ex)
                {
                    Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
                }
            }
                
            Assert.IsFalse(File.Exists(image_path));
        }

        [TestMethod]
        public void CropSmall()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "CropSmall");

            var image_helper = new Helpers.ImageHelper(filename);
            var x = 0;
            var y = 0;
            var width = 200;
            var height = 100;

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Crop(stream, x, y, width, height, 75);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Width == width);
                Assert.IsTrue(image.Height == height);
            }

            Assert.IsTrue(File.Exists(image_path));
        }

        [TestMethod]
        public void CropInvalidWide()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "CropWide");

            var image_helper = new Helpers.ImageHelper(filename);
            var x = 0;
            var y = 0;
            var width = 800;
            var height = 0;

            using (var stream = new MemoryStream())
            {
                try
                {
                    var size = image_helper.Crop(stream, x, y, width, height, 75);
                }
                catch (Exception ex)
                {
                    Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
                }
            }

            Assert.IsFalse(File.Exists(image_path));
        }

        [TestMethod]
        public void CropInvalidTall()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "CropTall");

            var image_helper = new Helpers.ImageHelper(filename);
            var x = 0;
            var y = 0;
            var width = 0;
            var height = 800;

            using (var stream = new MemoryStream())
            {
                try
                {
                    var size = image_helper.Crop(stream, x, y, width, height, 75);
                }
                catch (Exception ex)
                {
                    Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
                }
            }

            Assert.IsFalse(File.Exists(image_path));
        }

        [TestMethod]
        public void CropThin()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "CropThin");

            var image_helper = new Helpers.ImageHelper(filename);
            var x = 0;
            var y = 0;
            var width = 200;
            var height = 0;

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Crop(stream, x, y, width, height, 75);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Width == width);
                Assert.IsTrue(image.Height == image_helper.Photo.Height);
            }

            Assert.IsTrue(File.Exists(image_path));
        }

        [TestMethod]
        public void CropShort()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "CropShort");

            var image_helper = new Helpers.ImageHelper(filename);
            var x = 0;
            var y = 0;
            var width = 0;
            var height = 200;

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Crop(stream, x, y, width, height, 75);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Width == image_helper.Photo.Width);
                Assert.IsTrue(image.Height == height);
            }

            Assert.IsTrue(File.Exists(image_path));
        }

        [TestMethod]
        public void CropInvalidX()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "CropLarge");

            var image_helper = new Helpers.ImageHelper(filename);
            var x = -50;
            var y = 0;
            var width = 0;
            var height = 0;

            using (var stream = new MemoryStream())
            {
                try
                {
                    var size = image_helper.Crop(stream, x, y, width, height, 75);
                }
                catch (Exception ex)
                {
                    Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
                }
            }

            Assert.IsFalse(File.Exists(image_path));
        }

        [TestMethod]
        public void CropInvalidY()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "CropLarge");

            var image_helper = new Helpers.ImageHelper(filename);
            var x = -0;
            var y = 200;
            var width = 0;
            var height = 500;

            using (var stream = new MemoryStream())
            {
                try
                {
                    var size = image_helper.Crop(stream, x, y, width, height, 75);
                }
                catch (Exception ex)
                {
                    Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
                }
            }

            Assert.IsFalse(File.Exists(image_path));
        }
        #endregion

        #region Scale
        [TestMethod]
        public void ScaleWide()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "ScaleWide");

            var image_helper = new Helpers.ImageHelper(filename);
            var width = 800;

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Scale(stream, Helpers.ImageScaleDirection.Width, width, 75);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Width == width);
            }

            Assert.IsTrue(File.Exists(image_path));
        }

        [TestMethod]
        public void ScaleTall()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "ScaleTall");

            var image_helper = new Helpers.ImageHelper(filename);
            var height = 800;

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Scale(stream, Helpers.ImageScaleDirection.Height, height, 75);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Height == height);
            }

            Assert.IsTrue(File.Exists(image_path));
        }

        [TestMethod]
        public void ScaleThin()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "ScaleThin");

            var image_helper = new Helpers.ImageHelper(filename);
            var width = 200;

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Scale(stream, Helpers.ImageScaleDirection.Width, width, 75);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Width == width);
            }

            Assert.IsTrue(File.Exists(image_path));
        }

        [TestMethod]
        public void ScaleShort()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "ScaleShort");

            var image_helper = new Helpers.ImageHelper(filename);
            var height = 200;

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Scale(stream, Helpers.ImageScaleDirection.Height, height, 75);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Height == height);
            }

            Assert.IsTrue(File.Exists(image_path));
        }
        #endregion

        #region Resize
        [TestMethod]
        public void ResizeScaleWide()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "ResizeScaleWide");

            var image_helper = new Helpers.ImageHelper(filename);
            var width = 800;
            var height = 0;

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Resize(stream, width, height, Color.Black, 75);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Width == width);
                Assert.IsTrue(image.Height == image_helper.Photo.Height);
            }

            Assert.IsTrue(File.Exists(image_path));
        }

        [TestMethod]
        public void ResizeScaleWideShort()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "ResizeScaleWideShort");

            var image_helper = new Helpers.ImageHelper(filename);
            var width = 700;
            var height = 50;

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Resize(stream, width, height, Color.Black, 75);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Width == width);
                Assert.IsTrue(image.Height == height);
            }

            Assert.IsTrue(File.Exists(image_path));
        }

        [TestMethod]
        public void ResizeScaleTall()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "ResizeScaleTall");

            var image_helper = new Helpers.ImageHelper(filename);
            var width = 0;
            var height = 800;

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Resize(stream, width, height, Color.Black, 75);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Width == image_helper.Photo.Width);
                Assert.IsTrue(image.Height == height);
            }

            Assert.IsTrue(File.Exists(image_path));
        }

        [TestMethod]
        public void ResizeScaleTallThin()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "ResizeScaleTallThin");

            var image_helper = new Helpers.ImageHelper(filename);
            var width = 200;
            var height = 800;

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Resize(stream, width, height, Color.Black, 75);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Width == width);
                Assert.IsTrue(image.Height == height);
            }

            Assert.IsTrue(File.Exists(image_path));
        }

        [TestMethod]
        public void ResizeScaleLarge()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "ResizeScaleLarge");

            var image_helper = new Helpers.ImageHelper(filename);
            var width = 800;
            var height = 800;

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Resize(stream, width, height, Color.Black, 75);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Width == width);
                Assert.IsTrue(image.Height == height);
            }

            Assert.IsTrue(File.Exists(image_path));
        }

        [TestMethod]
        public void ResizeScaleSmall()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "ResizeScaleSmall");

            var image_helper = new Helpers.ImageHelper(filename);
            var width = 200;
            var height = 200;

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Resize(stream, width, height, Color.Black, 75);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Width == width);
                Assert.IsTrue(image.Height == height);
            }

            Assert.IsTrue(File.Exists(image_path));
        }

        [TestMethod]
        public void ResizeWide()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "ResizeWide");

            var image_helper = new Helpers.ImageHelper(filename);
            var width = 800;
            var height = 0;

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Resize(stream, width, height, null, 75);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Width == width);
                Assert.IsTrue(image.Height == image_helper.Photo.Height);
            }

            Assert.IsTrue(File.Exists(image_path));
        }

        [TestMethod]
        public void ResizeTall()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "ResizeTall");

            var image_helper = new Helpers.ImageHelper(filename);
            var width = 0;
            var height = 800;

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Resize(stream, width, height, null, 75);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Width == image_helper.Photo.Width);
                Assert.IsTrue(image.Height == height);
            }

            Assert.IsTrue(File.Exists(image_path));
        }

        [TestMethod]
        public void ResizeLarge()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "ResizeLarge");

            var image_helper = new Helpers.ImageHelper(filename);
            var width = 800;
            var height = 800;

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Resize(stream, width, height, null, 75);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Width == width);
                Assert.IsTrue(image.Height == width);
            }

            Assert.IsTrue(File.Exists(image_path));
        }

        [TestMethod]
        public void ResizeSmall()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "ResizeSmall");

            var image_helper = new Helpers.ImageHelper(filename);
            var width = 200;
            var height = 200;

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Resize(stream, width, height, null, 75);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Width == width);
                Assert.IsTrue(image.Height == width);
            }

            Assert.IsTrue(File.Exists(image_path));
        }
        #endregion

        #region Optimize
        [TestMethod]
        public void Optimize100()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "Optimize100");
            var optimize = 100;
            var image_helper = new Helpers.ImageHelper(filename);

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Optimize(stream, optimize);

                var image = Image.FromStream(stream);
                image.Save(image_path);
            }

            Assert.IsTrue(File.Exists(image_path));
        }

        [TestMethod]
        public void Optimize75()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "Optimize75");
            var optimize = 75;
            var image_helper = new Helpers.ImageHelper(filename);

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Optimize(stream, optimize);

                var image = Image.FromStream(stream);
                image.Save(image_path);
            }

            Assert.IsTrue(File.Exists(image_path));
        }

        [TestMethod]
        public void Optimize25()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "Optimize25");
            var optimize = 25;
            var image_helper = new Helpers.ImageHelper(filename);

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Optimize(stream, optimize);

                var image = Image.FromStream(stream);
                image.Save(image_path);
            }

            Assert.IsTrue(File.Exists(image_path));
        }
        #endregion
    }
}
