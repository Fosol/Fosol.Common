using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
#if WINDOWS_PHONE
using System.Windows;
using System.Windows.Resources;
#endif

namespace Fosol.Common.Helpers
{
    /// <summary>
    /// Provides utility methods to load Assembly Resource Dictionary.
    /// </summary>
    public static class AssemblyResourceHelper
    {
        #region Methods
        #region WP7
#if WINDOWS_PHONE
        /// <summary>
        /// Creates an XDocument by loading a file from the local application path
        /// </summary>
        /// <param name="resourcePath">Uri to the local application file (resource)</param>
        /// <returns>XDocument object</returns>
        public static XDocument OpenXDocument(Uri path)
        {
            var streamInfo = GetResourceStream(path);
            using (var s = streamInfo.Stream)
                return XDocument.Load(s);
        }

        /// <summary>
        /// Opens a local resource and deserializes it into the specified object type.
        /// </summary>
        /// <typeparam name="T">The type of the object you are openning.</typeparam>
        /// <param name="path">Path and name of the file to open.</param>
        /// <returns>Deserialized object.</returns>
        public static T Open<T>(Uri path)
        {
            var streamInfo = GetResourceStream(path);
            using (var s = streamInfo.Stream)
                return Open<T>(s);
        }

        /// <summary>
        /// Gets a StreamResourceInfo to the specified file within the Application.
        /// </summary>
        /// <param name="path">Uri to the file.</param>
        /// <returns>StreamResourceInfo object to resource.</returns>
        public static StreamResourceInfo GetResourceStream(Uri path)
        {
            return Application.GetResourceStream(path);
        }
#endif
        #endregion

        /// <summary>
        /// Gets a Stream to the specified Assembly Resource.
        /// </summary>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.IO.FileLoadException"></exception>
        /// <exception cref="System.IO.FileNotFoundException"></exception>
        /// <exception cref="System.BadImageFormatException"></exception>
        /// <exception cref="System.MemberAccessException"></exception>
        /// <param name="name">Assembly resource full name.</param>
        /// <returns>Stream to assembly resource.</returns>
        public static Stream GetStream(string name)
        {
            // Fetch the XML from the embedded resources
            Assembly assembly = Assembly.GetExecutingAssembly();
            return assembly.GetManifestResourceStream(name);
        }


        /// <summary>
        /// Gets a Stream to the specified Assembly Resource.
        /// </summary>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.IO.FileLoadException"></exception>
        /// <exception cref="System.IO.FileNotFoundException"></exception>
        /// <exception cref="System.BadImageFormatException"></exception>
        /// <exception cref="System.MemberAccessException"></exception>
        /// <param name="assemblyName">Assembly full name.</param>
        /// <param name="resourceName">Assembly resource full name.</param>
        /// <returns>Stream to assembly resource.</returns>
        public static Stream GetStream(string assemblyName, string resourceName)
        {
            Assembly assembly = Assembly.Load(assemblyName);
            return assembly.GetManifestResourceStream(resourceName);
        }

        /// <summary>
        /// Deserialize a local assembly resource.
        /// </summary>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.IO.FileLoadException"></exception>
        /// <exception cref="System.IO.FileNotFoundException"></exception>
        /// <exception cref="System.BadImageFormatException"></exception>
        /// <exception cref="System.MemberAccessException"></exception>
        /// <typeparam name="T">Type of object being deserialized.</typeparam>
        /// <param name="assembly">The CallingAssembly.</param>
        /// <param name="name">Full name of resource.</param>
        /// <returns>Deserialized object.</returns>
        public static T Open<T>(Assembly assembly, string name)
        {
            using (Stream stream = assembly.GetManifestResourceStream(name))
            {
                return Serialization.XmlHelper.Deserialize<T>(stream);
            }
        }

        /// <summary>
        /// Deserialize a stream into the specified type.
        /// </summary>
        /// <typeparam name="T">Type to deserialize into.</typeparam>
        /// <param name="stream">Stream to resource file.</param>
        /// <returns>Deserialized object.</returns>
        public static T Open<T>(Stream stream)
        {
            return Serialization.XmlHelper.Deserialize<T>(stream);
        }
        #endregion
    }
}
