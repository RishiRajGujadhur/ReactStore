using Confluent.Kafka; 

namespace Store.Notification.Service.Consumers
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration; 

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration; 
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumerConfig = new ConsumerConfig
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
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        _logger.LogError(ex.StackTrace);
                    }
                }
                else
                {
                    _logger.LogInformation("Nothing found to consume");
                }
            }
        }
    }
}
