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
        /// <summary>
        /// Test various escape sequences to ensure a format is correctly created from the syntax.
        /// </summary>
        [TestMethod]
        public void EscapedFormats()
        {
            var parser = new Fosol.Common.Parsers.ElementParser();

            var format = parser.Parse("{guid");
            Assert.IsNotNull(format);
            Assert.IsTrue(format.Elements.Count == 1);
            Assert.IsTrue(format.Elements[0] is Fosol.Common.Parsers.Elements.TextElement);
            var output = format.Render(null);
            Assert.IsTrue(output.Equals("{guid"));

            format = parser.Parse("guid}");
            Assert.IsNotNull(format);
            Assert.IsTrue(format.Elements.Count == 1);
            Assert.IsTrue(format.Elements[0] is Fosol.Common.Parsers.Elements.TextElement);
            output = format.Render(null);
            Assert.IsTrue(output.Equals("guid}"));

            format = parser.Parse("{{guid}}");
            Assert.IsNotNull(format);
            Assert.IsTrue(format.Elements.Count == 1);
            Assert.IsTrue(format.Elements[0] is Fosol.Common.Parsers.Elements.TextElement);
            output = format.Render(null);
            Assert.IsTrue(output.Equals("{guid}"));

            format = parser.Parse("{guid}}");
            Assert.IsNotNull(format);
            Assert.IsTrue(format.Elements.Count == 1);
            Assert.IsTrue(format.Elements[0] is Fosol.Common.Parsers.Elements.TextElement);
            output = format.Render(null);
            Assert.IsTrue(output.Equals("{guid}"));

            format = parser.Parse("{{guid}}");
            Assert.IsNotNull(format);
            Assert.IsTrue(format.Elements.Count == 1);
            Assert.IsTrue(format.Elements[0] is Fosol.Common.Parsers.Elements.TextElement);
            output = format.Render(null);
            Assert.IsTrue(output.Equals("{guid}"));

            format = parser.Parse("{{guid");
            Assert.IsNotNull(format);
            Assert.IsTrue(format.Elements.Count == 1);
            Assert.IsTrue(format.Elements[0] is Fosol.Common.Parsers.Elements.TextElement);
            output = format.Render(null);
            Assert.IsTrue(output.Equals("{guid"));

            format = parser.Parse("guid}}");
            Assert.IsNotNull(format);
            Assert.IsTrue(format.Elements.Count == 1);
            Assert.IsTrue(format.Elements[0] is Fosol.Common.Parsers.Elements.TextElement);
            output = format.Render(null);
            Assert.IsTrue(output.Equals("guid}"));

            format = parser.Parse("{guid{}}");
            Assert.IsNotNull(format);
            Assert.IsTrue(format.Elements.Count == 1);
            Assert.IsTrue(format.Elements[0] is Fosol.Common.Parsers.Elements.TextElement);
            output = format.Render(null);
            Assert.IsTrue(output.Equals("guid{}"));

            format = parser.Parse("{guid{test}}");
            Assert.IsNotNull(format);
            Assert.IsTrue(format.Elements.Count == 1);
            Assert.IsTrue(format.Elements[0] is Fosol.Common.Parsers.Elements.TextElement);
            output = format.Render(null);
            Assert.IsTrue(output.Equals("guid{test}"));

            format = parser.Parse("{guid{test}}{guid}");
            Assert.IsNotNull(format);
            Assert.IsTrue(format.Elements.Count == 2);
            Assert.IsTrue(format.Elements[0] is Fosol.Common.Parsers.Elements.TextElement);
            Assert.IsTrue(format.Elements[1] is Fosol.Common.Parsers.Elements.GuidElement);
            output = format.Render(null);
            Assert.IsTrue(output.StartsWith("guid{test}"));

            format = parser.Parse("{guid{test}} {guid}");
            Assert.IsNotNull(format);
            Assert.IsTrue(format.Elements.Count == 3);
            Assert.IsTrue(format.Elements[0] is Fosol.Common.Parsers.Elements.TextElement);
            Assert.IsTrue(format.Elements[1] is Fosol.Common.Parsers.Elements.TextElement);
            Assert.IsTrue(format.Elements[2] is Fosol.Common.Parsers.Elements.GuidElement);
            output = format.Render(null);
            Assert.IsTrue(output.StartsWith("guid{test} "));

            format = parser.Parse("{guid{test}?value={test2}}");
            Assert.IsNotNull(format);
            Assert.IsTrue(format.Elements.Count == 1);
            Assert.IsTrue(format.Elements[0] is Fosol.Common.Parsers.Elements.TextElement);
            output = format.Render(null);
            Assert.IsTrue(output.StartsWith("guid{test}"));

            format = parser.Parse("{guid{{test}}");
            Assert.IsNotNull(format);
            Assert.IsTrue(format.Elements.Count == 1);
            Assert.IsTrue(format.Elements[0] is Fosol.Common.Parsers.Elements.TextElement);
            output = format.Render(null);
            Assert.IsTrue(output.StartsWith("{guid{test}"));
        }

        /// <summary>
        /// Test AppDomainElement.
        /// </summary>
        [TestMethod]
        public void AppDomainElement()
        {
            var parser = new Fosol.Common.Parsers.ElementParser();
            var format = parser.Parse("This is a test for AppDomainElment ({appdomain}).");

            Assert.IsNotNull(format);
            Assert.IsTrue(format.Elements.Count == 3);
            Assert.IsTrue(format.Elements[0] is Fosol.Common.Parsers.Elements.TextElement);
            Assert.IsTrue(format.Elements[1] is Fosol.Common.Parsers.Elements.AppDomainElement);
            Assert.IsTrue(format.Elements[2] is Fosol.Common.Parsers.Elements.TextElement);

            var output = format.Render(null);

            Assert.IsNotNull(output);
        }

        /// <summary>
        /// Test CounterElement.
        /// </summary>
        [TestMethod]
        public void CounterElement()
        {
            var parser = new Fosol.Common.Parsers.ElementParser();
            var format = parser.Parse("{counter}");

            Assert.IsNotNull(format);
            Assert.IsTrue(format.Elements.Count == 1);
            Assert.IsTrue(format.Elements[0] is Fosol.Common.Parsers.Elements.CounterElement);

            var output = format.Render(null);
            Assert.IsTrue(int.Parse(output) == 0);

            output = format.Render(null);
            Assert.IsTrue(int.Parse(output) == 1);

            // Start at 5.
            var format1 = parser.Parse("{counter?name=start5&value=5}");

            Assert.IsNotNull(format1);
            Assert.IsTrue(format1.Elements.Count == 1);
            Assert.IsTrue(format1.Elements[0] is Fosol.Common.Parsers.Elements.CounterElement);

            output = format1.Render(null);
            Assert.IsTrue(int.Parse(output) == 5);

            output = format1.Render(null);
            Assert.IsTrue(int.Parse(output) == 6);

            // Start at 3.
            var format2 = parser.Parse("{counter?name=inc5&value=3&inc=5}");

            Assert.IsNotNull(format2);
            Assert.IsTrue(format2.Elements.Count == 1);
            Assert.IsTrue(format2.Elements[0] is Fosol.Common.Parsers.Elements.CounterElement);

            output = format2.Render(null);
            Assert.IsTrue(int.Parse(output) == 3);

            output = format2.Render(null);
            Assert.IsTrue(int.Parse(output) == 8);

            // A secondary use of the element should use the default position.
            var format3 = parser.Parse("{counter?inc=10}");

            Assert.IsNotNull(format3);
            Assert.IsTrue(format3.Elements.Count == 1);
            Assert.IsTrue(format3.Elements[0] is Fosol.Common.Parsers.Elements.CounterElement);

            output = format3.Render(null);
            Assert.IsTrue(int.Parse(output) == 11);

            output = format.Render(null);
            Assert.IsTrue(int.Parse(output) == 12);
        }

        /// <summary>
        /// Test DateTimeElement with format parameter.
        /// </summary>
        [TestMethod]
        public void DateTimeElement()
        {
            var parser = new Fosol.Common.Parsers.ElementParser();
            var format = parser.Parse("This is a test for DateTimeElement ({datetime?format=hh:mm}).");

            Assert.IsNotNull(format);
            Assert.IsTrue(format.Elements.Count == 3);
            Assert.IsTrue(format.Elements[0] is Fosol.Common.Parsers.Elements.TextElement);
            Assert.IsTrue(format.Elements[1] is Fosol.Common.Parsers.Elements.DateTimeElement);
            Assert.IsTrue(format.Elements[2] is Fosol.Common.Parsers.Elements.TextElement);
            Assert.IsTrue(format.Elements[1].Attributes.Count == 1);
            Assert.IsNotNull(((Fosol.Common.Parsers.Elements.DateTimeElement)format.Elements[1]).Format);

            var output = format.Render(null);

            Assert.IsNotNull(output);
        }

        /// <summary>
        /// Test GuidElement with format parameter.
        /// </summary>
        [TestMethod]
        public void GuidElement()
        {
            var parser = new Fosol.Common.Parsers.ElementParser();
            var format = parser.Parse("{guid?format=N}");

            Assert.IsNotNull(format);
            Assert.IsTrue(format.Elements.Count == 1);
            Assert.IsTrue(format.Elements[0] is Fosol.Common.Parsers.Elements.GuidElement);

            var output = format.Render(null);

            Assert.IsNotNull(output);
            Guid guid;
            Assert.IsTrue(Guid.TryParse(output, out guid));
        }

        /// <summary>
        /// Test for IdentityElement with format.
        /// </summary>
        [TestMethod]
        public void IdentityElement()
        {
            var parser = new Fosol.Common.Parsers.ElementParser();
            var format = parser.Parse("{identity?auth=false&type=false}");

            Assert.IsNotNull(format);
            Assert.IsTrue(format.Elements.Count == 1);
            Assert.IsTrue(format.Elements[0] is Fosol.Common.Parsers.Elements.IdentityElement);

            var output = format.Render(null);

            Assert.IsNotNull(output);
        }

        /// <summary>
        /// Test for MachineNameElement.
        /// </summary>
        [TestMethod]
        public void MachineNameElement()
        {
            var parser = new Fosol.Common.Parsers.ElementParser();
            var format = parser.Parse("{machineName}");

            Assert.IsNotNull(format);
            Assert.IsTrue(format.Elements.Count == 1);
            Assert.IsTrue(format.Elements[0] is Fosol.Common.Parsers.Elements.MachineNameElement);

            var output = format.Render(null);
            Assert.IsNotNull(output);
            Assert.IsTrue(output.Equals(Environment.MachineName));
        }

        /// <summary>
        /// Test for NewlineElement.
        /// </summary>
        [TestMethod]
        public void NewlineElement()
        {
            var parser = new Fosol.Common.Parsers.ElementParser();
            var format = parser.Parse("{newline}");

            Assert.IsNotNull(format);
            Assert.IsTrue(format.Elements.Count == 1);
            Assert.IsTrue(format.Elements[0] is Fosol.Common.Parsers.Elements.NewlineElement);

            var output = format.Render(null);
            Assert.IsNotNull(output);
        }

        /// <summary>
        /// Test for ParameterElement.
        /// </summary>
        [TestMethod]
        public void ParameterElement()
        {
            var parser = new Fosol.Common.Parsers.ElementParser();
            var format = parser.Parse("{parameter?name=@id&value=test}");

            Assert.IsNotNull(format);
            Assert.IsTrue(format.Elements.Count == 1);
            Assert.IsTrue(format.Elements[0] is Fosol.Common.Parsers.Elements.ParameterElement);

            var output = format.Render(null);
            Assert.IsNotNull(output);
            Assert.IsTrue(output.Equals("@id"));

            output = format.Render("{0}");
            Assert.IsNotNull(output);
            Assert.IsTrue(output.Equals("@id"));

            output = format.Render("{0}={1}");
            Assert.IsNotNull(output);
            Assert.IsTrue(output.Equals("@id=test"));
        }

        /// <summary>
        /// Test TextElement.
        /// </summary>
        [TestMethod]
        public void TextElement()
        {
            var parser = new Fosol.Common.Parsers.ElementParser();
            var format = parser.Parse("This is a test for TextElement ({{}}).");

            Assert.IsNotNull(format);
            Assert.IsTrue(format.Elements.Count == 1);
            Assert.IsTrue(format.Elements[0] is Fosol.Common.Parsers.Elements.TextElement);

            var output = format.Render(null);

            Assert.IsNotNull(output);
            Assert.IsTrue(output == "This is a test for TextElement ({}).");
        }

        /// <summary>
        /// Test for ThreadElement.
        /// </summary>
        [TestMethod]
        public void ThreadElement()
        {
            var current_thread = System.Threading.Thread.CurrentThread;
            var parser = new Fosol.Common.Parsers.ElementParser();
            var format = parser.Parse("{thread}");

            Assert.IsNotNull(format);
            Assert.IsTrue(format.Elements.Count == 1);
            Assert.IsTrue(format.Elements[0] is Fosol.Common.Parsers.Elements.ThreadElement);

            var output = format.Render(null);
            Assert.IsNotNull(output);
            Assert.IsTrue(current_thread.Name == null || output.Equals(current_thread.Name));

            format = parser.Parse("{thread?key=ManagedThreadId}");

            Assert.IsNotNull(format);
            Assert.IsTrue(format.Elements.Count == 1);
            Assert.IsTrue(format.Elements[0] is Fosol.Common.Parsers.Elements.ThreadElement);

            output = format.Render(null);
            Assert.IsNotNull(output);
            Assert.IsTrue(int.Parse(output) == current_thread.ManagedThreadId);
        }

        /// <summary>
        /// Test for TickElement.
        /// </summary>
        [TestMethod]
        public void TicksElement()
        {
            var current_thread = System.Threading.Thread.CurrentThread;
            var parser = new Fosol.Common.Parsers.ElementParser();
            var format = parser.Parse("{ticks}");

            Assert.IsNotNull(format);
            Assert.IsTrue(format.Elements.Count == 1);
            Assert.IsTrue(format.Elements[0] is Fosol.Common.Parsers.Elements.TicksElement);

            var output = format.Render(null);
            Assert.IsNotNull(output);
        }

        /// <summary>
        /// Test for TimestampElement.
        /// </summary>
        [TestMethod]
        public void TimestampElement()
        {
            var current_thread = System.Threading.Thread.CurrentThread;
            var parser = new Fosol.Common.Parsers.ElementParser();
            var format = parser.Parse("{timestamp}");

            Assert.IsNotNull(format);
            Assert.IsTrue(format.Elements.Count == 1);
            Assert.IsTrue(format.Elements[0] is Fosol.Common.Parsers.Elements.TimestampElement);

            var output = format.Render(null);
            Assert.IsNotNull(output);
        }

        /// <summary>
        /// Test for UserElement.
        /// </summary>
        [TestMethod]
        public void UserElement()
        {
            var current_thread = System.Threading.Thread.CurrentThread;
            var parser = new Fosol.Common.Parsers.ElementParser();
            var format = parser.Parse("{user}");

            Assert.IsNotNull(format);
            Assert.IsTrue(format.Elements.Count == 1);
            Assert.IsTrue(format.Elements[0] is Fosol.Common.Parsers.Elements.UserElement);

            var output = format.Render(null);
            Assert.IsNotNull(output);
        }

        /// <summary>
        /// Test for ValueElement.
        /// </summary>
        [TestMethod]
        public void ValueElement()
        {
            var current_thread = System.Threading.Thread.CurrentThread;
            var parser = new Fosol.Common.Parsers.ElementParser();
            var format = parser.Parse("{value}");

            Assert.IsNotNull(format);
            Assert.IsTrue(format.Elements.Count == 1);
            Assert.IsTrue(format.Elements[0] is Fosol.Common.Parsers.Elements.ValueElement);

            var output = format.Render(10);
            Assert.IsNotNull(output);
            Assert.IsTrue(int.Parse(output) == 10);

            format = parser.Parse("{value?format={0:00.00}}");

            Assert.IsNotNull(format);
            Assert.IsTrue(format.Elements.Count == 1);
            Assert.IsTrue(format.Elements[0] is Fosol.Common.Parsers.Elements.ValueElement);
            Assert.IsTrue(format.Elements[0].Attributes.Count == 1);

            output = format.Render(5.4);
            Assert.IsNotNull(output);
            Assert.IsTrue(output.Equals("05.40"));
            Assert.IsTrue(double.Parse(output) == 5.4);
        }
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
