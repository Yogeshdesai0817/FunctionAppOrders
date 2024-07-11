using Bussiness.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionAppOrders.Functions
{
    public class MessageSender
    {
        private readonly ILogger<MessageSender> _logger;
        private readonly IMessageSenderService _messageSender;

        public MessageSender(ILogger<MessageSender> logger, IMessageSenderService messageSender)
        {
            _logger = logger;
            _messageSender = messageSender;
        }

        /// <summary>
        /// Funtion generate message and upload json file to Azure blob storage
        /// </summary>
        /// <param name="myTimer"></param>
        /// <returns></returns>
        
        [Function("MessageSenderFunction")]
        public async Task Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation("Generating and sending message started");
            var messageCount = Environment.GetEnvironmentVariable("MessageCountServiceBus");
            int count = 2;
            if (string.IsNullOrEmpty(messageCount))
            {
                _logger.LogInformation("No message generation count set, default count of 2 will be applied");
            }
            else
            {
               int.TryParse(messageCount, out count);
            }
            await _messageSender.SendMessageAsync(count);
        }
    }
}
