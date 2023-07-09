using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;

namespace WiredBrainCoffee.Storage
{
    public class CoffeeVideoStorage : ICoffeeVideoStorage
    {
        private readonly string _containerNameVideos = "coffeevideos";
        private readonly string _connectionString;

        public CoffeeVideoStorage(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task UploadVideoAsync(byte[] videoByteArray, string blobName)
        {
            var cloudStorageAccount = CloudStorageAccount.Parse(_connectionString);

            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            var cloudBlobContainer = cloudBlobClient.GetContainerReference(_containerNameVideos);
            //Blob grants public read access to our blob
            await cloudBlobContainer.CreateIfNotExistsAsync(
                BlobContainerPublicAccessType.Blob, null, null);

            var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(blobName);

            await cloudBlockBlob.UploadFromByteArrayAsync(videoByteArray, 0, videoByteArray.Length);

        }

        public async Task<bool> CheckIfBlobExistsAsync(string blobName)
        {
            // TODO: Check if the blob exists in Blob Storage
            return false;
        }
    }
}
