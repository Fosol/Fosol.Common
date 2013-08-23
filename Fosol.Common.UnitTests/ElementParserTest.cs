using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fosol.Common.UnitTests
{
    /// <summary>
    /// Summary description for ElementParserTest
    /// </summary>
    [TestClass]
    public class ElementParserTest
    {
        #region Variables
        private TestContext testContextInstance;
        #endregion

        #region Properties
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        #endregion

        #region Constructors
        public ElementParserTest()
        {
        }
        #endregion

        #region Methods
        #region DateTime
        [TestMethod]
        public void DateTimeElement()
        {
            var text = "This is a test for DateTimeElement ({datetime?format={0:mmm}}}).";
            var parser = new Fosol.Common.Parsers.ElementParser();
            var format = parser.Parse(text);

            Assert.IsNotNull(format);
            Assert.IsTrue(format.Elements.Count == 3);

            var output = format.Render(null);

            Assert.IsNotNull(output);
        }
        #endregion
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion
    }
}
