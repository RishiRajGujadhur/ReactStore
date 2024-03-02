using System.Text.Json;
using Confluent.Kafka;

namespace InventoryService.Consumers
{
    public class AddInventoryConsumer : BackgroundService
    {
        private readonly IConsumer<Ignore, string> _consumer;

        private readonly ILogger<AddInventoryConsumer> _logger;

        public AddInventoryConsumer(IConfiguration configuration, ILogger<AddInventoryConsumer> logger)
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

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
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
                    var message = consumeResult.Message.Value;

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

    // Temp for testing - todo: Create seperate Data project which other projects can reference for models and db requests
    public class Inventory
    {
        public int InventoryID { get; set; }
        // Foreign Keys
        public int ProductID { get; set; }
        public int WarehouseID { get; set; }
        public int StockQuantity { get; set; }
    }
}
