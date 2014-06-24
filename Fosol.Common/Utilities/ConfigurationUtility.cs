using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Utilities
{
    /// <summary>
    /// Helper methods for configuration settings.
    /// </summary>
    public static class ConfigurationUtility
    {
        #region Methods
        /// <summary>
        /// Get the AppSetting key value from the configuration file for the specified assembly.
        /// This method is useful when using assemblies from other sources.
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException">Configuration collection does not contain requested key.</exception>
        /// <param name="assembly">Assembly which you want the key value from.</param>
        /// <param name="key">Key name.</param>
        /// <returns>Value of AppSetting key value.</returns>
        public static string GetAppSetting(System.Reflection.Assembly assembly, string key)
        {
            var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(assembly.Location);

            if (config == null || config.AppSettings.Settings[key] == null)
                throw new IndexOutOfRangeException(string.Format(Resources.Multilingual.Exception_Config_Does_Not_Contain_Key, key));

            return config.AppSettings.Settings[key].Value;
        }
        #endregion
    }
}
