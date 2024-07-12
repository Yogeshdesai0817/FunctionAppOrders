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
            int count;
            if (string.IsNullOrEmpty(messageCount))
            {
                _logger.LogInformation("No message generation count set, message sending is ignored. Please set parameter MessageCountServiceBus with message generation count");
                return;
            }
            int.TryParse(messageCount, out count);
            if (count == 0)
            {
                _logger.LogInformation("MessageCountServiceBus value cannot be 0, please set parameter MessageCountServiceBus with value greater then 0");
                return;
            }
            await _messageSender.SendMessageAsync(count);
        }
    }
}
