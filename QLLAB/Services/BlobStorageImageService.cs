using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using QLLAB.Models;
using QLLAB.Services.Interfaces;
using Microsoft.WindowsAzure.Storage.Blob;

namespace QLLAB.Services
{
    public class BlobStorageImageService : IBlobStorageImageService
    {
        private readonly string _connectionString;
        private readonly string _blobStorageContainer;

        public BlobStorageImageService(string connectionString, string containerName)
        {
            _connectionString = connectionString;
            _blobStorageContainer = containerName;
        }

        public void Save(string base64, string originalFilename)
        {
            var container = GetContainer();
            var imageBytes = Convert.FromBase64String(base64);
            var blob = container.GetBlockBlobReference(originalFilename);
            var extension = Path.GetExtension(originalFilename);
            blob.Properties.ContentType = $"img/{extension}";

            using (var stream = new MemoryStream(imageBytes, false))
            {
                blob.UploadFromStreamAsync(stream);
            }
        }

        public async Task<Content> SaveAsync(Content image)
        {
            var container = GetContainer();
            var imageBytes = Convert.FromBase64String(image.Data);
            var blobName = Guid.NewGuid().ToString();
            var blob = container.GetBlockBlobReference(blobName);
            var extension = Path.GetExtension(image.Filename);
            blob.Properties.ContentType = $"img/{extension}";

            using (var stream = new MemoryStream(imageBytes, false))
            {
                await blob.UploadFromStreamAsync(stream);
            }

            return await Task.Run(() => image);
        }

        public async Task<Content> GetAsync(Guid id)
        {
            var container = GetContainer();
            var blob = container.GetBlockBlobReference(id.ToString());

            if (blob == null)
                return null;

            await blob.FetchAttributesAsync();
            var arr = new byte[blob.Properties.Length];
            await blob.DownloadToByteArrayAsync(arr, 0);

            var base64Str = Convert.ToBase64String(arr);

            var image = new Content
            {
                Data = base64Str,
                Filename = $"{id}.{blob.Properties.ContentType}"
            };

            return image;
        }

        private CloudBlobContainer GetContainer()
        {
            var blobStorageAccount = CloudStorageAccount.Parse(_connectionString);
            var blobClient = blobStorageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(_blobStorageContainer);
            container.CreateIfNotExistsAsync();
            return container;
        }
    }
}
