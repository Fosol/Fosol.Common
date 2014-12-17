﻿using Fosol.Common.Extensions.Streams;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
#if WINDOWS_APP || WINDOWS_PHONE_APP
using Windows.Storage;
using Windows.Storage.Streams;
#endif

namespace Fosol.Common.Serialization
{
    /// <summary>
    /// Utility methods to serialize DataContract data.
    /// </summary>
    public static class DataContractUtility
    {
        #region Variables
        private static IDictionary<Type, DataContractSerializer> _CachedSerializers = new Dictionary<Type, DataContractSerializer>();
        #endregion

        #region Methods
        /// <summary>
        /// From MSDN:
        /// To increase performance, the XML serialization infrastructure dynamically generates assemblies to serialize and 
        /// deserialize specified types. The infrastructure finds and reuses those assemblies. This behavior occurs only when 
        /// using the following constructors:
        /// 
        /// XmlSerializer.XmlSerializer(Type)
        /// XmlSerializer.XmlSerializer(Type, String)
        /// 
        /// If you use any of the other constructors, multiple versions of the same assembly are generated and never unloaded, 
        /// which results in a memory leak and poor performance. The easiest solution is to use one of the previously mentioned 
        /// two constructors. Otherwise, you must cache the assemblies.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Parameter "classType" cannot be null.</exception>
        /// <param name="classType">Type of class being serialized.</param>
        /// <returns>DataContractSerializer object.</returns>
        public static DataContractSerializer GetSerializer(Type classType)
        {
            Validation.Argument.Assert.IsNotNull(classType, "classType");

            if (!_CachedSerializers.ContainsKey(classType))
            {
                _CachedSerializers.Add(classType, new DataContractSerializer(classType));
            }
            return _CachedSerializers[classType];
        }

        /// <summary>
        /// Serialize the DataContract object into a string using the DataContractSerializer.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "data" must be defined with a DataContractAttribute.</exception>
        /// <exception cref="System.ArgumentNullException">Paramter "data" cannot be null.</exception>
        /// <param name="data">Object to serialize.</param>
        /// <returns>Serialized object as a string.</returns>
        public static string Serialize(object data)
        {
            Validation.Argument.Assert.IsNotNull(data, "data");

#if !WINDOWS_APP && !WINDOWS_PHONE_APP
            Validation.Argument.Assert.HasAttribute(data, typeof(System.Runtime.Serialization.DataContractAttribute), "data");
#endif

            using (var stream = new MemoryStream())
            {
                ToStream(data, stream);
                return stream.WriteToString();
            }
        }

        /// <summary>
        /// Converts a DataContract object into a stream.
        /// The object must be defined with the DataContractAttribute.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "data" must be defined with a DataContractAttribute.</exception>
        /// <exception cref="System.ArgumentNullException">Parameters "data", and "stream" cannot be null.</exception>
        /// <param name="data">DataContract object to serialize to stream.</param>
        /// <param name="stream">Stream to write object to.</param>
        /// <param name="resetPosition">Whether the position in the stream should be reset to where it began before writing.</param>
        public static void ToStream(object data, Stream stream, bool resetPosition = true)
        {
            Validation.Argument.Assert.IsNotNull(data, "data");
#if !WINDOWS_APP && !WINDOWS_PHONE_APP
            Validation.Argument.Assert.HasAttribute(data, typeof(System.Runtime.Serialization.DataContractAttribute), "data");
#endif
            Validation.Argument.Assert.IsNotNull(stream, "stream");
            Validation.Argument.Assert.IsTrue(stream.CanWrite, "stream", "Parameter 'stream' must be writable.");
            
            var position = stream.Position;

            var serializer = GetSerializer(data.GetType());
            serializer.WriteObject(stream, data);

            // Set the position to the beginning of the stream after writing to it.
            if (resetPosition)
                stream.Position = position;
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
            Validation.Argument.Assert.IsNotNull(stream, "stream");

            var serializer = GetSerializer(typeof(T));
            return (T)serializer.ReadObject(stream);
        }

        /// <summary>
        /// Deserialize the string value into an object of the specified type.
        /// Uses the DataContractSerializer object to deserialize.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "data" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "data" cannot be null.</exception>
        /// <typeparam name="T">Type of object to create.</typeparam>
        /// <param name="data">String data to deserialize.</param>
        /// <returns>Object of type T.</returns>
        public static T Deserialize<T>(string data)
        {
            Validation.Argument.Assert.IsNotNullOrEmpty(data, "data");

            var deserializer = GetSerializer(typeof(T));

            using (var stream = data.ToMemoryStream())
            {
                stream.Position = 0;
                return (T)deserializer.ReadObject(stream);
            }
        }

#if WINDOWS_APP || WINDOWS_PHONE_APP
        /// <summary>
        /// Serialize object and save the data as a file at the specified location.
        /// </summary>
        /// <exception cref="System.ArgumentException">Parameter "path" cannot be empty.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "path" cannot be null.</exception>
        /// <exception cref="System.ArgumentNullException">Parameter "data" cannot be null.</exception>
        /// <param name="data">Object to serialize and save.</param>
        /// <param name="path">Path and filename of the location you want to save the data.</param>
        /// <param name="collisionOption">What to do if the file already exists.</param>
        public static void SerializeToFile(object data, string path, CreationCollisionOption collisionOption = CreationCollisionOption.FailIfExists)
        {
            Validation.Argument.Assert.IsNotNull(data, "data");
            Validation.Argument.Assert.IsNotNullOrEmpty(path, "path");

            using (MemoryStream data_in_stream = new MemoryStream())
            {
                ToStream(data, data_in_stream);

                var file_task = Task.Run(async () =>
                    {
                        return await ApplicationData.Current.LocalFolder.CreateFileAsync(path, collisionOption);
                    });
                var file = file_task.Result;

                using (Stream stream = file.OpenStreamForWriteAsync().Result)
                {
                    data_in_stream.CopyToAsync(stream);
                }
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
        /// <param name="collisionOption">What to do if the file already exists.</param>
        public async static Task SerializeToFileAsync(object data, string path, CreationCollisionOption collisionOption = CreationCollisionOption.FailIfExists)
        {
            Validation.Argument.Assert.IsNotNull(data, "data");
            Validation.Argument.Assert.IsNotNullOrEmpty(path, "path");

            using (MemoryStream data_in_stream = new MemoryStream())
            {
                ToStream(data, data_in_stream);

                // Get an output stream for the SessionState file and write the state asynchronously
                StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(path, collisionOption);
                using (Stream stream = await file.OpenStreamForWriteAsync())
                {
                    await data_in_stream.CopyToAsync(stream);
                    await stream.FlushAsync();
                }
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
            Validation.Argument.Assert.IsNotNullOrEmpty(path, "path");

            var file_task = Task.Run(async () =>
                {
                    return await ApplicationData.Current.LocalFolder.GetFileAsync(path);
                });
            var file = file_task.Result;

            var read_task = Task.Run(async () =>
                {
                    return await file.OpenSequentialReadAsync();
                });

            using (IInputStream stream = read_task.Result)
            {
                var file_stream = stream.AsStreamForRead();
                return Deserialize<T>(file_stream);
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
        public async static Task<T> DeserializeFromFileAsync<T>(string path)
        {
            Validation.Argument.Assert.IsNotNullOrEmpty(path, "path");

            StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync(path);
            using (IInputStream stream = await file.OpenSequentialReadAsync())
            {
                var file_stream = stream.AsStreamForRead();
                return Deserialize<T>(file_stream);
            }
        }
#else

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
            Validation.Argument.Assert.IsNotNull(data, "data");
            Validation.Argument.Assert.IsNotNullOrEmpty(path, "path");

            using (Stream stream = File.Open(path, fileMode, fileAccess, fileShare))
            {
                ToStream(data, stream);
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
        public async static Task SerializeToFileAsync(object data, string path, FileMode fileMode = FileMode.CreateNew, FileAccess fileAccess = FileAccess.Write, FileShare fileShare = FileShare.None)
        {
            Validation.Argument.Assert.IsNotNull(data, "data");
            Validation.Argument.Assert.IsNotNullOrEmpty(path, "path");

            await Task.Run(() => 
            {
                using (Stream stream = File.Open(path, fileMode, fileAccess, fileShare))
                {
                    ToStream(data, stream);
                }
            });
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
            Validation.Argument.Assert.IsNotNullOrEmpty(path, "path");

            using (var stream = File.OpenRead(path))
            {
                var reader = GetSerializer(typeof(T));
                return (T)reader.ReadObject(stream);
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
        public async static Task<T> DeserializeFromFileAsync<T>(string path)
        {
            Validation.Argument.Assert.IsNotNullOrEmpty(path, "path");

            return await Task.Run(() =>
            {
                using (var stream = File.OpenRead(path))
                {
                    var reader = GetSerializer(typeof(T));
                    return (T)reader.ReadObject(stream);
                }
            });
        }
#endif

#if WINDOWS_PHONE
        /// <summary>
        /// Deserialize object from isolated storage with the DataContractSerializer.
        /// </summary>
        /// <typeparam name="T">Type of object being deserialized.</typeparam>
        /// <param name="stream">IsolatedStorageFileStream object.</param>
        /// <returns>Object of type T.</returns>
        public static T Deserialize<T>(System.IO.IsolatedStorage.IsolatedStorageFileStream stream)
        {
            Validation.Argument.Assert.IsNotNull(stream, "stream");

            var deserializer = GetSerializer(typeof(T));
            return (T)deserializer.ReadObject(stream);
        }
#endif
        #endregion
    }
}
