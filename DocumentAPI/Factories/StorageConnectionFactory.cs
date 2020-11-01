using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentAPI.Factories
{
    public class StorageConnectionFactory:IStorageConnectionFactory
    {
        private CloudBlobClient _blobClient;
        private CloudBlobContainer _blobContainer;
        private readonly CloudStorageOptionsDTO _storageOptions;

        public StorageConnectionFactory(CloudStorageOptionsDTO storageOptions)
        {
            _storageOptions = storageOptions;

        }
        public async Task<CloudBlobContainer> GetContainer()
        {
            if (_blobContainer != null)
            {
                return _blobContainer;
            }
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_storageOptions.ConnectionString);

            _blobClient = storageAccount.CreateCloudBlobClient();
            _blobContainer = _blobClient.GetContainerReference(_storageOptions.DocumentsContainer);
            await _blobContainer.CreateIfNotExistsAsync();

            await _blobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            return _blobContainer;
        }
    }
}
