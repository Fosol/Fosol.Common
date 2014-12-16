using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fosol.Common.UnitTests.Web
{
    [TestClass]
    public class UriBuilderTest
    {
        #region Variables
        static Data.UriTestData _TestData;
        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Methods
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _TestData = new Data.UriTestData();
        }

        #region Pass
        [TestMethod]
        public void Web_UrBuilder_Constructor()
        {
            foreach (Data.UriExample test in _TestData.Pass.Where(d => d is Data.UriExample))
            {
                try
                {
                    var builder = new Fosol.Common.Net.UriBuilder(test.Value);

                    Assert.AreEqual(test.ExpectedValue, builder.ToString(), "Original value: '{0}'", test.Value);
                }
                catch
                {
                    Assert.Fail("This test '{0}' should have passed.", test.Value);
                }
            }
        }

        [TestMethod]
        public void Web_UriBuilder_Scheme()
        {
            foreach (Data.UriSchemeExample test in _TestData.Pass.Where(d => d is Data.UriSchemeExample))
            {
                try
                {
                    var builder = new Fosol.Common.Net.UriBuilder();
                    builder.Scheme = test.Value;

                    Assert.AreEqual(test.ExpectedValue, builder.Scheme, "Original value: '{0}'", test.Value);
                }
                catch
                {
                    Assert.Fail("This test '{0}' should have passed.", test.Value);
                }
            }
        }

        [TestMethod]
        public void Web_UriBuilder_Username()
        {
            foreach (Data.UriUserInfoExample test in _TestData.Pass.Where(d => d is Data.UriUserInfoExample))
            {
                try
                {
                    var builder = new Fosol.Common.Net.UriBuilder();
                    builder.Username = test.Value;

                    Assert.AreEqual(test.ExpectedValue, builder.Username, "Original value: '{0}'", test.Value);
                }
                catch
                {
                    Assert.Fail("This test '{0}' should have passed.", test.Value);
                }
            }
        }

        [TestMethod]
        public void Web_UriBuilder_Authority()
        {
            foreach (Data.UriAuthorityExample test in _TestData.Pass.Where(d => d is Data.UriAuthorityExample))
            {
                try
                {
                    var builder = new Fosol.Common.Net.UriBuilder();
                    builder.Authority = test.Value;

                    Assert.AreEqual(test.ExpectedValue, builder.Authority, "Original value: '{0}'", test.Value);
                }
                catch
                {
                    Assert.Fail("This test '{0}' should have passed.", test.Value);
                }
            }
        }

        [TestMethod]
        public void Web_UriBuilder_Host()
        {
            foreach (Data.UriHostExample test in _TestData.Pass.Where(d => d is Data.UriHostExample))
            {
                try
                {
                    var builder = new Fosol.Common.Net.UriBuilder();
                    builder.Host = test.Value;

                    Assert.AreEqual(test.ExpectedValue, builder.Host, "Original value: '{0}'", test.Value);
                }
                catch
                {
                    Assert.Fail("This test '{0}' should have passed.", test.Value);
                }
            }
        }

        [TestMethod]
        public void Web_UriBuilder_Path()
        {
            foreach (Data.UriPathExample test in _TestData.Pass.Where(d => d is Data.UriPathExample))
            {
                try
                {
                    var builder = new Fosol.Common.Net.UriBuilder();
                    builder.Path = test.Value;

                    Assert.AreEqual(test.ExpectedValue, builder.Path, "Original value: '{0}'", test.Value);
                }
                catch
                {
                    Assert.Fail("This test '{0}' should have passed.", test.Value);
                }
            }
        }

        [TestMethod]
        public void Web_UriBuilder_PathSegments()
        {
            foreach (Data.UriPathExample test in _TestData.Pass.Where(d => d is Data.UriPathExample))
            {
                try
                {
                    var builder = new Fosol.Common.Net.UriBuilder();
                    builder.Path = test.Value;

                    Assert.AreEqual(test.NumberOfSegments, builder.GetPath().Count, "Original value: '{0}'", test.Value);
                }
                catch
                {
                    Assert.Fail("This test '{0}' should have passed.", test.Value);
                }
            }
        }

        [TestMethod]
        public void Web_UriBuilder_Query()
        {
            foreach (Data.UriQueryExample test in _TestData.Pass.Where(d => d is Data.UriQueryExample))
            {
                try
                {
                    var builder = new Fosol.Common.Net.UriBuilder();
                    builder.Query = test.Value;

                    Assert.AreEqual(test.ExpectedValue, builder.Query, "Original value: '{0}'", test.Value);
                }
                catch
                {
                    Assert.Fail("This test '{0}' should have passed.", test.Value);
                }
            }
        }

        [TestMethod]
        public void Web_UriBuilder_Fragment()
        {
            foreach (Data.UriFragmentExample test in _TestData.Pass.Where(d => d is Data.UriFragmentExample))
            {
                try
                {
                    var builder = new Fosol.Common.Net.UriBuilder();
                    builder.Fragment = test.Value;

                    Assert.AreEqual(test.ExpectedValue, builder.Fragment, "Original value: '{0}'", test.Value);
                }
                catch
                {
                    Assert.Fail("This test '{0}' should have passed.", test.Value);
                }
            }
        }
        #endregion

        #region Fail
        [TestMethod]
        public void Web_UrBuilder_Constructor_Fail()
        {
            foreach (Data.UriExample test in _TestData.Fail.Where(d => d is Data.UriExample))
            {
                try
                {
                    var builder = new Fosol.Common.Net.UriBuilder(test.Value);

                    Assert.Fail("This test '{0}' should have thrown an exception.", test.Value);
                }
                catch (Exception ex)
                {
                    Assert.IsInstanceOfType(ex, typeof(UriFormatException), "Test value: '{0}'", test.Value);
                }
            }
        }

        [TestMethod]
        public void Web_UriBuilder_Scheme_Fail()
        {
            foreach (Data.UriSchemeExample test in _TestData.Fail.Where(d => d is Data.UriSchemeExample))
            {
                try
                {
                    var builder = new Fosol.Common.Net.UriBuilder();
                    builder.Scheme = test.Value;

                    Assert.Fail("This test '{0}' should have thrown an exception.", test.Value);
                }
                catch (Exception ex)
                {
                    Assert.IsInstanceOfType(ex, typeof(UriFormatException), "Test value: '{0}'", test.Value);
                }
            }
        }

        [TestMethod]
        public void Web_UriBuilder_Username_Fail()
        {
            foreach (Data.UriUserInfoExample test in _TestData.Fail.Where(d => d is Data.UriUserInfoExample))
            {
                try
                {
                    var builder = new Fosol.Common.Net.UriBuilder();
                    builder.Username = test.Value;

                    Assert.Fail("This test '{0}' should have thrown an exception.", test.Value);
                }
                catch (Exception ex)
                {
                    Assert.IsInstanceOfType(ex, typeof(UriFormatException), "Test value: '{0}'", test.Value);
                }
            }
        }

        [TestMethod]
        public void Web_UriBuilder_Authority_Fail()
        {
            foreach (Data.UriAuthorityExample test in _TestData.Fail.Where(d => d is Data.UriAuthorityExample))
            {
                try
                {
                    var builder = new Fosol.Common.Net.UriBuilder();
                    builder.Authority = test.Value;

                    Assert.Fail("This test '{0}' should have thrown an exception.", test.Value);
                }
                catch (Exception ex)
                {
                    Assert.IsInstanceOfType(ex, typeof(UriFormatException), "Test value: '{0}'", test.Value);
                }
            }
        }

        [TestMethod]
        public void Web_UriBuilder_Host_Fail()
        {
            foreach (Data.UriHostExample test in _TestData.Fail.Where(d => d is Data.UriHostExample))
            {
                try
                {
                    var builder = new Fosol.Common.Net.UriBuilder();
                    builder.Host = test.Value;

                    Assert.Fail("This test '{0}' should have thrown an exception.", test.Value);
                }
                catch (Exception ex)
                {
                    Assert.IsInstanceOfType(ex, typeof(UriFormatException), "Test value: '{0}'", test.Value);
                }
            }
        }

        [TestMethod]
        public void Web_UriBuilder_Path_Fail()
        {
            foreach (Data.UriPathExample test in _TestData.Fail.Where(d => d is Data.UriPathExample))
            {
                try
                {
                    var builder = new Fosol.Common.Net.UriBuilder();
                    builder.Path = test.Value;

                    Assert.Fail("This test '{0}' should have thrown an exception.", test.Value);
                }
                catch (Exception ex)
                {
                    Assert.IsInstanceOfType(ex, typeof(UriFormatException), "Test value: '{0}'", test.Value);
                }
            }
        }

        [TestMethod]
        public void Web_UriBuilder_PathSegments_Fail()
        {
            foreach (Data.UriPathExample test in _TestData.Fail.Where(d => d is Data.UriPathExample))
            {
                try
                {
                    var builder = new Fosol.Common.Net.UriBuilder();
                    builder.Path = test.Value;

                    Assert.Fail("This test '{0}' should have thrown an exception.", test.Value);
                }
                catch (Exception ex)
                {
                    Assert.IsInstanceOfType(ex, typeof(UriFormatException), "Test value: '{0}'", test.Value);
                }
            }
        }

        [TestMethod]
        public void Web_UriBuilder_Query_Fail()
        {
            foreach (Data.UriQueryExample test in _TestData.Fail.Where(d => d is Data.UriQueryExample))
            {
                try
                {
                    var builder = new Fosol.Common.Net.UriBuilder();
                    builder.Query = test.Value;

                    Assert.Fail("This test '{0}' should have thrown an exception.", test.Value);
                }
                catch (Exception ex)
                {
                    Assert.IsInstanceOfType(ex, typeof(UriFormatException), "Test value: '{0}'", test.Value);
                }
            }
        }

        [TestMethod]
        public void Web_UriBuilder_Fragment_Fail()
        {
            foreach (Data.UriFragmentExample test in _TestData.Fail.Where(d => d is Data.UriFragmentExample))
            {
                try
                {
                    var builder = new Fosol.Common.Net.UriBuilder();
                    builder.Fragment = test.Value;

                    Assert.Fail("This test '{0}' should have thrown an exception.", test.Value);
                }
                catch (Exception ex)
                {
                    Assert.IsInstanceOfType(ex, typeof(UriFormatException), "Test value: '{0}'", test.Value);
                }
            }
        }
        #endregion
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
