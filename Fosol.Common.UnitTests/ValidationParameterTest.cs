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
                Validation.Parameter.AssertIsNotNull(null, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
            }
            
            try
            {
                Validation.Parameter.AssertIsNotNull(null, "message", new Exception());
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
            }

            try
            {
                Validation.Parameter.AssertIsNotNull(null, "param", "message");
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
                Validation.Parameter.AssertIsNotNullOrEmpty(value, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
            }

            try
            {
                Validation.Parameter.AssertIsNotNullOrEmpty(value, "param", "message", new Exception());
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
            }

            try
            {
                Validation.Parameter.AssertIsNotNullOrEmpty(value, "param", "message");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
            }

            value = "";
            try
            {
                Validation.Parameter.AssertIsNotNullOrEmpty(value, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }

            try
            {
                Validation.Parameter.AssertIsNotNullOrEmpty(value, "param", "message", new Exception());
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }

            try
            {
                Validation.Parameter.AssertIsNotNullOrEmpty(value, "param", "message");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }

            string[] value1 = null;
            try
            {
                Validation.Parameter.AssertIsNotNullOrEmpty(value1, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
            }

            try
            {
                Validation.Parameter.AssertIsNotNullOrEmpty(value1, "param", "message", new Exception());
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
            }

            try
            {
                Validation.Parameter.AssertIsNotNullOrEmpty(value1, "param", "message");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
            }

            value1 = new string[0];
            try
            {
                Validation.Parameter.AssertIsNotNullOrEmpty(value1, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }

            try
            {
                Validation.Parameter.AssertIsNotNullOrEmpty(value1, "param", "message", new Exception());
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }

            try
            {
                Validation.Parameter.AssertIsNotNullOrEmpty(value1, "param", "message");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }
        }

        [TestMethod]
        public void AssertMinRange()
        {
            Validation.Parameter.AssertMinRange((decimal)1, 0, "param");
            Validation.Parameter.AssertMinRange((double)1, 0, "param");
            Validation.Parameter.AssertMinRange((int)1, 0, "param");
            Validation.Parameter.AssertMinRange((long)1, 0, "param");
            Validation.Parameter.AssertMinRange((float)1, 0, "param");

            try
            {
                decimal value = -1;
                Validation.Parameter.AssertMinRange(value, 0, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }

            try
            {
                double value = -1;
                Validation.Parameter.AssertMinRange(value, 0, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }

            try
            {
                int value = -1;
                Validation.Parameter.AssertMinRange(value, 0, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }

            try
            {
                long value = -1;
                Validation.Parameter.AssertMinRange(value, 0, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }

            try
            {
                float value = -1;
                Validation.Parameter.AssertMinRange(value, 0, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }
        }

        [TestMethod]
        public void AssertMaxRange()
        {
            Validation.Parameter.AssertMaxRange((decimal)1, 1, "param");
            Validation.Parameter.AssertMaxRange((double)1, 1, "param");
            Validation.Parameter.AssertMaxRange((int)1, 1, "param");
            Validation.Parameter.AssertMaxRange((long)1, 1, "param");
            Validation.Parameter.AssertMaxRange((float)1, 1, "param");

            try
            {
                decimal value = 1;
                Validation.Parameter.AssertMaxRange(value, 0, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }

            try
            {
                double value = 1;
                Validation.Parameter.AssertMaxRange(value, 0, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }

            try
            {
                int value = 1;
                Validation.Parameter.AssertMaxRange(value, 0, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }

            try
            {
                long value = 1;
                Validation.Parameter.AssertMaxRange(value, 0, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }

            try
            {
                float value = 1;
                Validation.Parameter.AssertMaxRange(value, 0, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }
        }

        [TestMethod]
        public void AssertRange()
        {
            Validation.Parameter.AssertRange((decimal)1, 0, 10, "param");
            Validation.Parameter.AssertRange((double)1, 0, 10, "param");
            Validation.Parameter.AssertRange((int)1, 0, 10, "param");
            Validation.Parameter.AssertRange((long)1, 0, 10, "param");
            Validation.Parameter.AssertRange((float)1, 0, 10, "param");

            try
            {
                decimal value = 1;
                Validation.Parameter.AssertRange(value, 0, 10, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }

            try
            {
                double value = 1;
                Validation.Parameter.AssertRange(value, 0, 10, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }

            try
            {
                int value = 1;
                Validation.Parameter.AssertRange(value, 0, 10, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }

            try
            {
                long value = 1;
                Validation.Parameter.AssertRange(value, 0, 10, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }

            try
            {
                float value = 1;
                Validation.Parameter.AssertRange(value, 0, 10, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }
        }
    }
}
