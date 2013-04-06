using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Data
{
    /// <summary>
    /// Lightweight class to capture table name and schema information.
    /// </summary>
    public class TableName
    {
        #region Variables
        #endregion

        #region Properties
        /// <summary>
        /// get - The name of the table.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// get - The table namespace.
        /// </summary>
        public string TableNamespace { get; private set; }
        #endregion

        #region Constructors
        public TableName(string name, string tableNamespace = null)
        {
            this.Name = name;
            this.TableNamespace = tableNamespace;
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
