using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fosol.Common.UnitTests.Configuration
{
    [TestClass]
    public class SpecialConfigurationCollectionTest
    {
        [TestMethod]
        public void CreateSpecialConfigurationSection()
        {
            var section = new TestSection();
            section.Items.Add(new OneElement());
            section.Items.Add(new TwoElement());

            Assert.IsTrue(section.Items.Count == 2);
        }

        [TestMethod]
        public void GetSpecialConfigurationSection()
        {
            var section = System.Configuration.ConfigurationManager.GetSection("special") as TestSection;

            Assert.IsNotNull(section);
        }

        [TestMethod]
        public void GetSpecialConfigurationSectionItems()
        {
            var section = System.Configuration.ConfigurationManager.GetSection("special") as TestSection;

            var element = section.Items["test one"];

            Assert.IsNotNull(element);
        }
    }
}
