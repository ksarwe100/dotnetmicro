using MICROSERVICES.AZ.Account.DTOs;
using MICROSERVICES.AZ.Account.Repositories;
using Azure.Messaging.ServiceBus;
using System.Text.Json;

namespace MICROSERVICES.AZ.Account.Listener
{

    public class TransactionManagerService : BackgroundService
    {
        private readonly ILogger<TransactionManagerService> _logger;
        private readonly ServiceBusClient _serviceBusClient;
        private readonly ServiceBusProcessor _processor;
        private readonly IServiceProvider _serviceProvider;

        public TransactionManagerService(IServiceProvider serviceProvider, ILogger<TransactionManagerService> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _serviceBusClient = new ServiceBusClient(configuration["CONFIG_CN_SERVICE_BUS"]);

            _processor = _serviceBusClient.CreateProcessor(configuration["CONFIG_TOPIC_TRANSACTION_SERVICE_BUS"],
                configuration["CONFIG_SUBSCRIPTION_ACCOUNT_SERVICE_BUS"],
                new ServiceBusProcessorOptions
                {
                    AutoCompleteMessages = false,
                    MaxConcurrentCalls = 1,
                });

            _processor.ProcessMessageAsync += MessageHandler;
            _processor.ProcessErrorAsync += ErrorHandler;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"TransactionManagerService is starting.");

            stoppingToken.Register(() =>
                _logger.LogInformation($" Transaction background task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Transaction task doing background work.");
                await _processor.StartProcessingAsync();
                await Task.Delay(15000, stoppingToken);
            }
            _logger.LogInformation($"Transaction background task is stopping...");
        }

        private async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string? body = args.Message.Body.ToString();
            TransactionResponse? transactionDTO = JsonSerializer.Deserialize<TransactionResponse>(body);
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                IAccountRepository accountRepository = scope.ServiceProvider.GetService<IAccountRepository>();
                await accountRepository.UpdateAccount(transactionDTO.AccountId, transactionDTO.Amount);
                await args.CompleteMessageAsync(args.Message);
            }
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }
    }
}
