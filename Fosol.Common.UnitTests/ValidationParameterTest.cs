using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fosol.Common.UnitTests
{
    [TestClass]
    public class ValidationParameterTest
    {
        [TestMethod]
        public void AssertIsNotNull()
        {
            try
            {
                Validation.Assert.IsNotNull(null, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
            }
            
            try
            {
                Validation.Assert.IsNotNull(null, "message", new Exception());
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
            }

            try
            {
                Validation.Assert.IsNotNull(null, "param", "message");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void AssertIsNotNullOrEmpty()
        {
            string value = null;
            try
            {
                Validation.Assert.IsNotNullOrEmpty(value, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
            }

            try
            {
                Validation.Assert.IsNotNullOrEmpty(value, "param", "message", new Exception());
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
            }

            try
            {
                Validation.Assert.IsNotNullOrEmpty(value, "param", "message");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
            }

            value = "";
            try
            {
                Validation.Assert.IsNotNullOrEmpty(value, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }

            try
            {
                Validation.Assert.IsNotNullOrEmpty(value, "param", "message", new Exception());
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }

            try
            {
                Validation.Assert.IsNotNullOrEmpty(value, "param", "message");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }

            string[] value1 = null;
            try
            {
                Validation.Assert.IsNotNullOrEmpty(value1, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
            }

            try
            {
                Validation.Assert.IsNotNullOrEmpty(value1, "param", "message", new Exception());
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
            }

            try
            {
                Validation.Assert.IsNotNullOrEmpty(value1, "param", "message");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
            }

            value1 = new string[0];
            try
            {
                Validation.Assert.IsNotNullOrEmpty(value1, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }

            try
            {
                Validation.Assert.IsNotNullOrEmpty(value1, "param", "message", new Exception());
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }

            try
            {
                Validation.Assert.IsNotNullOrEmpty(value1, "param", "message");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }
        }

        [TestMethod]
        public void AssertMinRange()
        {
            Validation.Assert.MinRange((decimal)1, 0, "param");
            Validation.Assert.MinRange((double)1, 0, "param");
            Validation.Assert.MinRange((int)1, 0, "param");
            Validation.Assert.MinRange((long)1, 0, "param");
            Validation.Assert.MinRange((float)1, 0, "param");

            try
            {
                decimal value = -1;
                Validation.Assert.MinRange(value, 0, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }

            try
            {
                double value = -1;
                Validation.Assert.MinRange(value, 0, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }

            try
            {
                int value = -1;
                Validation.Assert.MinRange(value, 0, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }

            try
            {
                long value = -1;
                Validation.Assert.MinRange(value, 0, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }

            try
            {
                float value = -1;
                Validation.Assert.MinRange(value, 0, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }
        }

        [TestMethod]
        public void AssertMaxRange()
        {
            Validation.Assert.MaxRange((decimal)1, 1, "param");
            Validation.Assert.MaxRange((double)1, 1, "param");
            Validation.Assert.MaxRange((int)1, 1, "param");
            Validation.Assert.MaxRange((long)1, 1, "param");
            Validation.Assert.MaxRange((float)1, 1, "param");

            try
            {
                decimal value = 1;
                Validation.Assert.MaxRange(value, 0, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }

            try
            {
                double value = 1;
                Validation.Assert.MaxRange(value, 0, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }

            try
            {
                int value = 1;
                Validation.Assert.MaxRange(value, 0, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }

            try
            {
                long value = 1;
                Validation.Assert.MaxRange(value, 0, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }

            try
            {
                float value = 1;
                Validation.Assert.MaxRange(value, 0, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }
        }

        [TestMethod]
        public void AssertRange()
        {
            Validation.Assert.Range((decimal)1, 0, 10, "param");
            Validation.Assert.Range((double)1, 0, 10, "param");
            Validation.Assert.Range((int)1, 0, 10, "param");
            Validation.Assert.Range((long)1, 0, 10, "param");
            Validation.Assert.Range((float)1, 0, 10, "param");
            
            try
            {
                decimal value = 1;
                Validation.Assert.Range(value, 0, 10, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }

            try
            {
                double value = 1;
                Validation.Assert.Range(value, 0, 10, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }

            try
            {
                int value = 1;
                Validation.Assert.Range(value, 0, 10, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }

            try
            {
                long value = 1;
                Validation.Assert.Range(value, 0, 10, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }

            try
            {
                float value = 1;
                Validation.Assert.Range(value, 0, 10, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }
        }
    }
}
