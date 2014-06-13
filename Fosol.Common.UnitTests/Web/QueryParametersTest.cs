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

        [TestMethod]
        public void QueryParameters_ParseQueryStringToKeyValuePair_ParameterCount()
        {
            foreach (var test in _TestData.Queries)
            {
                try
                {
                    var result = Fosol.Common.Web.QueryParameters.ParseQueryStringToKeyValuePair(test.Value);

                    // Count the number of individual values for each query parameter.
                    // This will give you the distinct number of key value pairs.
                    var param_count = 0;
                    foreach (var p in test.QueryParameters)
                    {
                        param_count += p.Value.Length;
                    }

                    Assert.IsFalse(test.ShouldFail, String.Format("This test should have failed. {0}", test.Value));
                    Assert.IsTrue(result.Count == param_count,
                        String.Format("Original value: '{0}'", test.Value));
                }
                catch (UriFormatException)
                {
                    Assert.IsTrue(test.ShouldFail, String.Format("This test should have passed. {0}", test.Value));
                }
            }
        }

        [TestMethod]
        public void QueryParameters_ParseQueryStringToKeyValuePair_ParameterValueCount()
        {
            foreach (var test in _TestData.Queries)
            {
                try
                {
                    var result = Fosol.Common.Web.QueryParameters.ParseQueryStringToKeyValuePair(test.Value);

                    Assert.IsFalse(test.ShouldFail, String.Format("This test should have failed. {0}", test.Value));
                    foreach (var p in test.QueryParameters)
                    {
                        // Fetch all the query parameters with the specified key and count them.
                        var value_count = result.Where(r => r.Key.Equals(p.Key)).Count();

                        Assert.AreEqual(p.Value.Length, value_count,
                            String.Format("Original value: '{0}'", test.Value));
                    }
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
