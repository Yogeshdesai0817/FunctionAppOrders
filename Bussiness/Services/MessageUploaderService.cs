using Bussiness.Interfaces;
using Data.Interfaces;
using Microsoft.Extensions.Logging;

namespace Bussiness.Services
{
    public class MessageUploaderService : IMessageUploaderService
    {
        private readonly IUploader _uploader;
        private readonly ILogger<MessageUploaderService> _logger;
        public MessageUploaderService(IUploader uploader, ILogger<MessageUploaderService> logger)
        {
            _uploader = uploader;
            _logger = logger;
        }

        /// <summary>
        /// Method uploads message received from source.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<bool> UpLoadMessageAsync(string message)
        {
            try
            {
                var fileName = $"{Guid.NewGuid()}.json";
                return await _uploader.UploadDataAsync(fileName, message);               
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while uploading order message to Azure Blob Storage {ex}", ex.ToString());
                return false;
            }
        }
    }
}
