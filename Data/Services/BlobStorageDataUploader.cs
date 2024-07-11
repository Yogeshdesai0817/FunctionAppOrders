using Azure.Storage.Blobs;
using Data.Interfaces;
using Microsoft.Extensions.Logging;

namespace ProcessCompanyOrders.Services
{
    public class BlobStorageDataUploader : IUploader
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly ILogger<BlobStorageDataUploader> _logger;
        public BlobStorageDataUploader(ILogger<BlobStorageDataUploader> logger)
        {
            _logger = logger;
            var sasUrl = Environment.GetEnvironmentVariable("BlobServiceSasUrl");
            if (string.IsNullOrEmpty(sasUrl))
            {
                _logger.LogError("Empty BlobServiceSasUrl please check configuration");
                throw new ArgumentNullException("Blob Storage url is empty");
            }
            _blobServiceClient = new BlobServiceClient(new Uri(sasUrl));
        }

        /// <summary>
        /// Method upload data to Blob Storage
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> UploadDataAsync(string filename, string data)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("orders");
            var blobClient = containerClient.GetBlobClient(filename);
            try
            {
                using (var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(data)))
                {
                    await blobClient.UploadAsync(stream, overwrite: true);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while uploading data to blob storage, {ex}", ex.ToString());
                return false;
            }
            return true;
        }
    }
}
