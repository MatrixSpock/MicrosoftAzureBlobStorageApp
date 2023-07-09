using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System.Threading.Tasks;

namespace WiredBrainCoffee.Storage
{
    public class CoffeeVideoStorage : ICoffeeVideoStorage
    {
        private readonly string _connectionString;
        private readonly string _containerNameVideos;

        public CoffeeVideoStorage(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task UploadVideoAsync(byte[] videoByteArray, string blobName)
        {
            var cloudStorageAccount = CloudStorageAccount.Parse(_connectionString);

            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            var cloudBlobContainter = cloudBlobClient.GetContainerReference(_containerNameVideos);

            await cloudBlobContainter.CreateIfNotExistsAsync(
                BlobContainerPublicAccessType.Blob, null, null);

            var cloudBlockBlob = cloudBlobContainter.GetBlockBlobReference(blobName);

            await cloudBlockBlob.UploadFromByteArrayAsync(videoByteArray, 0, videoByteArray.Length);

        }

        public async Task<bool> CheckIfBlobExistsAsync(string blobName)
        {
            // TODO: Check if the blob exists in Blob Storage
            return false;
        }
    }
}
