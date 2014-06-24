using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fosol.Common.UnitTests.Web
{
    [TestClass]
    class QueryParametersTest
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
        public void Web_QueryParameters_ParseQueryStringToKeyValuePair_ParameterCount()
        {
            foreach (Data.UriQueryExample test in _TestData.Pass.Where(d => d is Data.UriQueryExample))
            {
                try
                {
                    var result = Fosol.Common.Net.UriQuery.ParseQueryStringToKeyValuePair(test.Value);

                    // Count the number of individual values for each query parameter.
                    // This will give you the distinct number of key value pairs.
                    var param_count = 0;
                    foreach (var p in test.QueryParameters)
                    {
                        param_count += p.Value.Length;
                    }

                    Assert.IsTrue(result.Count == param_count, "Original value: '{0}'", test.Value);
                }
                catch
                {
                    Assert.Fail("This test '{0}' should have passed.", test.Value);
                }
            }
        }

        [TestMethod]
        public void Web_QueryParameters_ParseQueryStringToKeyValuePair_ParameterValueCount()
        {
            foreach (Data.UriQueryExample test in _TestData.Pass.Where(d => d is Data.UriQueryExample))
            {
                try
                {
                    var result = Fosol.Common.Net.UriQuery.ParseQueryStringToKeyValuePair(test.Value);

                    foreach (var p in test.QueryParameters)
                    {
                        // Fetch all the query parameters with the specified key and count them.
                        var value_count = result.Where(r => r.Key.Equals(p.Key)).Count();

                        Assert.AreEqual(p.Value.Length, value_count, "Original value: '{0}'", test.Value);
                    }
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
        public void Web_QueryParameters_ParseQueryStringToKeyValuePair_ParameterCount_Fail()
        {
            foreach (Data.UriQueryExample test in _TestData.Fail.Where(d => d is Data.UriQueryExample))
            {
                try
                {
                    var result = Fosol.Common.Net.UriQuery.ParseQueryStringToKeyValuePair(test.Value);

                    Assert.Fail("This test '{0}' should have thrown an exception.", test.Value);
                }
                catch (Exception ex)
                {
                    Assert.IsInstanceOfType(ex, typeof(UriFormatException), "Test value: '{0}'", test.Value);
                }
            }
        }

        [TestMethod]
        public void Web_QueryParameters_ParseQueryStringToKeyValuePair_ParameterValueCount_Fail()
        {
            foreach (Data.UriQueryExample test in _TestData.Fail.Where(d => d is Data.UriQueryExample))
            {
                try
                {
                    var result = Fosol.Common.Net.UriQuery.ParseQueryStringToKeyValuePair(test.Value);

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
