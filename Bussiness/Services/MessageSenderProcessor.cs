using Bussiness.Interfaces;
using Data.Interfaces;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using System.Runtime.Serialization;

namespace Bussiness.Services
{
    public class MessageSenderProcessor : IMessageSenderService
    {
        private static readonly Random random = new Random();
        private static readonly string[] categories = { "Electronics", "Clothing", "Groceries", "Utilities", "Miscellaneous" };
        private readonly IMessageSender _messageSender;
        private readonly ILogger<MessageSenderProcessor> _logger;

        public MessageSenderProcessor(ILogger<MessageSenderProcessor> logger, IMessageSender messageSender)
        {
            _messageSender = messageSender;
            _logger = logger;
        }
        public async Task<bool> SendMessageAsync(int messagecount)
        {
            for (int i = 0; i < messagecount; i++)
            {
                var transaction = new
                {
                    transactionId = Guid.NewGuid().ToString(),
                    timestamp = DateTime.UtcNow.ToString("o"),
                    amount = (random.NextDouble() * 1000).ToString("F2"),
                    description = "Random transaction description",
                    category = categories[random.Next(categories.Length)]
                };

                try
                {
                    string messageBody = JsonSerializer.Serialize(transaction, new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });
                    var isSuccess = await _messageSender.SendQueueMessageAsync(messageBody);
                    if (isSuccess)
                    {
                        _logger.LogInformation("Message with transaction id {transactionId} is successfully sent to Azure Service Bus queue", transaction.transactionId);
                    }
                    else
                    {
                       _logger.LogError("Issue while sending message with transaction id {transactionId} to Azure Service Bus queue", transaction.transactionId);
                    }
                }
                catch (Exception ex)
                {
                   _logger.LogError("Issue occurred while sending message to Azure Service Bus, please see log for more details {ex}", ex.ToString());
                    return false;
                }
            }
            return true;
        }
    }
}
