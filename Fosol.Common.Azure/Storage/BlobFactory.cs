using Fosol.Common.Extensions.Strings;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Azure.Storage
{
    /// <summary>
    /// Provides a one stop easy solution to interact with Azure Storage Blob services.
    /// </summary>
    public class BlobFactory
    {
        #region Variables
        #endregion

        #region Properties
        /// <summary>
        /// get/set - The Account name to your Azure Storage services.
        /// </summary>
        public string Account { get; protected set; }

        /// <summary>
        /// get - Azure Storage key
        /// </summary>
        public string Key { get; protected set; }

        /// <summary>
        /// get - The CloudBlobClient that will be used to interact with Azure Storage.
        /// </summary>
        public CloudBlobClient Client { get; protected set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new BlobFactory object.
        /// If the account value is "local" it will create a client that connects to the Development Storage Account.
        /// 
        /// AppSetting Keys:
        ///     AzureAccount
        ///     AzureKey
        /// </summary>
        /// <exception cref="System.Configuration.ConfigurationException">If AppSetting AzureAccount and AzureKey values are empty.</exception>
        /// <param name="account">Azure Storage Blob account name.</param>
        /// <param name="key">Azure Storage key.</param>
        public BlobFactory(string account, string key)
        {
            this.Account = account;
            this.Key = key;

            Initialize();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Ensure BlobFactory object has been correctly initialized.
        /// If the AppSetting AzureAccount value is "local" it will create a client that connects to the Development Storage Account.
        /// </summary>
        /// <exception cref="System.Configuration.ConfigurationException">If AppSetting AzureAccount and AzureKey values are empty.</exception>
        protected void Initialize()
        {
            // If the account name is "local" it will create a client that connects to the Development Storage Account.
            if (this.Account.ToLower() == "local")
            {
                CloudStorageAccount account = CloudStorageAccount.DevelopmentStorageAccount;
                this.Client = account.CreateCloudBlobClient();
                return;
            }

            if (string.IsNullOrEmpty(this.Account))
                throw new System.Configuration.ConfigurationErrorsException("AppSetting 'AzureAccount' key cannot be empty");

            if (string.IsNullOrEmpty(this.Key))
                throw new System.Configuration.ConfigurationErrorsException("AppSetting 'AzureKey' key cannot be empty");

            this.Client = this.CreateCloudBlobClient();
        }

        /// <summary>
        /// Creates the CloudBlobClient that will be used to interact with Azure Storage.
        /// </summary>
        /// <returns>CloudBlobClient object.</returns>
        protected CloudBlobClient CreateCloudBlobClient()
        {
            var sc = new StorageCredentials(this.Account, this.Key);
            var csa = new CloudStorageAccount(sc, false);
            return csa.CreateCloudBlobClient();
        }

        /// <summary>
        /// Creates a CloudBlobContainer within Azure Storage.
        /// </summary>
        /// <param name="containerName">Name of the container to create.</param>
        /// <returns>CloudBlobContainer object just created.</returns>
        public CloudBlobContainer CreateContainer(string containerName)
        {
            CloudBlobContainer container = this.Client.GetContainerReference(containerName);
            container.Create();

            return container;
        }

        /// <summary>
        /// Creates a CloudBlobContainer within Azure Storage if it doesn't already exist.
        /// </summary>
        /// <param name="containerName">Name of the container to create.</param>
        /// <returns>CloudBlobContainer object just created.</returns>
        public CloudBlobContainer CreateIfNotExistContainer(string containerName)
        {
            CloudBlobContainer container = this.Client.GetContainerReference(containerName);
            container.CreateIfNotExists();

            return container;
        }

        /// <summary>
        /// Deletes a CloudBlobContainer within Azure Storage.
        /// </summary>
        /// <param name="containerName">Name of the container to delete.</param>
        public void DeleteContainer(string containerName)
        {
            CloudBlobContainer container = this.Client.GetContainerReference(containerName);
            container.Delete();
        }

        /// <summary>
        /// Uploads the byte array into Azure Storage as a CloudPageBlob.
        /// </summary>
        /// <param name="containerName">URL or name of the storage container.</param>
        /// <param name="blobName">URI or name of the blob.</param>
        /// <param name="data">Byte array of data to upload to storage.</param>
        public void UploadByteArray(string containerName, string blobName, byte[] data, string contentType = null, IEnumerable<KeyValuePair<string, string>> metadata = null)
        {
            var container = this.GetContainerReference(containerName);
            var blob = container.GetBlockBlobReference(blobName);

            using (var stream = new MemoryStream(data))
            {
                blob.UploadFromStream(stream);
            }

            if (!string.IsNullOrEmpty(contentType))
            {
                blob.Properties.ContentType = contentType;
                blob.SetProperties();
            }

            if (metadata != null && metadata.Count() > 0)
            {
                foreach (var meta in metadata)
                    blob.Metadata.Add(meta);
                blob.SetMetadata();
            }
        }

        /// <summary>
        /// Uploads text into Azure Storage as a CloudPageBlob.
        /// </summary>
        /// <param name="containerName">URL or name of the storage container.</param>
        /// <param name="blobName">URI or name of the blob.</param>
        /// <param name="data">Text to upload to storage.</param>
        public void UploadByteArray(string containerName, string blobName, string data, string contentType = null, IEnumerable<KeyValuePair<string, string>> metadata = null)
        {
            var blob = this.GetContainerReference(containerName).GetBlockBlobReference(blobName);

            using (var stream = new MemoryStream(data.ToByteArray()))
                blob.UploadFromStream(stream);

            if (!string.IsNullOrEmpty(contentType))
            {
                blob.Properties.ContentType = contentType;
                blob.SetProperties();
            }

            if (metadata != null && metadata.Count() > 0)
            {
                foreach (var meta in metadata)
                    blob.Metadata.Add(meta);
                blob.SetMetadata();
            }
        }

        /// <summary>
        /// Returns the CloudBlobContainer for the specified container URL.
        /// </summary>
        /// <param name="containerName">URL or name of the storage container.</param>
        /// <returns>CloudBlobContainer object.</returns>
        public CloudBlobContainer GetContainerReference(string containerName)
        {
            return this.Client.GetContainerReference(containerName);
        }

        /// <summary>
        /// Returns the CloudBlob for the specified container and blob name.
        /// </summary>
        /// <param name="containerName">URL or name of the container.</param>
        /// <param name="blobName">URI or name of the blob.</param>
        /// <returns>CloudBlob object.</returns>
        public ICloudBlob GetBlobReference(string containerName, string blobName)
        {
            var blob = this.GetContainerReference(containerName).GetBlobReferenceFromServer(blobName);
            return blob;
        }

        /// <summary>
        /// Does a head request for the blob. If it is found it will return true.
        /// </summary>
        /// <param name="containerName">URL or name of the container.</param>
        /// <param name="blobName">URI or name of the blob.</param>
        /// <returns>True if blob exists.</returns>
        public bool BlobExists(string containerName, string blobName)
        {
            return BlobExists(this.GetBlobReference(containerName, blobName));
        }

        /// <summary>
        /// Makes a HEAD request for the blob attributes to determine if the blob exists.
        /// </summary>
        /// <param name="blob">CloudBlob reference.</param>
        /// <param name="options">BlobRequestOptions reference.</param>
        /// <returns>True if the blob exists.</returns>
        public static bool BlobExists(ICloudBlob blob, BlobRequestOptions options = null)
        {
            try
            {
                if (options == null)
                    blob.FetchAttributes();
                else
                    blob.FetchAttributes(null, options);

                return true;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null
                    && ex.InnerException is System.Net.WebException
                    && ((System.Net.HttpWebResponse)((System.Net.WebException)ex.InnerException).Response).StatusCode == System.Net.HttpStatusCode.NotFound)
                    return false;
                else
                    throw;
            }
        }

        /// <summary>
        /// Makes a HEAD request for the blob attributes to determine if the blob exists.
        /// </summary>
        /// <param name="blob">CloudPageBlob reference.</param>
        /// <param name="options">BlobRequestOptions reference.</param>
        /// <returns>True if the blob exists.</returns>
        public static bool BlobExists(CloudPageBlob blob, BlobRequestOptions options = null)
        {
            try
            {
                if (options == null)
                    blob.FetchAttributes();
                else
                    blob.FetchAttributes(null, options);

                return true;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null
                    && ex.InnerException is System.Net.WebException
                    && ((System.Net.HttpWebResponse)((System.Net.WebException)ex.InnerException).Response).StatusCode == System.Net.HttpStatusCode.NotFound)
                    return false;
                else
                    throw;
            }
        }

        /// <summary>
        /// Downloads the blob from storage and returns a byte array.
        /// </summary>
        /// <param name="containerName">URL or name of the container.</param>
        /// <param name="blobName">URI or name of the blob.</param>
        /// <returns>The blob from storage as a byte array.</returns>
        public byte[] DownloadByteArray(string containerName, string blobName)
        {
            using (var stream = new MemoryStream())
            {
                this.GetBlobReference(containerName, blobName).DownloadToStream(stream);

                if (stream != null && stream.Length > 0)
                {
                    stream.Position = 0;
                    return stream.ToArray();
                }

                return null;
            }
        }

        /// <summary>
        /// Downloads the blob from storage and returns a string.
        /// </summary>
        /// <param name="containerName">URL or name of the container.</param>
        /// <param name="blobName">URI or name of the blob.</param>
        /// <returns>The blob from storage as a string.</returns>
        public string DownloadText(string containerName, string blobName)
        {
            var data = this.DownloadByteArray(containerName, blobName);

            if (data != null)
                return Encoding.UTF8.GetString(data);

            return null;
        }
        #endregion

        #region Events
        #endregion
    }
}
