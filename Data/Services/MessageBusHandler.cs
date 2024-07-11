using Azure.Messaging.ServiceBus;
using Azure.Storage.Blobs;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services
{
    public class MessageBusHandler : IMessageSender
    {
        private readonly string serviceBusConnectionString;
        private readonly string queueName;
        private readonly ServiceBusClient client;
        private readonly ServiceBusSender sender;
        public MessageBusHandler()
        {
            serviceBusConnectionString = Environment.GetEnvironmentVariable("ServiceBusConnectionString");
            if (string.IsNullOrEmpty(serviceBusConnectionString))
            {
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
                return false;
            }
            return true;
        }
    }
}
