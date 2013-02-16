using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Drawing;

namespace Fosol.Common.UnitTests
{
    [TestClass]
    public class AutocropTest
    {
        [TestMethod]
        public void CropImageLarge()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "CropImageLarge");

            var autocrop = new Helpers.Autocrop(filename);
            var width = 800;
            var height = 500;

            using (var stream = new MemoryStream())
            {
                var size = autocrop.Generate(stream, width, height, 75, Helpers.AutocropMode.Crop, Color.Black);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Width == width);
                Assert.IsTrue(image.Height == height);
            }

            Assert.IsTrue(File.Exists(image_path));
        }

        [TestMethod]
        public void CropImageSmall()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "CropImageSmall");

            var autocrop = new Helpers.Autocrop(filename);
            var width = 200;
            var height = 100;

            using (var stream = new MemoryStream())
            {
                var size = autocrop.Generate(stream, width, height, 75, Helpers.AutocropMode.Crop, Color.Black);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Width == width);
                Assert.IsTrue(image.Height == height);
            }

            Assert.IsTrue(File.Exists(image_path));
        }

        [TestMethod]
        public void CropImageWide()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "CropImageWide");

            var autocrop = new Helpers.Autocrop(filename);
            var width = 800;
            var height = autocrop.Photo.Height;

            using (var stream = new MemoryStream())
            {
                var size = autocrop.Generate(stream, width, height, 75, Helpers.AutocropMode.Crop, Color.Black);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Width == width);
                Assert.IsTrue(image.Height == height);
            }

            Assert.IsTrue(File.Exists(image_path));
        }

        [TestMethod]
        public void CropImageTall()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "CropImageTall");

            var autocrop = new Helpers.Autocrop(filename);
            var width = autocrop.Photo.Width;
            var height = 800;

            using (var stream = new MemoryStream())
            {
                var size = autocrop.Generate(stream, width, height, 75, Helpers.AutocropMode.Crop, Color.Black);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Width == width);
                Assert.IsTrue(image.Height == height);
            }

            Assert.IsTrue(File.Exists(image_path));
        }

        [TestMethod]
        public void CropImageThin()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "CropImageThin");

            var autocrop = new Helpers.Autocrop(filename);
            var width = 200;
            var height = autocrop.Photo.Height;

            using (var stream = new MemoryStream())
            {
                var size = autocrop.Generate(stream, width, height, 75, Helpers.AutocropMode.Crop, Color.Black);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Width == width);
                Assert.IsTrue(image.Height == height);
            }

            Assert.IsTrue(File.Exists(image_path));
        }

        [TestMethod]
        public void CropImageShort()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "CropImageShort");

            var autocrop = new Helpers.Autocrop(filename);
            var width = autocrop.Photo.Width;
            var height = 200;

            using (var stream = new MemoryStream())
            {
                var size = autocrop.Generate(stream, width, height, 75, Helpers.AutocropMode.Crop, Color.Black);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Width == width);
                Assert.IsTrue(image.Height == height);
            }

            Assert.IsTrue(File.Exists(image_path));
        }

        [TestMethod]
        public void ScaleImageLarge()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "ScaleImageLarge");

            var autocrop = new Helpers.Autocrop(filename);
            var width = 800;
            var height = 500;

            using (var stream = new MemoryStream())
            {
                var size = autocrop.Generate(stream, width, height, 75, Helpers.AutocropMode.Scale, Color.Black);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Width == width);
                Assert.IsTrue(image.Height == height);
            }

            Assert.IsTrue(File.Exists(image_path));
        }

        [TestMethod]
        public void ScaleImageSmall()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "ScaleImageSmall");

            var autocrop = new Helpers.Autocrop(filename);
            var width = 300;
            var height = 100;

            using (var stream = new MemoryStream())
            {
                var size = autocrop.Generate(stream, width, height, 75, Helpers.AutocropMode.Scale, Color.Black);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Width == width);
                Assert.IsTrue(image.Height == height);
            }

            Assert.IsTrue(File.Exists(image_path));
        }

        [TestMethod]
        public void ScaleImageWide()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "ScaleImageWide");

            var autocrop = new Helpers.Autocrop(filename);
            var width = 800;
            var height = 0;

            using (var stream = new MemoryStream())
            {
                var size = autocrop.Generate(stream, width, height, 75, Helpers.AutocropMode.Scale, Color.Black);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Width == width);
            }

            Assert.IsTrue(File.Exists(image_path));
        }

        [TestMethod]
        public void ScaleImageTall()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "ScaleImageTall");

            var autocrop = new Helpers.Autocrop(filename);
            var width = 0;
            var height = 800;

            using (var stream = new MemoryStream())
            {
                var size = autocrop.Generate(stream, width, height, 75, Helpers.AutocropMode.Scale, Color.Black);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Height == height);
            }

            Assert.IsTrue(File.Exists(image_path));
        }

        [TestMethod]
        public void ScaleImageThin()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "ScaleImageThin");

            var autocrop = new Helpers.Autocrop(filename);
            var width = 200;
            var height = 0;

            using (var stream = new MemoryStream())
            {
                var size = autocrop.Generate(stream, width, height, 75, Helpers.AutocropMode.Scale, Color.Black);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Width == width);
            }

            Assert.IsTrue(File.Exists(image_path));
        }

        [TestMethod]
        public void ScaleImageShort()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "ScaleImageShort");

            var autocrop = new Helpers.Autocrop(filename);
            var width = 0;
            var height = 200;

            using (var stream = new MemoryStream())
            {
                var size = autocrop.Generate(stream, width, height, 75, Helpers.AutocropMode.Scale, Color.Black);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Height == height);
            }

            Assert.IsTrue(File.Exists(image_path));
        }

        [TestMethod]
        public void StretchImageWide()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "stretch_wider");

            var autocrop = new Helpers.Autocrop(filename);
            var width = 800;
            var height = autocrop.Photo.Height;

            using (var stream = new MemoryStream())
            {
                var size = autocrop.Generate(stream, width, height, 75, Helpers.AutocropMode.Scale, Color.Black);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Width == width);
                Assert.IsTrue(image.Height == height);
            }

            Assert.IsTrue(File.Exists(image_path));
        }

        [TestMethod]
        public void StretchImageTall()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "stretch_taller");

            var autocrop = new Helpers.Autocrop(filename);
            var width = autocrop.Photo.Width;
            var height = 800;

            using (var stream = new MemoryStream())
            {
                var size = autocrop.Generate(stream, width, height, 75, Helpers.AutocropMode.Scale, Color.Black);

                var image = Image.FromStream(stream);
                image.Save(image_path);

                Assert.IsTrue(image.Width == width);
                Assert.IsTrue(image.Height == height);
            }

            Assert.IsTrue(File.Exists(image_path));
        }
    }
}
