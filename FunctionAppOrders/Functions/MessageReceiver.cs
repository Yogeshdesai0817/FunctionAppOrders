using Bussiness.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionAppOrders.Functions
{
    public class MessageReceiver
    {
        private readonly ILogger<MessageReceiver> _logger;
        private readonly IMessageUploaderService _uploader;

        public MessageReceiver(ILogger<MessageReceiver> logger, IMessageUploaderService uploader)
        {
            _logger = logger;
            _uploader = uploader;
        }

        /// <summary>
        /// Function reads message from order queue and upload to blob storage
        /// </summary>
        /// <param name="myQueueItem"></param>
        /// <returns></returns>
        [Function("MessageReceiverFunction")]
        public async Task Run([ServiceBusTrigger("orderqueue", Connection = "ServiceBusConnectionString")] string myQueueItem)
        {
            _logger.LogInformation("New message received in queue {myQueueItem}", myQueueItem);
            try
            {
                var isSuccess = await _uploader.UpLoadMessageAsync(myQueueItem);
                if (isSuccess)
                {
                    _logger.LogInformation("New message was uploaded to Azure blob storage");
                }
                else
                {
                    _logger.LogError("Issue while uploading message to Azure blob storage received, please check log for more details");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in uploading Order message to Azure Blob Storage {ex}", ex.ToString());
            }
        }
    }
}
