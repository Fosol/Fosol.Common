using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Configuration
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class ConfigurationElementAttribute
        : Attribute
    {
        #region Variables
        #endregion

        #region Properties
        public string AddName { get; set; }
        #endregion

        #region Constructors
        public ConfigurationElementAttribute(string addName)
        {
            Fosol.Common.Validation.Assert.IsNotNullOrWhiteSpace(addName, "addName");
            this.AddName = addName;
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
