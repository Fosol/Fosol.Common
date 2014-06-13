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

        [TestMethod]
        public void UrBuilder_Constructor()
        {
            foreach (var test in _TestData.Uris)
            {
                try
                {
                    var builder = new Fosol.Common.Web.UriBuilder(test.Value);

                    Assert.IsFalse(test.ShouldFail, String.Format("This test should have failed. {0}", test.Value));
                    Assert.AreEqual(test.ExpectedResult, builder.ToString(),
                        String.Format("Original value: '{0}'", test.Value));
                }
                catch (UriFormatException)
                {
                    Assert.IsTrue(test.ShouldFail, String.Format("This test should have passed. {0}", test.Value));
                }
            }
        }

        [TestMethod]
        public void UriBuilder_Scheme()
        {
            foreach (var test in _TestData.Schemes)
            {
                try
                {
                    var builder = new Fosol.Common.Web.UriBuilder();
                    builder.Scheme = test.Value;

                    Assert.IsFalse(test.ShouldFail, String.Format("This test should have failed. {0}", test.Value));
                    Assert.AreEqual(test.ExpectedResult, builder.Scheme,
                        String.Format("Original value: '{0}'", test.Value));
                }
                catch (UriFormatException)
                {
                    Assert.IsTrue(test.ShouldFail, String.Format("This test should have passed. {0}", test.Value));
                }
            }
        }

        [TestMethod]
        public void UriBuilder_Username()
        {
            foreach (var test in _TestData.UserInfos)
            {
                try
                {
                    var builder = new Fosol.Common.Web.UriBuilder();
                    builder.Username = test.Value;

                    Assert.IsFalse(test.ShouldFail, String.Format("This test should have failed. {0}", test.Value));
                    Assert.AreEqual(test.ExpectedResult, builder.Username,
                        String.Format("Original value: '{0}'", test.Value));
                }
                catch (UriFormatException)
                {
                    Assert.IsTrue(test.ShouldFail, String.Format("This test should have passed. {0}", test.Value));
                }
            }
        }

        [TestMethod]
        public void UriBuilder_Authority()
        {
            foreach (var test in _TestData.Authorities)
            {
                try
                {
                    var builder = new Fosol.Common.Web.UriBuilder();
                    builder.Authority = test.Value;

                    Assert.IsFalse(test.ShouldFail, String.Format("This test should have failed. {0}", test.Value));
                    Assert.AreEqual(test.ExpectedResult, builder.Authority,
                        String.Format("Original value: '{0}'", test.Value));
                }
                catch (UriFormatException)
                {
                    Assert.IsTrue(test.ShouldFail, String.Format("This test should have passed. {0}", test.Value));
                }
            }
        }

        [TestMethod]
        public void UriBuilder_Host()
        {
            foreach (var test in _TestData.Hosts)
            {
                try
                {
                    var builder = new Fosol.Common.Web.UriBuilder();
                    builder.Host = test.Value;

                    Assert.IsFalse(test.ShouldFail, String.Format("This test should have failed. {0}", test.Value));
                    Assert.AreEqual(test.ExpectedResult, builder.Host,
                        String.Format("Original value: '{0}'", test.Value));
                }
                catch (UriFormatException)
                {
                    Assert.IsTrue(test.ShouldFail, String.Format("This test should have passed. {0}", test.Value));
                }
            }
        }

        [TestMethod]
        public void UriBuilder_Path()
        {
            foreach (var test in _TestData.Paths)
            {
                try
                {
                    var builder = new Fosol.Common.Web.UriBuilder();
                    builder.Path = test.Value;

                    Assert.IsFalse(test.ShouldFail, String.Format("This test should have failed. {0}", test.Value));
                    Assert.AreEqual(test.ExpectedResult, builder.Path,
                        String.Format("Original value: '{0}'", test.Value));
                }
                catch (UriFormatException)
                {
                    Assert.IsTrue(test.ShouldFail, String.Format("This test should have passed. {0}", test.Value));
                }
            }
        }

        [TestMethod]
        public void UriBuilder_PathSegments()
        {
            foreach (var test in _TestData.Paths)
            {
                try
                {
                    var builder = new Fosol.Common.Web.UriBuilder();
                    builder.Path = test.Value;

                    Assert.IsFalse(test.ShouldFail, String.Format("This test should have failed. {0}", test.Value));
                    Assert.AreEqual(test.NumberOfSegments, builder.GetPath().Count,
                        String.Format("Original value: '{0}'", test.Value));
                }
                catch (UriFormatException)
                {
                    Assert.IsTrue(test.ShouldFail, String.Format("This test should have passed. {0}", test.Value));
                }
            }
        }

        [TestMethod]
        public void UriBuilder_Query()
        {
            foreach (var test in _TestData.Queries)
            {
                try
                {
                    var builder = new Fosol.Common.Web.UriBuilder();
                    builder.Query = test.Value;

                    Assert.IsFalse(test.ShouldFail, String.Format("This test should have failed. {0}", test.Value));
                    Assert.AreEqual(test.ExpectedResult, builder.Query,
                        String.Format("Original value: '{0}'", test.Value));
                }
                catch (UriFormatException)
                {
                    Assert.IsTrue(test.ShouldFail, String.Format("This test should have passed. {0}", test.Value));
                }
            }
        }

        [TestMethod]
        public void UriBuilder_Fragment()
        {
            foreach (var test in _TestData.Fragments)
            {
                try
                {
                    var builder = new Fosol.Common.Web.UriBuilder();
                    builder.Fragment = test.Value;

                    Assert.IsFalse(test.ShouldFail, String.Format("This test should have failed. {0}", test.Value));
                    Assert.AreEqual(test.ExpectedResult, builder.Fragment,
                        String.Format("Original value: '{0}'", test.Value));
                }
                catch (UriFormatException)
                {
                    Assert.IsTrue(test.ShouldFail, String.Format("This test should have passed. {0}", test.Value));
                }
            }
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
