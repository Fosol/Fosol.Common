using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Drawing;

namespace Fosol.Common.UnitTests
{
    [TestClass]
    public class ImageHelperTest
    {
        #region Variables
        private string _Path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
        private string _Filename = "Square.png";
        #endregion

        #region Methods
        private string GetPathToFile()
        {
            return string.Format(@"{0}{1}", _Path, _Filename);
        }

        private string SetPathToFile(string name)
        {
            return string.Format("{0}{1}.{2}", _Path, name, Path.GetExtension(_Filename));
        }
        #endregion

        #region Canvas
        [TestMethod]
        public void CanvasWide()
        {
            var image_helper = new Helpers.ImageHelper(GetPathToFile());
            var image_path = SetPathToFile("CanvasWide");
            var width = 800;
            var height = 0;

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Canvas(stream, width, height, Color.Black);

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
            var image_helper = new Helpers.ImageHelper(GetPathToFile());
            var image_path = SetPathToFile("CanvasTall");
            var width = 0;
            var height = 800;

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Canvas(stream, width, height, Color.Black);

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
            var image_helper = new Helpers.ImageHelper(GetPathToFile());
            var image_path = SetPathToFile("CanvasLarge");
            var width = 800;
            var height = 800;

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Canvas(stream, width, height, Color.Black);

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
            var image_helper = new Helpers.ImageHelper(GetPathToFile());
            var image_path = SetPathToFile("CanvasSmall");
            var width = 200;
            var height = 200;

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Canvas(stream, width, height, Color.Black);

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
            var image_helper = new Helpers.ImageHelper(GetPathToFile());
            var image_path = SetPathToFile("CropInvalidLarge");
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
            var image_helper = new Helpers.ImageHelper(GetPathToFile());
            var image_path = SetPathToFile("CropSmall");
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
            var image_helper = new Helpers.ImageHelper(GetPathToFile());
            var image_path = SetPathToFile("CropInvalidWide");
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
            var image_helper = new Helpers.ImageHelper(GetPathToFile());
            var image_path = SetPathToFile("CropInvalidTall");
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
            var image_helper = new Helpers.ImageHelper(GetPathToFile());
            var image_path = SetPathToFile("CropThin");
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
            var image_helper = new Helpers.ImageHelper(GetPathToFile());
            var image_path = SetPathToFile("CropShort");
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
            var image_helper = new Helpers.ImageHelper(GetPathToFile());
            var image_path = SetPathToFile("CropInvalidX");
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
            var image_helper = new Helpers.ImageHelper(GetPathToFile());
            var image_path = SetPathToFile("CropInvalidY");
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
            var image_helper = new Helpers.ImageHelper(GetPathToFile());
            var image_path = SetPathToFile("ScaleWide");
            var width = 1800;
            var height = 0;

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Scale(stream, width, height);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Width == width);
            }

            Assert.IsTrue(File.Exists(image_path));
        }

        [TestMethod]
        public void ScaleTall()
        {
            var image_helper = new Helpers.ImageHelper(GetPathToFile());
            var image_path = SetPathToFile("ScaleTall");
            var width = 0;
            var height = 1200;

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Scale(stream, width, height);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Height == height);
            }

            Assert.IsTrue(File.Exists(image_path));
        }

        [TestMethod]
        public void ScaleThin()
        {
            var image_helper = new Helpers.ImageHelper(GetPathToFile());
            var image_path = SetPathToFile("ScaleThin");
            var width = 200;
            var height = 0;

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Scale(stream, width, height);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Width == width);
            }

            Assert.IsTrue(File.Exists(image_path));
        }

        [TestMethod]
        public void ScaleShort()
        {
            var image_helper = new Helpers.ImageHelper(GetPathToFile());
            var image_path = SetPathToFile("ScaleShort");
            var width = 0;
            var height = 200;

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Scale(stream, width, height);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Height == height);
            }

            Assert.IsTrue(File.Exists(image_path));
        }
        #endregion

        #region Resize
        [TestMethod]
        public void ResizeWide()
        {
            var image_helper = new Helpers.ImageHelper(GetPathToFile());
            var image_path = SetPathToFile("ResizeWide");
            var width = 800;
            var height = 0;

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Resize(stream, width, height, 75);

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
            var image_helper = new Helpers.ImageHelper(GetPathToFile());
            var image_path = SetPathToFile("ResizeTall");
            var width = 0;
            var height = 800;

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Resize(stream, width, height, 75);

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
            var image_helper = new Helpers.ImageHelper(GetPathToFile());
            var image_path = SetPathToFile("ResizeLarge");
            var width = 800;
            var height = 800;

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Resize(stream, width, height, 75);

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
            var image_helper = new Helpers.ImageHelper(GetPathToFile());
            var image_path = SetPathToFile("ResizeSmall");
            var width = 200;
            var height = 200;

            using (var stream = new MemoryStream())
            {
                var size = image_helper.Resize(stream, width, height, 75);

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
            var image_helper = new Helpers.ImageHelper(GetPathToFile());
            var image_path = SetPathToFile("Optimize100");
            var optimize = 100;

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
            var image_helper = new Helpers.ImageHelper(GetPathToFile());
            var image_path = SetPathToFile("Optimize75");
            var optimize = 75;

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
            var image_helper = new Helpers.ImageHelper(GetPathToFile());
            var image_path = SetPathToFile("Optimize75");
            var optimize = 25;

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
