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
        public void CropImageLarger()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "crop_larger");

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
        public void CropImageSmaller()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "crop_smaller");

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
        public void CropImageWider()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "crop_wider");

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
        public void CropImageTaller()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "crop_taller");

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
        public void ScaleImageLarge()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "scale_larger");

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
        public void ScaleImageSmaller()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "scale_smaller");

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
        public void ScaleImageWider()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "scale_wider");

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
        public void ScaleImageTaller()
        {
            var path = string.Format(@"{0}\Resources\Images\", Directory.GetCurrentDirectory());
            var filename = string.Format(@"{0}{1}", path, "Jeremy Foster 2008.jpg");
            var image_path = string.Format("{0}{1}.jpg", path, "scale_taller");

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
        public void StretchImageWider()
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
        public void StretchImageTaller()
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
