using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Initialization
{
    /// <summary>
    /// Methods to help initialize configuration settings.
    /// </summary>
    public static class Configuration
    {
        #region AppSetting Methods
        /// <summary>
        /// Read from the AppSetting configuration file and return the value for the specified key.
        /// If the AppSetting key is missing return the default value.
        /// </summary>
        /// <param name="key">Key name.</param>
        /// <param name="defaultValue">Default value to use if the AppSetting key is null.</param>
        /// <returns>Value of the key, or default value.</returns>
        public static string GetAppSetting(string key, string defaultValue = null)
        {
            Validation.Parameter.AssertIsNotNullOrEmpty(key, "key");
            var value = System.Configuration.ConfigurationManager.AppSettings[key];

            if (value == null)
                return defaultValue;
            else
                return value;
        }

        /// <summary>
        /// Read from the AppSetting configuration file and return the value for the specified key.
        /// If the AppSetting key is missing return the default value.
        /// </summary>
        /// <param name="key">Key name.</param>
        /// <param name="defaultValue">Default value to use if the AppSetting key is null.</param>
        /// <returns>Value of the key, or default value.</returns>
        public static bool GetAppSetting(string key, bool defaultValue = default(bool))
        {
            Validation.Parameter.AssertIsNotNullOrEmpty(key, "key");
            var value = System.Configuration.ConfigurationManager.AppSettings[key];
            return Initialization.Parameter.AssertTryParse(value, defaultValue);
        }

        /// <summary>
        /// Read from the AppSetting configuration file and return the value for the specified key.
        /// If the AppSetting key is missing return the default value.
        /// </summary>
        /// <param name="key">Key name.</param>
        /// <param name="defaultValue">Default value to use if the AppSetting key is null.</param>
        /// <returns>Value of the key, or default value.</returns>
        public static byte GetAppSetting(string key, byte defaultValue = default(byte))
        {
            Validation.Parameter.AssertIsNotNullOrEmpty(key, "key");
            var value = System.Configuration.ConfigurationManager.AppSettings[key];
            return Initialization.Parameter.AssertTryParse(value, defaultValue);
        }

        /// <summary>
        /// Read from the AppSetting configuration file and return the value for the specified key.
        /// If the AppSetting key is missing return the default value.
        /// </summary>
        /// <param name="key">Key name.</param>
        /// <param name="defaultValue">Default value to use if the AppSetting key is null.</param>
        /// <returns>Value of the key, or default value.</returns>
        public static sbyte GetAppSetting(string key, sbyte defaultValue = default(sbyte))
        {
            Validation.Parameter.AssertIsNotNullOrEmpty(key, "key");
            var value = System.Configuration.ConfigurationManager.AppSettings[key];
            return Initialization.Parameter.AssertTryParse(value, defaultValue);
        }

        /// <summary>
        /// Read from the AppSetting configuration file and return the value for the specified key.
        /// If the AppSetting key is missing return the default value.
        /// </summary>
        /// <param name="key">Key name.</param>
        /// <param name="defaultValue">Default value to use if the AppSetting key is null.</param>
        /// <returns>Value of the key, or default value.</returns>
        public static char GetAppSetting(string key, char defaultValue = default(char))
        {
            Validation.Parameter.AssertIsNotNullOrEmpty(key, "key");
            var value = System.Configuration.ConfigurationManager.AppSettings[key];
            return Initialization.Parameter.AssertTryParse(value, defaultValue);
        }

        /// <summary>
        /// Read from the AppSetting configuration file and return the value for the specified key.
        /// If the AppSetting key is missing return the default value.
        /// </summary>
        /// <param name="key">Key name.</param>
        /// <param name="defaultValue">Default value to use if the AppSetting key is null.</param>
        /// <returns>Value of the key, or default value.</returns>
        public static decimal GetAppSetting(string key, decimal defaultValue = default(decimal))
        {
            Validation.Parameter.AssertIsNotNullOrEmpty(key, "key");
            var value = System.Configuration.ConfigurationManager.AppSettings[key];
            return Initialization.Parameter.AssertTryParse(value, defaultValue);
        }

        /// <summary>
        /// Read from the AppSetting configuration file and return the value for the specified key.
        /// If the AppSetting key is missing return the default value.
        /// </summary>
        /// <param name="key">Key name.</param>
        /// <param name="defaultValue">Default value to use if the AppSetting key is null.</param>
        /// <returns>Value of the key, or default value.</returns>
        public static double GetAppSetting(string key, double defaultValue = default(double))
        {
            Validation.Parameter.AssertIsNotNullOrEmpty(key, "key");
            var value = System.Configuration.ConfigurationManager.AppSettings[key];
            return Initialization.Parameter.AssertTryParse(value, defaultValue);
        }

        /// <summary>
        /// Read from the AppSetting configuration file and return the value for the specified key.
        /// If the AppSetting key is missing return the default value.
        /// </summary>
        /// <param name="key">Key name.</param>
        /// <param name="defaultValue">Default value to use if the AppSetting key is null.</param>
        /// <returns>Value of the key, or default value.</returns>
        public static float GetAppSetting(string key, float defaultValue = default(float))
        {
            Validation.Parameter.AssertIsNotNullOrEmpty(key, "key");
            var value = System.Configuration.ConfigurationManager.AppSettings[key];
            return Initialization.Parameter.AssertTryParse(value, defaultValue);
        }

        /// <summary>
        /// Read from the AppSetting configuration file and return the value for the specified key.
        /// If the AppSetting key is missing return the default value.
        /// </summary>
        /// <param name="key">Key name.</param>
        /// <param name="defaultValue">Default value to use if the AppSetting key is null.</param>
        /// <returns>Value of the key, or default value.</returns>
        public static int GetAppSetting(string key, int defaultValue = default(int))
        {
            Validation.Parameter.AssertIsNotNullOrEmpty(key, "key");
            var value = System.Configuration.ConfigurationManager.AppSettings[key];
            return Initialization.Parameter.AssertTryParse(value, defaultValue);
        }

        /// <summary>
        /// Read from the AppSetting configuration file and return the value for the specified key.
        /// If the AppSetting key is missing return the default value.
        /// </summary>
        /// <param name="key">Key name.</param>
        /// <param name="defaultValue">Default value to use if the AppSetting key is null.</param>
        /// <returns>Value of the key, or default value.</returns>
        public static uint GetAppSetting(string key, uint defaultValue = default(uint))
        {
            Validation.Parameter.AssertIsNotNullOrEmpty(key, "key");
            var value = System.Configuration.ConfigurationManager.AppSettings[key];
            return Initialization.Parameter.AssertTryParse(value, defaultValue);
        }

        /// <summary>
        /// Read from the AppSetting configuration file and return the value for the specified key.
        /// If the AppSetting key is missing return the default value.
        /// </summary>
        /// <param name="key">Key name.</param>
        /// <param name="defaultValue">Default value to use if the AppSetting key is null.</param>
        /// <returns>Value of the key, or default value.</returns>
        public static long GetAppSetting(string key, long defaultValue = default(long))
        {
            Validation.Parameter.AssertIsNotNullOrEmpty(key, "key");
            var value = System.Configuration.ConfigurationManager.AppSettings[key];
            return Initialization.Parameter.AssertTryParse(value, defaultValue);
        }

        /// <summary>
        /// Read from the AppSetting configuration file and return the value for the specified key.
        /// If the AppSetting key is missing return the default value.
        /// </summary>
        /// <param name="key">Key name.</param>
        /// <param name="defaultValue">Default value to use if the AppSetting key is null.</param>
        /// <returns>Value of the key, or default value.</returns>
        public static ulong GetAppSetting(string key, ulong defaultValue = default(ulong))
        {
            Validation.Parameter.AssertIsNotNullOrEmpty(key, "key");
            var value = System.Configuration.ConfigurationManager.AppSettings[key];
            return Initialization.Parameter.AssertTryParse(value, defaultValue);
        }

        /// <summary>
        /// Read from the AppSetting configuration file and return the value for the specified key.
        /// If the AppSetting key is missing return the default value.
        /// </summary>
        /// <param name="key">Key name.</param>
        /// <param name="defaultValue">Default value to use if the AppSetting key is null.</param>
        /// <returns>Value of the key, or default value.</returns>
        public static short GetAppSetting(string key, short defaultValue = default(short))
        {
            Validation.Parameter.AssertIsNotNullOrEmpty(key, "key");
            var value = System.Configuration.ConfigurationManager.AppSettings[key];
            return Initialization.Parameter.AssertTryParse(value, defaultValue);
        }

        /// <summary>
        /// Read from the AppSetting configuration file and return the value for the specified key.
        /// If the AppSetting key is missing return the default value.
        /// </summary>
        /// <param name="key">Key name.</param>
        /// <param name="defaultValue">Default value to use if the AppSetting key is null.</param>
        /// <returns>Value of the key, or default value.</returns>
        public static ushort GetAppSetting(string key, ushort defaultValue = default(ushort))
        {
            Validation.Parameter.AssertIsNotNullOrEmpty(key, "key");
            var value = System.Configuration.ConfigurationManager.AppSettings[key];
            return Initialization.Parameter.AssertTryParse(value, defaultValue);
        }

        #endregion
    }
}
