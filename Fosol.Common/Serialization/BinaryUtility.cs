﻿using Fosol.Common.Extensions.Streams;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Serialization
{
    /// <summary>
    /// Utility methods to serialize data.
    /// </summary>
    public static class BinaryUtility
    {
        #region Methods
        /// <summary>
        /// Serialize the object into a byte array with the BinaryFormatter.
        /// </summary>
        /// <param name="data">Object to serialize.</param>
        /// <returns>Byte array.</returns>
        public static byte[] Serialize(object data)
        {
            Validation.Argument.Assert.IsNotNull(data, "data");

            var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, data);
                stream.Seek(0, SeekOrigin.Begin);
                return stream.ToArray();
            }
        }

        /// <summary>
        /// Deserialize a byte array into the original object that it was serialized from.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "data" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "data" cannot be null.</exception>
        /// <param name="data">Byte array to deserialize.</param>
        /// <returns>A new instance of an object.</returns>
        public static object Deserialize(byte[] data)
        {
            Validation.Argument.Assert.IsNotNullOrEmpty(data, "data");

            var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            using (var stream = new System.IO.MemoryStream(data))
            {
                return formatter.Deserialize(stream);
            }
        }

        /// <summary>
        /// Deserialize a byte array into the specified type.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "data" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "data" cannot be null.</exception>
        /// <typeparam name="T">Type of object to create.</typeparam>
        /// <param name="data">Byte array to deserialize.</param>
        /// <returns>A new instance of the specified type.</returns>
        public static T Deserialize<T>(byte[] data)
        {
            return (T)Deserialize(data);
        }

        /// <summary>
        /// Serialize the data as a binary file.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "path" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameters "data", and "path" cannot be null.</exception>
        /// <exception cref="System.Runtime.Serialization.SerializationException">Parameter "data" must be serializable.</exception>
        /// <exception cref="System.Security.SecurityException">Parameter "path" must be a writable location.</exception>
        /// <param name="data">Object to serialize and save as a binary file.</param>
        /// <param name="path">Path and file name of object being saved.</param>
        /// <param name="fileMode">How should the file be opened.</param>
        /// <param name="fileAccess">What access should be granted to the file.</param>
        /// <param name="fileShare">Control the access to the file.</param>
        public static void SerializetoFile(object data, string path, FileMode fileMode = FileMode.CreateNew, FileAccess fileAccess = FileAccess.Write, FileShare fileShare = FileShare.None)
        {
            Validation.Argument.Assert.IsNotNull(data, "data");
            Validation.Argument.Assert.IsNotNullOrEmpty(path, "path");

            using (var stream = File.Open(path, fileMode, fileAccess, fileShare))
            {
                var writer = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                writer.Serialize(stream, data);
            }
        }

        /// <summary>
        /// Deserialize the file as an object of the specified type.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "path" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "path" cannot be null.</exception>
        /// <exception cref="System.Runtime.Serialization.SerializationException">Parameter "data" must be serializable.</exception>
        /// <exception cref="System.Security.SecurityException">Parameter "path" must be a writable location.</exception>
        /// <exception cref="System.IO.PathTooLongException"></exception>
        /// <exception cref="System.IO.DirectoryNotFoundException"></exception>
        /// <exception cref="System.UnauthorizedAccessException"></exception>
        /// <exception cref="System.IO.FileNotFoundException"></exception>
        /// <exception cref="System.NotSupportedException"></exception>
        /// <typeparam name="T">Type of object to deserialize into.</typeparam>
        /// <param name="path">Path and name of file to open.</param>
        /// <returns>Object of specified type.</returns>
        public static T DeserializeFromFile<T>(string path)
        {
            Validation.Argument.Assert.IsNotNullOrEmpty(path, "path");

            using (var stream = File.OpenRead(path))
            {
                var reader = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)reader.Deserialize(stream);
            }
        }
        #endregion
    }
}
