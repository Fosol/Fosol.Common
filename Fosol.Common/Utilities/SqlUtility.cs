using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fosol.Common.Utilities
{
    /// <summary>
    /// SqlUtility provides a handful of helpful methods to interact with databases.
    /// </summary>
    public static class SqlUtility
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Methods
        /// <summary>
        /// Execute the script specified at the path.
        /// </summary>
        /// <param name="connection">Connection to the database.</param>
        /// <param name="path">Path to the SQL script file.</param>
        /// <param name="commandTimeout">The time in seconds to wait for the command to execute.  '0' means no timeout.</param>
        /// <returns>The number of rows affected by the script.</returns>
        public static int ExecuteScript(System.Data.IDbConnection connection, string path, int commandTimeout = 0)
        {
            Fosol.Common.Validation.Assert.IsNotNull(connection, "connection");
            Fosol.Common.Validation.Assert.IsNotNullOrEmpty(path, "path");

            var script = File.ReadAllText(path);

            // Need to parse the script for each individual 'GO' statement because a DbCommand cannot execute that keyword.
            var steps = Regex.Split(script, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant).Where(s => s != String.Empty);
            int result = 0;

            var cmd = connection.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandTimeout = commandTimeout;

            foreach (var sql in steps)
            {
                cmd.CommandText = sql;
                result += cmd.ExecuteNonQuery();
            }

            return result;
        }
        #endregion

        #region Operators
        #endregion

        #region Events
        #endregion
    }
}
