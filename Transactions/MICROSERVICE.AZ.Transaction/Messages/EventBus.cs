using Azure.Messaging.ServiceBus;
using System.Text.Json;

namespace MICROSERVICE.AZ.Transaction.Messages
{
    public class EventBus : IEventBus
    {
        private readonly IConfiguration _configuration;
        public EventBus(IConfiguration configuration) => _configuration = configuration;

        public async Task<bool> PublishMessage(object request)
        {
            string data = JsonSerializer.Serialize(request);
            var client = new ServiceBusClient(_configuration["CONFIG_CN_SERVICE_BUS"]);
            var sender = client.CreateSender(_configuration["CONFIG_TOPIC_TRANSACTION_SERVICE_BUS"]);
            using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();
            messageBatch.TryAddMessage(new ServiceBusMessage(data));
            await sender.SendMessagesAsync(messageBatch);
            await sender.DisposeAsync();
            await client.DisposeAsync();
            return true;
        }
    }
}
