using API.Entities;
using Confluent.Kafka;
using Newtonsoft.Json;

namespace API.Integrations.Services.Kafka
{
    // Todo: Move ConsumerServices to a different project because this is not an API
    public class ConsumerService : BackgroundService
    {
        private readonly IConsumer<Ignore, string> _consumer;

        private readonly ILogger<ConsumerService> _logger;

        public ConsumerService(IConfiguration configuration, ILogger<ConsumerService> logger)
        {
            _logger = logger;

            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = configuration["Kafka:BootstrapServers"],
                GroupId = "InventoryConsumerGroup",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumer.Subscribe("AddInventory");

            while (!stoppingToken.IsCancellationRequested)
            {
                ProcessKafkaMessage(stoppingToken);

                Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }

            _consumer.Close();
        }

        public void ProcessKafkaMessage(CancellationToken stoppingToken)
        {
            try
            {
                //while (true) {
                    var consumeResult = _consumer.Consume(stoppingToken);
                    if (consumeResult.Message != null)
                    {
                        Inventory message = JsonConvert.DeserializeObject<Inventory>(consumeResult.Message.Value);

                        _logger.LogInformation($"Received inventory update: {message}");
                    }
                //}
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error processing Kafka message: {ex.Message}");
            }
        }
    }
}