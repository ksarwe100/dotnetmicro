using System;
using System.Collections;
using System.Configuration;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using history_process_azf;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace history_process_azf
{
    public class history_process_azf
    {
        private readonly ILogger<history_process_azf> _logger;

        public history_process_azf(ILogger<history_process_azf> log)
        {
            _logger = log;
        }

        [FunctionName("history_process_azf")]
        public async Task Run(
            [ServiceBusTrigger("transaction-topic", "history-suscription", Connection = "SB_Connection")] string serviceBusMessage,
            [CosmosDB(
                databaseName: "DB_TRANSACTION_HISTORY",
                containerName: "transactionsCollection",
                Connection = "CosmosDBConnection")] IAsyncCollector<TransactionModel> documentsOut)
        {
            var transaction = JsonConvert.DeserializeObject<TransactionModel>(serviceBusMessage);
            await documentsOut.AddAsync(transaction);
            _logger.LogInformation($"C# ServiceBus topic trigger function processed message: {serviceBusMessage}");
        }
    }
}








