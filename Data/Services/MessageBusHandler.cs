using Azure.Messaging.ServiceBus;
using Data.Interfaces;
using Microsoft.Extensions.Logging;


namespace Data.Services
{
    public class MessageBusHandler : IMessageSender
    {
        private readonly string serviceBusConnectionString;
        private readonly string queueName;
        private readonly ServiceBusClient client;
        private readonly ServiceBusSender sender;
        private readonly ILogger<MessageBusHandler> _logger;
        public MessageBusHandler(ILogger<MessageBusHandler> logger)
        {
            _logger = logger;
            serviceBusConnectionString = Environment.GetEnvironmentVariable("ServiceBusConnectionString");
            if (string.IsNullOrEmpty(serviceBusConnectionString))
            {
                _logger.LogError("Empty service bus connection string please check configuration");
                throw new ArgumentNullException("ServiceBus ConnectionString is empty");
            }
            queueName = Environment.GetEnvironmentVariable("QueueName");
            client = new ServiceBusClient(serviceBusConnectionString);
            if (client != null)
            {
                sender = client.CreateSender(queueName);
            }
        }

        /// <summary>
        /// Method sends message to Queue
        /// </summary>
        /// <param name="messagebody"></param>
        /// <returns></returns>
        public async Task<bool> SendQueueMessageAsync(string messagebody)
        {
            try
            {
                ServiceBusMessage message = new ServiceBusMessage(messagebody);
                await sender.SendMessageAsync(message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while sending message to service bus {ex}", ex.ToString());
                return false;
            }
            return true;
        }
    }
}
