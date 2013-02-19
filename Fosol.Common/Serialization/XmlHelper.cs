using Fosol.Common.Extensions.Streams;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fosol.Common.Serialization
{
    /// <summary>
    /// Utility methods to serialize Xml data.
    /// </summary>
    public static class XmlHelper
    {
        #region Methods
        /// <summary>
        /// Serialize the object into a string using the XmlSerializer.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "data" must be defined with a SerializableAttribute.</exception>
        /// <exception cref="System.ArgumentNullException">Paramter "data" cannot be null.</exception>
        /// <param name="data">Object to serialize.</param>
        /// <returns>Serialized object as a string.</returns>
        public static string Serialize(object data)
        {
            Validation.Assert.IsNotNull(data, "data");
            Validation.Assert.HasAttribute(data, typeof(SerializableAttribute), "data");

            using (MemoryStream stream = new MemoryStream())
            {
                ToStream(data, stream);
                return stream.WriteToString();
            }
        }

        /// <summary>
        /// Converts a DataContract object into a stream.
        /// The object must be defined with the SerializableAttribute.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "data" must be defined with a SerializableAttribute.</exception>
        /// <exception cref="System.ArgumentNullException">Parameters "data", and "stream" cannot be null.</exception>
        /// <param name="data">Serializable object to serialize to stream.</param>
        /// <param name="stream">Stream to write object to.</param>
        public static void ToStream(object data, Stream stream)
        {
            Validation.Assert.IsNotNull(data, "data");
            Validation.Assert.HasAttribute(data, typeof(SerializableAttribute), "data");
            Validation.Assert.IsNotNull(stream, "stream");

            var serializer = new System.Xml.Serialization.XmlSerializer(data.GetType());
            serializer.Serialize(stream, data);
            stream.Position = 0;
        }

        /// <summary>
        /// Deserialize the stream into the specified object.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Parameter "stream" cannot be null.</exception>
        /// <typeparam name="T">Type of object to create from the serialized stream.</typeparam>
        /// <param name="stream">Stream object containing the serialized data.</param>
        /// <returns>Object of type T.</returns>
        public static T Deserialize<T>(Stream stream)
        {
            Validation.Assert.IsNotNull(stream, "stream");

            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(stream);
        }

        /// <summary>
        /// Deserialize the Xml string value into an object of the specified type.
        /// Uses the XmlSerializer object to deserialize.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "data" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "data" cannot be null.</exception>
        /// <typeparam name="T">Type of object to create.</typeparam>
        /// <param name="data">String data to deserialize.</param>
        /// <returns>Object of type T.</returns>
        public static T Deserialize<T>(string data)
        {
            Validation.Assert.IsNotNullOrEmpty(data, "data");

            var deserializer = new XmlSerializer(typeof(T));

            using (var stream = data.ToMemoryStream())
            {
                stream.Position = 0;
                return (T)deserializer.Deserialize(stream);
            }
        }

        /// <summary>
        /// Serialize object and save the data as a file at the specified location.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "path" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "path" cannot be null.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "data" cannot be null.</exception>
        /// <param name="data">Object to serialize and save.</param>
        /// <param name="path">Path and filename of the location you want to save the data.</param>
        /// <param name="fileMode">File mode control.</param>
        /// <param name="fileAccess">File access control.</param>
        /// <param name="fileShare">File share control.</param>
        public static void SerializeToFile(object data, string path, FileMode fileMode = FileMode.CreateNew, FileAccess fileAccess = FileAccess.Write, FileShare fileShare = FileShare.None)
        {
            Validation.Assert.IsNotNull(data, "data");
            Validation.Assert.IsNotNullOrEmpty(path, "path");

            using (Stream stream = File.Open(path, fileMode, fileAccess, fileShare))
            {
                ToStream(data, stream);
            }
        }

        /// <summary>
        /// Deserialize the file an create an object of the specified type.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "path" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "path" cannot be null.</exception>
        /// <typeparam name="T">Type of object to create.</typeparam>
        /// <param name="path">Path and filename of the location you want to deserialize.</param>
        /// <returns>Object of type T.</returns>
        public static T DeserializeFromFile<T>(string path)
        {
            Validation.Assert.IsNotNullOrEmpty(path, "path");

            using (var stream = File.OpenRead(path))
            {
                var reader = new XmlSerializer(typeof(T));
                return (T)reader.Deserialize(stream);
            }
        }

        /// <summary>
        /// Deserialize a stream from isolated storage and return the object.
        /// </summary>
        /// <typeparam name="T">Type of object being deserialized.</typeparam>
        /// <param name="stream">IsolatedStorageFileStream object.</param>
        /// <returns>Object of type T.</returns>
        public static T Deserialize<T>(System.IO.IsolatedStorage.IsolatedStorageFileStream stream)
        {
            Validation.Assert.IsNotNull(stream, "stream");
            Validation.Assert.IsValue(stream.CanRead, "true", "stream.CanRead");

            using (var reader = new StreamReader(stream))
            {
                return Deserialize<T>(reader.ReadToEnd());
            }
        }
        #endregion
    }
}
