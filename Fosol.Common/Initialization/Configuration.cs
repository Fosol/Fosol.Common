using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
            Validation.Assert.IsNotNullOrEmpty(key, "key");
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
            Validation.Assert.IsNotNullOrEmpty(key, "key");
            var value = System.Configuration.ConfigurationManager.AppSettings[key];
            return Initialization.Assert.TryParse(value, defaultValue);
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
            Validation.Assert.IsNotNullOrEmpty(key, "key");
            var value = System.Configuration.ConfigurationManager.AppSettings[key];
            return Initialization.Assert.TryParse(value, defaultValue);
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
            Validation.Assert.IsNotNullOrEmpty(key, "key");
            var value = System.Configuration.ConfigurationManager.AppSettings[key];
            return Initialization.Assert.TryParse(value, defaultValue);
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
            Validation.Assert.IsNotNullOrEmpty(key, "key");
            var value = System.Configuration.ConfigurationManager.AppSettings[key];
            return Initialization.Assert.TryParse(value, defaultValue);
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
            Validation.Assert.IsNotNullOrEmpty(key, "key");
            var value = System.Configuration.ConfigurationManager.AppSettings[key];
            return Initialization.Assert.TryParse(value, defaultValue);
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
            Validation.Assert.IsNotNullOrEmpty(key, "key");
            var value = System.Configuration.ConfigurationManager.AppSettings[key];
            return Initialization.Assert.TryParse(value, defaultValue);
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
            Validation.Assert.IsNotNullOrEmpty(key, "key");
            var value = System.Configuration.ConfigurationManager.AppSettings[key];
            return Initialization.Assert.TryParse(value, defaultValue);
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
            Validation.Assert.IsNotNullOrEmpty(key, "key");
            var value = System.Configuration.ConfigurationManager.AppSettings[key];
            return Initialization.Assert.TryParse(value, defaultValue);
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
            Validation.Assert.IsNotNullOrEmpty(key, "key");
            var value = System.Configuration.ConfigurationManager.AppSettings[key];
            return Initialization.Assert.TryParse(value, defaultValue);
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
            Validation.Assert.IsNotNullOrEmpty(key, "key");
            var value = System.Configuration.ConfigurationManager.AppSettings[key];
            return Initialization.Assert.TryParse(value, defaultValue);
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
            Validation.Assert.IsNotNullOrEmpty(key, "key");
            var value = System.Configuration.ConfigurationManager.AppSettings[key];
            return Initialization.Assert.TryParse(value, defaultValue);
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
            Validation.Assert.IsNotNullOrEmpty(key, "key");
            var value = System.Configuration.ConfigurationManager.AppSettings[key];
            return Initialization.Assert.TryParse(value, defaultValue);
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
            Validation.Assert.IsNotNullOrEmpty(key, "key");
            var value = System.Configuration.ConfigurationManager.AppSettings[key];
            return Initialization.Assert.TryParse(value, defaultValue);
        }

        #endregion

        #region NameValueCollection Methods
        public delegate T DefaultValueMethod<T>();

        /// <summary>
        /// Get the value for the specified key and convert it into the appropriate type.
        /// </summary>
        /// <exception cref="Exceptions.ConfigurationException">The configuration value for the specified key is not valid.</exception>
        /// <typeparam name="T">Type of value you want.</typeparam>
        /// <param name="config">NameValueCollection containing configuration information.</param>
        /// <param name="key">Key name to identify the configuration value.</param>
        /// <returns>The value for the specified key.</returns>
        public static T GetValue<T>(this NameValueCollection config, string key)
        {
            var value = config[key];

            if (value == null)
                return default(T);

            T result = default(T);

            if (Helpers.ReflectionHelper.TryConvert<T>(value, ref result))
                return result;

            throw new Exceptions.ConfigurationException(string.Format("The value of key '{0}' cannot be converted to type '{1}'", key, typeof(T).Name));
        }

        /// <summary>
        /// Get the value for the specified key and convert it into the appropriate type.
        /// </summary>
        /// <exception cref="Exceptions.ConfigurationException">The configuration value for the specified key is not valid.</exception>
        /// <typeparam name="T">Type of value you want.</typeparam>
        /// <param name="config">NameValueCollection containing configuration information.</param>
        /// <param name="key">Key name to identify the configuration value.</param>
        /// <param name="defaultValue">Default value if one is not specified in the configuration.</param>
        /// <returns>The value for the specified key.</returns>
        public static T GetValue<T>(this NameValueCollection config, string key, T defaultValue)
        {
            var value = config[key];

            if (value == null)
                return defaultValue;

            T result = default(T);

            if (Helpers.ReflectionHelper.TryConvert<T>(value, ref result))
                return result;

            throw new Exceptions.ConfigurationException(string.Format("The value of key '{0}' cannot be converted to type '{1}'", key, typeof(T).Name));
        }

        /// <summary>
        /// Get the value for the specified key and convert it into the appropriate type.
        /// This particular method essentially lazy loads the default value.
        /// </summary>
        /// <exception cref="Exceptions.ConfigurationException">The configuration value for the specified key is not valid.</exception>
        /// <typeparam name="T">Type of value you want.</typeparam>
        /// <param name="config">NameValueCollection containing configuration information.</param>
        /// <param name="key">Key name to identify the configuration value.</param>
        /// <param name="defaultValueMethod">A method that will return a default value.</param>
        /// <returns>The value for the specified key.</returns>
        public static T GetValue<T>(this NameValueCollection config, string key, DefaultValueMethod<T> defaultValueMethod)
        {
            var value = config[key];

            if (value == null)
                return defaultValueMethod();

            T result = default(T);

            if (Helpers.ReflectionHelper.TryConvert<T>(value, ref result))
                return result;

            throw new Exceptions.ConfigurationException(string.Format("The value of key '{0}' cannot be converted to type '{1}'", key, typeof(T).Name));
        }

        /// <summary>
        /// Get the value for the specified key and convert it into the appropriate type.
        /// Ensure that the value is within the minimum and maximum constraints.
        /// </summary>
        /// <exception cref="Exceptions.ConfigurationException">The configuration value for the specified key is not valid.</exception>
        /// <param name="config">NameValueCollection containing configuration information.</param>
        /// <param name="key">Key name to identify the configuration value.</param>
        /// <param name="defaultValue">Default value if one is not specified in the configuration.</param>
        /// <param name="minimum">The minimum value the key can be configured with.</param>
        /// <param name="maximum">The maximum value the key can be configured with.</param>
        /// <returns>The value for the specified key.</returns>
        public static int GetValue(this NameValueCollection config, string key, int defaultValue, int? minimum, int? maximum)
        {
            var value = GetValue(config, key, defaultValue);

            if (minimum.HasValue && value < minimum.Value)
                throw new Exceptions.ConfigurationException(string.Format("The value of key '{0}' must be greater than or equal to the minimum '{1}'.", key, minimum.Value));
            else if (maximum.HasValue && value > maximum.Value)
                throw new Exceptions.ConfigurationException(string.Format("The value of key '{0}' must be less than or equal to the maximum '{1}'.", key, maximum.Value));

            return value;
        }

        /// <summary>
        /// Get the value for the specified key and convert it into the appropriate type.
        /// Ensure that the value is within the minimum and maximum constraints.
        /// This particular method essentially lazy loads the default value.
        /// </summary>
        /// <exception cref="Exceptions.ConfigurationException">The configuration value for the specified key is not valid.</exception>
        /// <param name="config">NameValueCollection containing configuration information.</param>
        /// <param name="key">Key name to identify the configuration value.</param>
        /// <param name="defaultValueMethod">A method that will return a default value.</param>
        /// <param name="minimum">The minimum value the key can be configured with.</param>
        /// <param name="maximum">The maximum value the key can be configured with.</param>
        /// <returns>The value for the specified key.</returns>
        public static int GetValue(this NameValueCollection config, string key, DefaultValueMethod<int> defaultValueMethod, int? minimum, int? maximum)
        {
            var value = GetValue(config, key, defaultValueMethod);

            if (minimum.HasValue && value < minimum.Value)
                throw new Exceptions.ConfigurationException(string.Format("The value of key '{0}' must be greater than or equal to the minimum '{1}'.", key, minimum.Value));
            else if (maximum.HasValue && value > maximum.Value)
                throw new Exceptions.ConfigurationException(string.Format("The value of key '{0}' must be less than or equal to the maximum '{1}'.", key, maximum.Value));

            return value;
        }
        #endregion
    }
}
