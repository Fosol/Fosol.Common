using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.UnitTests.Web.Data
{
    class UriTestData
    {
        #region Variables
        public const string ReservedGeneralDelims = ":/?#[]@";
        public const string ReservedSubsetDelims = "!$&'()*+,;=";
        public const string Unreserved = Fosol.Common.Text.GlobalConstant.Alphanumeric + "-._~";
        #endregion

        #region Properties
        public UriSchemeExample[] Schemes { get; set; }
        public UriUserInfoExample[] UserInfos { get; set; }
        public UriAuthorityExample[] Authorities { get; set; }
        public UriHostExample[] Hosts { get; set; }
        public UriPathExample[] Paths { get; set; }
        public UriQueryExample[] Queries { get; set; }
        public UriFragmentExample[] Fragments { get; set; }
        public UriExample[] Uris { get; set; }
        #endregion

        #region Constructors
        public UriTestData()
        {
            this.Schemes = new UriSchemeExample[]
            {
                new UriSchemeExample(Fosol.Common.Text.GlobalConstant.Letter + Fosol.Common.Text.GlobalConstant.Alphanumeric + "+-."),
                new UriSchemeExample("http"),
                new UriSchemeExample("ftp"),
                new UriSchemeExample("https"),
                new UriSchemeExample("unc"),
                new UriSchemeExample("abcdefghijklmnopqrstuvwxyz"),
                new UriSchemeExample("ABCDEFGHIJKLMNOPQRSTUVWXYZ"),
                new UriSchemeExample("a0123456789"),
                new UriSchemeExample("a+-."),
                new UriSchemeExample(true, null),
                new UriSchemeExample(true, ""),
                new UriSchemeExample(true, " "),
                new UriSchemeExample(true, "%20"),
                new UriSchemeExample(true, "0123456789")
            };

            this.UserInfos = new UriUserInfoExample[]
            {
                new UriUserInfoExample("user"),
                new UriUserInfoExample("user:pwd", "user"),
                new UriUserInfoExample("user:pwd:stuff", "user"),
                new UriUserInfoExample(Unreserved + "%20" + ReservedSubsetDelims + ":", Unreserved + "%20" + ReservedSubsetDelims),
                new UriUserInfoExample(true, ":"),
                new UriUserInfoExample(true, ReservedGeneralDelims)
            };

            this.Authorities = new UriAuthorityExample[]
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
            };

            this.Hosts = new UriHostExample[]
            {
                new UriHostExample("www.fosol.ca"),
                new UriHostExample(Unreserved),
                new UriHostExample("%40"),
                new UriHostExample(ReservedSubsetDelims),
                new UriHostExample("test"),
                new UriHostExample(true, "www.fosol.ca:12345"),
                new UriHostExample(true, "www.fosol.ca:"),
                new UriHostExample(true, "@")
            };

            this.Paths = new UriPathExample[]
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
                new UriPathExample("ABCDEFGHIJKLMNOPQRSTUVWXYZ") { NumberOfSegments = 1 },
                new UriPathExample(true, "//"),
                new UriPathExample(true, @"\"),
                new UriPathExample(true, "{"),
                new UriPathExample(true, "}"),
                new UriPathExample(true, "["),
                new UriPathExample(true, "]")
            };

            this.Queries = new UriQueryExample[]
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
                new UriQueryExample("#", ""),
                new UriQueryExample(true, @"\")
            };

            this.Fragments = new UriFragmentExample[]
            {
                new UriFragmentExample("test"),
                new UriFragmentExample("#test", "test"),
                new UriFragmentExample("#", null),
                new UriFragmentExample(true, @"\")
            };

            this.Uris = new UriExample[]
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
                new UriExample("http://www.fosol.ca/index.html?key1=value1&amp;&key2=value2"),
                new UriExample(true, "?key1=value1%26&key2=value2"),
                new UriExample(true, "key1=value1%26&key2=value2"),
                new UriExample(true, "#")
            };
        }
        #endregion

        #region Methods

        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
