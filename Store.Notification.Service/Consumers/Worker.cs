using Confluent.Kafka;
using Microsoft.AspNetCore.SignalR; 
using System.Text.Json; 

namespace Store.Notification.Service.Consumers
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHubContext<NotificationHub> _hubContext;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, IHubContext<NotificationHub> hubContext)
        {
            _logger = logger;
            _configuration = configuration;
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumerConfig = new ConsumerConfig()
            {
                BootstrapServers = _configuration["Kafka:BootstrapServers"],
                GroupId = "invoiceConsumerGroup",
                ClientId = Guid.NewGuid().ToString(),
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
            consumer.Subscribe("OrderNotificationTopic");
            while (!stoppingToken.IsCancellationRequested)
            {
                var consumedData = consumer.Consume(TimeSpan.FromSeconds(3));
                if (consumedData is not null)
                {
                    try
                    {
                        var notification = consumedData.Message.Value;
                        _logger.LogInformation($"Consuming {notification}");
                        await _hubContext.Clients.All.SendAsync("ReceiveMessage", "Hello World", cancellationToken: stoppingToken);
                        //await Task.Delay(4000, stoppingToken);
                    }
                    catch (Exception Ex)
                    {
                        Console.WriteLine(Ex.Message);
                        _logger.LogError(Ex.StackTrace);
                    }
                }
                else
                    _logger.LogInformation("Nothing found to consume");
                // await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
