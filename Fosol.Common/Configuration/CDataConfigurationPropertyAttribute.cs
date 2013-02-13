using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Configuration
{
    /// <summary>
    /// Use this property in the configuration class that needs access to the text within a node.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CDataConfigurationPropertyAttribute
        : Attribute
    {
    }
}
