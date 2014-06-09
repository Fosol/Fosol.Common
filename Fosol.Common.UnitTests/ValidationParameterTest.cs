using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fosol.Common.UnitTests
{
    [TestClass]
    public class ValidationParameterTest
    {
        #region AssertIsNull
        [TestMethod]
        public void AssertIsNotNullException()
        {
            try
            {
                Validation.Assert.IsNotNull(null, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void AssertIsNotNullException1()
        {
            try
            {
                Validation.Assert.IsNotNull(null, "message", new Exception());
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void AssertIsNotNullException2()
        {
            try
            {
                Validation.Assert.IsNotNull(null, "param", "message");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
            }
        }
        #endregion

        #region AssertIsNotNullOrEmpty
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
        public void AssertIsNotNullOrEmpty1()
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
        }

        [TestMethod]
        public void AssertIsNotNullOrEmpty2()
        {
            string value = null;
            try
            {
                Validation.Assert.IsNotNullOrEmpty(value, "param", "message", new Exception());
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void AssertIsNotNullOrEmpty3()
        {
            string value = null;
            try
            {
                Validation.Assert.IsNotNullOrEmpty(value, "param", "message");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void AssertIsNotNullOrEmpty4()
        {
            string value = "";
            try
            {
                Validation.Assert.IsNotNullOrEmpty(value, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }
        }

        [TestMethod]
        public void AssertIsNotNullOrEmpty5()
        {
            string value = "";
            try
            {
                Validation.Assert.IsNotNullOrEmpty(value, "param", "message", new Exception());
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }
        }

        [TestMethod]
        public void AssertIsNotNullOrEmpty6()
        {
            string value = "";
            try
            {
                Validation.Assert.IsNotNullOrEmpty(value, "param", "message");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }
        }

        [TestMethod]
        public void AssertIsNotNullOrEmpty7()
        {
            string[] value = null;
            try
            {
                Validation.Assert.IsNotNullOrEmpty(value, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void AssertIsNotNullOrEmpty8()
        {
            string[] value = null;
            try
            {
                Validation.Assert.IsNotNullOrEmpty(value, "param", "message", new Exception());
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void AssertIsNotNullOrEmpty9()
        {
            var value1 = new string[0];
            try
            {
                Validation.Assert.IsNotNullOrEmpty(value1, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }
        }

        [TestMethod]
        public void AssertIsNotNullOrEmpty10()
        {
            var value = new string[0];
            try
            {
                Validation.Assert.IsNotNullOrEmpty(value, "param", "message", new Exception());
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }
        }

        [TestMethod]
        public void AssertIsNotNullOrEmpty11()
        {
            var value = new string[0];
            try
            {
                Validation.Assert.IsNotNullOrEmpty(value, "param", "message");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }
        }
        #endregion

        #region AssertMinRange
        [TestMethod]
        public void AssertMinRangeDecimal()
        {
            try
            {
                Validation.Assert.MinRange((decimal)1, 0, "param");
            }
            catch
            {
                Assert.Fail("This test should have passed.");
            }
        }

        [TestMethod]
        public void AssertMinRangeDecimalException()
        {
            try
            {
                decimal value = -1;
                Validation.Assert.MinRange(value, 0, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }
        }

        [TestMethod]
        public void AssertMinRangeDouble()
        {
            try
            {
                Validation.Assert.MinRange((double)1, 0, "param");
            }
            catch
            {
                Assert.Fail("This test should have passed.");
            }
        }

        [TestMethod]
        public void AssertMinRangeDoubleException()
        {
            try
            {
                double value = -1;
                Validation.Assert.MinRange(value, 0, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }
        }

        [TestMethod]
        public void AssertMinRangeInt()
        {
            try
            {
                Validation.Assert.MinRange((int)1, 0, "param");
            }
            catch
            {
                Assert.Fail("This test should have passed.");
            }
        }

        [TestMethod]
        public void AssertMinRangeIntException()
        {
            try
            {
                int value = -1;
                Validation.Assert.MinRange(value, 0, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }
        }

        [TestMethod]
        public void AssertMinRangeLong()
        {
            try
            {
                Validation.Assert.MinRange((long)1, 0, "param");
            }
            catch
            {
                Assert.Fail("This test should have passed.");
            }
        }

        [TestMethod]
        public void AssertMinRangeLongException()
        {
            try
            {
                long value = -1;
                Validation.Assert.MinRange(value, 0, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }
        }

        [TestMethod]
        public void AssertMinRangeFloat()
        {
            try
            {
                Validation.Assert.MinRange((float)1, 0, "param");
            }
            catch
            {
                Assert.Fail("This test should have passed.");
            }
        }

        [TestMethod]
        public void AssertMinRangeFloatException()
        {
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
        #endregion

        #region AssertMaxRange
        [TestMethod]
        public void AssertMaxRangeDecimal()
        {
            try
            {
                Validation.Assert.MaxRange((decimal)1, 1, "param");
            }
            catch
            {
                Assert.Fail("This test should have passed.");
            }
        }

        [TestMethod]
        public void AssertMaxRangeDecimalException()
        {
            try
            {
                decimal value = 1;
                Validation.Assert.MaxRange(value, 0, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }
        }

        [TestMethod]
        public void AssertMaxRangeDouble()
        {
            try
            {
                Validation.Assert.MaxRange((double)1, 1, "param");
            }
            catch
            {
                Assert.Fail("This test should have passed.");
            }
        }

        [TestMethod]
        public void AssertMaxRangeDoubleException()
        {
            try
            {
                double value = 1;
                Validation.Assert.MaxRange(value, 0, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }
        }

        [TestMethod]
        public void AssertMaxRangeInt()
        {
            try
            {
                Validation.Assert.MaxRange((int)1, 1, "param");
            }
            catch
            {
                Assert.Fail("This test should have passed.");
            }
        }

        [TestMethod]
        public void AssertMaxRangeIntException()
        {
            try
            {
                int value = 1;
                Validation.Assert.MaxRange(value, 0, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }
        }

        [TestMethod]
        public void AssertMaxRangeLong()
        {
            try
            {
                Validation.Assert.MaxRange((long)1, 1, "param");
            }
            catch
            {
                Assert.Fail("This test should have passed.");
            }
        }

        [TestMethod]
        public void AssertMaxRangeLongException()
        {
            try
            {
                long value = 1;
                Validation.Assert.MaxRange(value, 0, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }
        }

        [TestMethod]
        public void AssertMaxRangeFloat()
        {
            try
            {
                Validation.Assert.MaxRange((float)1, 1, "param");
            }
            catch
            {
                Assert.Fail("This test should have passed.");
            }
        }

        [TestMethod]
        public void AssertMaxRangeFloatException()
        {
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
        #endregion

        #region AssertRange
        [TestMethod]
        public void AssertRangeDecimal()
        {
            try
            {
                Validation.Assert.Range((decimal)1, 0, 10, "param");
            }
            catch
            {
                Assert.Fail("This test should have passed.");
            }
        }

        [TestMethod]
        public void AssertRangeDecimalException()
        {
            try
            {
                decimal value = -1;
                Validation.Assert.Range(value, 0, 10, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }
        }

        [TestMethod]
        public void AssertRangeDouble()
        {
            try
            {
                Validation.Assert.Range((double)1, 0, 10, "param");
            }
            catch
            {
                Assert.Fail("This test should have passed.");
            }
        }

        [TestMethod]
        public void AssertRangeDoubleException()
        {
            try
            {
                double value = -1;
                Validation.Assert.Range(value, 0, 10, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }
        }

        [TestMethod]
        public void AssertRangeInt()
        {
            try
            {
                Validation.Assert.Range((int)1, 0, 10, "param");
            }
            catch
            {
                Assert.Fail("This test should have passed.");
            }
        }

        [TestMethod]
        public void AssertRangeIntException()
        {
            try
            {
                int value = -1;
                Validation.Assert.Range(value, 0, 10, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }
        }

        [TestMethod]
        public void AssertRangeLong()
        {
            try
            {
                Validation.Assert.Range((long)1, 0, 10, "param");
            }
            catch
            {
                Assert.Fail("This test should have passed.");
            }
        }

        [TestMethod]
        public void AssertRangeLongException()
        {
            try
            {
                long value = -1;
                Validation.Assert.Range(value, 0, 10, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }
        }

        [TestMethod]
        public void AssertRangeFloat()
        {
            try
            {
                Validation.Assert.Range((float)1, 0, 10, "param");
            }
            catch
            {
                Assert.Fail("This test should have passed.");
            }
        }

        [TestMethod]
        public void AssertRangeFloatException()
        {
            try
            {
                float value = -11;
                Validation.Assert.Range(value, 0, 10, "param");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }
        }
        #endregion
    }
}
