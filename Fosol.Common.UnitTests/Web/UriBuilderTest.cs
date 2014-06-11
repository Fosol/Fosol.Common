using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fosol.Common.UnitTests.Web
{
    [TestClass]
    public class UriBuilderTest
    {
        #region Variables
        private static readonly UriTest[] _TestValues = new UriTest[]
            {
                new UriTest("http://www.fosol.ca", 1, new [] { new QueryParamTest("", 1) }),
                new UriTest("http://www.fosol.ca/index.html", 1, new [] { new QueryParamTest("", 1) }),
                new UriTest("http://www.fosol.ca/index.html?key1=value1", 1, new [] { new QueryParamTest("key1", 1) }),
                new UriTest("http://www.fosol.ca/index.html?key1=value1&key2=value2", 2, new [] { new QueryParamTest("key1", 1), new QueryParamTest("key2", 1) }),
                new UriTest("http://www.fosol.ca/index.html?key1=value1&key1=value2", 2, new [] { new QueryParamTest("key1", 2) }),
                new UriTest("http://www.fosol.ca/index.html?value1", 1, new [] { new QueryParamTest("", 1) }),
                new UriTest("http://www.fosol.ca/index.html?key1=", 1, new [] { new QueryParamTest("key1", 1) }),
                new UriTest("http://www.fosol.ca/index.html?=value1", 1, new [] { new QueryParamTest("", 1) }),
                new UriTest("http://www.fosol.ca/index.html?=", 1, new [] { new QueryParamTest("", 1) }),
                new UriTest("http://www.fosol.ca/index.html?key1=value1&amp;&key2=value2", 3, new [] { new QueryParamTest("key1", 1), new QueryParamTest("", 1), new QueryParamTest("key2", 1) }),
                new UriTest("?key1=value1%26&key2=value2", 2, new [] { new QueryParamTest("key1", 1), new QueryParamTest("key2", 1) }),
                new UriTest("key1=value1%26&key2=value2", 2, new [] { new QueryParamTest("key1", 1), new QueryParamTest("key2", 1) }),
                new UriTest("key1=value1%26&key2=value2", 2, new [] { new QueryParamTest("key1", 1), new QueryParamTest("key2", 1) })
            };
        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Methods
        [TestMethod]
        public void ParseQueryStringToKeyValuePair_ParameterCount()
        {
            foreach (var value in _TestValues)
            {
                var result = Fosol.Common.Web.UriBuilder.ParseQueryStringToKeyValuePair(value.Uri);

                Assert.IsTrue(result.Count == value.KeyValuePairs, String.Format("UriParser.ParseQueryStringToKeyValuePair returned {0} parameters when it should have returned {1} parameters.", result.Count, value.KeyValuePairs));
            }
        }

        [TestMethod]
        public void ParseQueryStringToKeyValuePair_ParameterValues()
        {
            foreach (var test in _TestValues)
            {
                var result = Fosol.Common.Web.UriBuilder.ParseQueryStringToKeyValuePair(test.Uri);

                for (var i = 0; i < test.Parameters.Length; i++)
                {
                    var parameter = result.Where(r => r.Key.Equals(test.Parameters[i].Key)).Count();

                    Assert.AreEqual(test.Parameters[i].NumberOfValues, parameter, String.Format("UriParser.ParseQueryStringToKeyValuePair parameter '{0}' should have {1} values.", test.Parameters[i].Key, test.Parameters[i].NumberOfValues));
                }
            }
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion

        #region Other
        struct UriTest
        {
            #region Variables
            #endregion

            #region Properties
            public string Uri;
            public int KeyValuePairs;
            public QueryParamTest[] Parameters;
            #endregion

            #region Constructors
            public UriTest(string uri, int keyValuePairs, QueryParamTest[] parameters)
            {
                this.Uri = uri;
                this.KeyValuePairs = keyValuePairs;
                this.Parameters = parameters ?? new QueryParamTest[0];
            }
            #endregion

            #region Methods

            #endregion

            #region Operators
            #endregion

            #region Events
            #endregion
        }

        struct QueryParamTest
        {
            #region Variables
            #endregion

            #region Properties
            public string Key;
            public int NumberOfValues;
            #endregion

            #region Constructors
            public QueryParamTest(string key, int numberOfValues)
            {
                this.Key = key;
                this.NumberOfValues = numberOfValues;
            }
            #endregion

            #region Methods

            #endregion

            #region Operators
            #endregion

            #region Events
            #endregion
        }
        #endregion
    }
}
