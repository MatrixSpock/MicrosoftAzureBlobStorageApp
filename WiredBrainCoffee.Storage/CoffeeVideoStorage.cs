using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System.Threading.Tasks;

namespace WiredBrainCoffee.Storage
{
    public class CoffeeVideoStorage : ICoffeeVideoStorage
    {
        private readonly string _connectionString;
        private readonly string _containerNameVideos = "coffeevideos";
        public CoffeeVideoStorage(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<CloudBlockBlob> UploadVideoAsync(byte[] videoByteArray, string blobName)
        {
            var cloudBlobContainter = await GetCoffeeVideosContainerAsync();

            var cloudBlockBlob = cloudBlobContainter.GetBlockBlobReference(blobName);

            await cloudBlockBlob.UploadFromByteArrayAsync(videoByteArray, 0, videoByteArray.Length);

            return cloudBlockBlob;

        }
        public async Task<bool> CheckIfBlobExistsAsync(string blobName)
        {
            var cloudBlobContainter = await GetCoffeeVideosContainerAsync();

            var cloudBlockBlob = cloudBlobContainter.GetBlockBlobReference(blobName);

            return await cloudBlockBlob.ExistsAsync();
        }
        private async Task<CloudBlobContainer> GetCoffeeVideosContainerAsync()
        {
            var cloudStorageAccount = CloudStorageAccount.Parse(_connectionString);

            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            var cloudBlobContainter = cloudBlobClient.GetContainerReference(_containerNameVideos);
            await cloudBlobContainter.CreateIfNotExistsAsync(
                BlobContainerPublicAccessType.Blob, null, null);
            return cloudBlobContainter;
        }
    }
}
