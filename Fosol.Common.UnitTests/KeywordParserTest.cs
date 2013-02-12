using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fosol.Common.UnitTests
{
    [TestClass]
    public class KeywordParserTest
    {
        [TestMethod]
        public void ParseText()
        {
            var format = "${datetime} - ${message}";
            var parser = new Common.Parsers.SimpleParser();
            var phrases = parser.Parse(format);

            // Should have found keywords.
            Assert.IsTrue(phrases != null);

            // There should be a total of three phrases.
            Assert.IsTrue(phrases.Count() == 3);

            // There should be two keywords and one phrase.
            Assert.IsTrue(phrases.Where(p => p is Parsers.Keyword).Count() == 2);
        }

        [TestMethod]
        public void ParseTextWithInvalidBoundary()
        {
            var text = "some raw text ${datetime?utc=true&format={0:u}}${time}";
            var parser = new Common.Parsers.SimpleParser();
            var phrases = parser.Parse(text);

            Assert.IsTrue(phrases != null);

            Assert.IsTrue(phrases.Count() == 2);

            Assert.IsTrue(phrases.Where(p => p is Parsers.Keyword).Count() == 1);
        }

        [TestMethod]
        public void ParseTextWithParams()
        {
            var text = "some raw text ${datetime?utc=true&format={0:u}}}";
            var parser = new Common.Parsers.SimpleParser();
            var phrases = parser.Parse(text);

            Assert.IsTrue(phrases != null);

            Assert.IsTrue(phrases.Count() == 2);

            Assert.IsTrue(phrases.Where(p => p is Parsers.Keyword).Count() == 1);
        }

        [TestMethod]
        public void ParseTextWithPartialParams()
        {
            var text = "some raw text ${datetime?utc=&format={0:u}}}";
            var parser = new Common.Parsers.SimpleParser();
            var phrases = parser.Parse(text);

            Assert.IsTrue(phrases != null);

            Assert.IsTrue(phrases.Count() == 2);

            Assert.IsTrue(phrases.Where(p => p is Parsers.Keyword).Count() == 1);
        }
    }
}
