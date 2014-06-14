using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.UnitTests.Web.Data
{
    class UriTestData
        : Fosol.Common.UnitTests.TestDataCollection
    {
        #region Variables
        public const string ReservedGeneralDelims = ":/?#[]@";
        public const string ReservedSubsetDelims = "!$&'()*+,;=";
        public const string Unreserved = Fosol.Common.Text.GlobalConstant.Alphanumeric + "-._~";
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public UriTestData()
        {
            InitializePass();
            InitializeFail();
        }
        #endregion

        #region Methods
        private void InitializePass()
        {
            this.Pass.AddRange(new UriSchemeExample[]
            {
                new UriSchemeExample(Fosol.Common.Text.GlobalConstant.Letter + Fosol.Common.Text.GlobalConstant.Alphanumeric + "+-."),
                new UriSchemeExample("http"),
                new UriSchemeExample("ftp"),
                new UriSchemeExample("https"),
                new UriSchemeExample("unc"),
                new UriSchemeExample("abcdefghijklmnopqrstuvwxyz"),
                new UriSchemeExample("ABCDEFGHIJKLMNOPQRSTUVWXYZ"),
                new UriSchemeExample("a0123456789"),
                new UriSchemeExample("a+-.")
            });

            this.Pass.AddRange(new UriUserInfoExample[]
            {
                new UriUserInfoExample("user"),
                new UriUserInfoExample("user:pwd", "user"),
                new UriUserInfoExample("user:pwd:stuff", "user"),
                new UriUserInfoExample(Unreserved + "%20" + ReservedSubsetDelims + ":", Unreserved + "%20" + ReservedSubsetDelims)
            });

            this.Pass.AddRange(new UriAuthorityExample[]
            {
                new UriAuthorityExample("www.fosol.ca"),
                new UriAuthorityExample("www.fosol.ca:12345"),
                new UriAuthorityExample("user:pwd@www.fosol.ca", "www.fosol.ca"),
                new UriAuthorityExample("user:pwd@www.fosol.ca:12345", "www.fosol.ca:12345"),
                new UriAuthorityExample("user@www.fosol.ca", "www.fosol.ca"),
                new UriAuthorityExample("user@www.fosol.ca:12345", "www.fosol.ca:12345"),
                new UriAuthorityExample("123.123.123.123"),
                new UriAuthorityExample("123.123.123.123:12345"),
                new UriAuthorityExample("1.1.1.1"),
                new UriAuthorityExample("1.1.1.1:1234"),
                new UriAuthorityExample("~")
            });

            this.Pass.AddRange(new UriHostExample[]
            {
                new UriHostExample("www.fosol.ca"),
                new UriHostExample(Unreserved),
                new UriHostExample("%40"),
                new UriHostExample(ReservedSubsetDelims),
                new UriHostExample("test")
            });

            this.Pass.AddRange(new UriPathExample[]
            {
                new UriPathExample("/path1/path2") { NumberOfSegments = 2 },
                new UriPathExample("path1/path2") { NumberOfSegments = 2 },
                new UriPathExample("path1/path2/path3/") { NumberOfSegments = 4 },
                new UriPathExample("/") { NumberOfSegments = 0 },
                new UriPathExample(null, "") { NumberOfSegments = 0 },
                new UriPathExample("") { NumberOfSegments = 0 },
                new UriPathExample(" ", "%20") { NumberOfSegments = 1 },
                new UriPathExample("  ", "%20%20") { NumberOfSegments = 1 },
                new UriPathExample("AbC234") { NumberOfSegments = 1 },
                new UriPathExample("234.jk324") { NumberOfSegments = 1 },
                new UriPathExample("_test!") { NumberOfSegments = 1 },
                new UriPathExample("._~!$&'()*+,;=:@") { NumberOfSegments = 1 },
                new UriPathExample("0123456789") { NumberOfSegments = 1 },
                new UriPathExample("abcdefghijklmnopqrstuvwxyz") { NumberOfSegments = 1 },
                new UriPathExample("ABCDEFGHIJKLMNOPQRSTUVWXYZ") { NumberOfSegments = 1 }
            });

            this.Pass.AddRange(new UriQueryExample[]
            {
                new UriQueryExample("key1=http://www.fosol.ca/index.html%3Fkey1=val1") { QueryParameters = new [] { new KeyValuePair<string, string[]>("key1", new [] { "http://www.fosol.ca/index.html%3Fkey1=val1" } ) } },
                new UriQueryExample("key1=http://www.fosol.ca/index.html?key1=val1", "key1=val1") { QueryParameters = new [] { new KeyValuePair<string, string[]>("key1", new [] { "val1" } ) } },
                new UriQueryExample("key1&key1&key1") { QueryParameters = new [] { new KeyValuePair<string, string[]>("key1", new [] { "", "", "" } ) } },
                new UriQueryExample("?key1=val1", "key1=val1") { QueryParameters = new [] { new KeyValuePair<string, string[]>("key1", new [] { "val1" } ) } },
                new UriQueryExample("?key1=val1&key2=val2", "key1=val1&key2=val2") { QueryParameters = new [] { new KeyValuePair<string, string[]>("key1", new [] { "val1" } ), new KeyValuePair<string, string[]>("key2", new [] { "val2" } ) } },
                new UriQueryExample("key1=val1") { QueryParameters = new [] { new KeyValuePair<string, string[]>("key1", new [] { "val1" } ) } },
                new UriQueryExample("=val1") { QueryParameters = new [] { new KeyValuePair<string, string[]>("", new [] { "val1" } ) } },
                new UriQueryExample("key1=", "key1") { QueryParameters = new [] { new KeyValuePair<string, string[]>("key1", new [] { "" } ) } },
                new UriQueryExample("=") { QueryParameters = new [] { new KeyValuePair<string, string[]>("=", new [] { "" } ) } },
                new UriQueryExample("key1") { QueryParameters = new [] { new KeyValuePair<string, string[]>("key1", new [] { "" } ) } },
                new UriQueryExample("key1=val1") { QueryParameters = new [] { new KeyValuePair<string, string[]>("key1", new [] { "val1" } ) } },
                new UriQueryExample("key1=val1%20") { QueryParameters = new [] { new KeyValuePair<string, string[]>("key1", new [] { "val1%20" } ) } },
                new UriQueryExample("key1%20=val1") { QueryParameters = new [] { new KeyValuePair<string, string[]>("key1%20", new [] { "val1" } ) } },
                new UriQueryExample("key 1=val1", "key%201=val1") { QueryParameters = new [] { new KeyValuePair<string, string[]>("key%201", new [] { "val1" } ) } },
                new UriQueryExample("key1=val 1", "key1=val%201") { QueryParameters = new [] { new KeyValuePair<string, string[]>("key1", new [] { "val%201" } ) } },
                new UriQueryExample("ke%34y1=val 1", "ke%34y1=val%201") { QueryParameters = new [] { new KeyValuePair<string, string[]>("ke%34y1", new [] { "val%201" } ) } },
                new UriQueryExample("key1&key2&key3") { QueryParameters = new [] { new KeyValuePair<string, string[]>("key1", new [] { "" } ), new KeyValuePair<string, string[]>("key2", new [] { "" } ), new KeyValuePair<string, string[]>("key3", new [] { "" } ) } },
                new UriQueryExample("key1=val1&key1=val2&key1=val3") { QueryParameters = new [] { new KeyValuePair<string, string[]>("key1", new [] { "val1", "val2", "val3" } ) } },
                new UriQueryExample("http://www.fosol.ca") { QueryParameters = new [] { new KeyValuePair<string, string[]>("http://www.fosol.ca", new [] { "" }) } },
                new UriQueryExample("http://www.fosol.ca/index.html") { QueryParameters = new [] { new KeyValuePair<string, string[]>("http://www.fosol.ca/index.html", new [] { "" } ) } },
                new UriQueryExample("http://www.fosol.ca/index.html?key1=value1", "key1=value1") { QueryParameters = new [] { new KeyValuePair<string, string[]>("key1", new [] { "value1" } ) }  },
                new UriQueryExample("http://www.fosol.ca/index.html?key1=value1&key2=value2", "key1=value1&key2=value2") { QueryParameters = new [] { new KeyValuePair<string, string[]>("key1", new [] { "value1" } ), new KeyValuePair<string, string[]>("key2", new [] { "value2" }) }  },
                new UriQueryExample("http://www.fosol.ca/index.html?key1=value1&key1=value2", "key1=value1&key1=value2") { QueryParameters = new [] { new KeyValuePair<string, string[]>("key1", new [] { "value1", "value2" } ) }  },
                new UriQueryExample("http://www.fosol.ca/index.html?key1", "key1") { QueryParameters = new [] { new KeyValuePair<string, string[]>("key1", new [] { "" } ) }  },
                new UriQueryExample("http://www.fosol.ca/index.html?key1=", "key1") { QueryParameters = new [] { new KeyValuePair<string, string[]>("key1", new [] { "" } ) }  },
                new UriQueryExample("http://www.fosol.ca/index.html?=value1", "=value1") { QueryParameters = new [] { new KeyValuePair<string, string[]>("", new [] { "value1" } ) }  },
                new UriQueryExample("http://www.fosol.ca/index.html?=", "=") { QueryParameters = new [] { new KeyValuePair<string, string[]>("=", new [] { "" } ) }  },
                new UriQueryExample("http://www.fosol.ca/index.html?key1=value1&amp;&key2=value2", "key1=value1&amp;&key2=value2") { QueryParameters = new [] { new KeyValuePair<string, string[]>("key1", new [] { "value1" } ), new KeyValuePair<string, string[]>("amp;", new [] { "" } ), new KeyValuePair<string, string[]>("key2", new [] { "value2" } ) }  },
                new UriQueryExample("?key1=value1%26&key2=value2", "key1=value1%26&key2=value2") { QueryParameters = new [] { new KeyValuePair<string, string[]>("key1", new [] { "value1%26" } ), new KeyValuePair<string, string[]>("key2", new [] { "value2" } ) }  },
                new UriQueryExample("key1=value1%26&key2=value2") { QueryParameters = new [] { new KeyValuePair<string, string[]>("key1", new [] { "value1%26" } ), new KeyValuePair<string, string[]>("key2", new [] { "value2" } ) }  },
                new UriQueryExample("#", "")
            });

            this.Pass.AddRange(new UriFragmentExample[]
            {
                new UriFragmentExample("test"),
                new UriFragmentExample("#test", "test"),
                new UriFragmentExample("#", null)
            });

            this.Pass.AddRange(new UriExample[]
            {
                new UriExample("test:", "test://"),
                new UriExample("http://www.fosol.ca", "http://www.fosol.ca/"),
                new UriExample("http://www.fosol.ca/index.html"),
                new UriExample("http://www.fosol.ca/index.html?key1=value1") { QueryParameters = new [] { new KeyValuePair<string, string[]>("key1", new [] { "value1" } ) }  },
                new UriExample("http://www.fosol.ca/index.html?key1=value1&key2=value2") { QueryParameters = new [] { new KeyValuePair<string, string[]>("key1", new [] { "value1" } ), new KeyValuePair<string, string[]>("key2", new [] { "value2" }) }  },
                new UriExample("http://www.fosol.ca/index.html?key1=value1&key1=value2") { QueryParameters = new [] { new KeyValuePair<string, string[]>("key1", new [] { "value1", "value2" } ) }  },
                new UriExample("http://www.fosol.ca/index.html?key1") { QueryParameters = new [] { new KeyValuePair<string, string[]>("key1", new [] { "" } ) }  },
                new UriExample("http://www.fosol.ca/index.html?key1=", "http://www.fosol.ca/index.html?key1") { QueryParameters = new [] { new KeyValuePair<string, string[]>("key1", new [] { "" } ) }  },
                new UriExample("http://www.fosol.ca/index.html?=value1") { QueryParameters = new [] { new KeyValuePair<string, string[]>("", new [] { "value1" } ) }  },
                new UriExample("http://www.fosol.ca/index.html?=") { QueryParameters = new [] { new KeyValuePair<string, string[]>("=", new [] { "" } ) }  },
                new UriExample("http://www.fosol.ca/index.html?key1=value1&amp;&key2=value2")
            });
        }

        private void InitializeFail()
        {
            this.Fail.AddRange(new UriSchemeExample[]
            {
                new UriSchemeExample(null),
                new UriSchemeExample(""),
                new UriSchemeExample(" "),
                new UriSchemeExample("%20"),
                new UriSchemeExample("0123456789")
            });

            this.Fail.AddRange(new UriUserInfoExample[]
            {
                new UriUserInfoExample(":"),
                new UriUserInfoExample(ReservedGeneralDelims)
            });

            this.Fail.AddRange(new UriAuthorityExample[]
            {
            });

            this.Fail.AddRange(new UriHostExample[]
            {
                new UriHostExample("www.fosol.ca:12345"),
                new UriHostExample("www.fosol.ca:"),
                new UriHostExample("@")
            });

            this.Fail.AddRange(new UriPathExample[]
            {
                new UriPathExample("//"),
                new UriPathExample(@"\"),
                new UriPathExample("{"),
                new UriPathExample("}"),
                new UriPathExample("["),
                new UriPathExample("]")
            });

            this.Fail.AddRange(new UriQueryExample[]
            {
                new UriQueryExample(@"\")
            });

            this.Fail.AddRange(new UriFragmentExample[]
            {
                new UriFragmentExample(@"\")
            });

            this.Fail.AddRange(new UriExample[]
            {
                new UriExample("?key1=value1%26&key2=value2"),
                new UriExample("key1=value1%26&key2=value2"),
                new UriExample("#")
            });
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
