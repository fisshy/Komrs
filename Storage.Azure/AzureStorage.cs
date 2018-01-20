using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading;

namespace Storage.Azure
{
    public class AzureStorage : IStorage
    {
        private readonly string _storageConnectionString;

        public AzureStorage(string storageConnectionString)
        {
            _storageConnectionString = !string.IsNullOrWhiteSpace(storageConnectionString) ? storageConnectionString : throw new ArgumentNullException();
        }

        public Task<string> UploadFile(IFile file, CancellationToken cancellationToken)
        {
            if (file == null)
            {
                throw new ArgumentNullException();
            }

            return Task.FromResult(string.Empty);
        }

        public async Task<IEnumerable<string>> UploadFiles(IEnumerable<IFile> files, CancellationToken cancellationToken)
        {
            if (files == null)
            {
                throw new ArgumentNullException();
            }

            return await Task.WhenAll(files.Select(f => UploadFile(f, cancellationToken)));
        }

        public async Task<string> UploadImage(IFile file, CancellationToken cancellationToken)
        {
            if (file == null)
            {
                throw new ArgumentNullException();
            }

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_storageConnectionString);

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference("komrs");

            await container.CreateIfNotExistsAsync();

            await container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            CloudBlockBlob blob = container.GetBlockBlobReference(file.FileName);

            await blob.UploadFromStreamAsync(file.Stream);

            return blob.Uri.AbsolutePath;
        }

        public async Task<IEnumerable<string>> UploadImages(IEnumerable<IFile> files, CancellationToken cancellationToken)
        {
            if (files == null)
            {
                throw new ArgumentNullException();
            }

            return await Task.WhenAll(files.Select(f => UploadImage(f, cancellationToken)));
        }
    }
}
