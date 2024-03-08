using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Store.Infrastructure.Data;
using Store.Infrastructure.Entities.OrderAgrgregate;

namespace Store.Ordering.Service.Consumers
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;

        private readonly StoreContext _context;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, StoreContext context)
        {
            _logger = logger;
            _configuration = configuration;
            _context = context;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = _configuration["Kafka:BootstrapServers"],
                GroupId = "orderStatusConsumerGroup",
                ClientId = Guid.NewGuid().ToString(),
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
            consumer.Subscribe("OrderStatusChangedTopic");

            while (!stoppingToken.IsCancellationRequested)
            {
                var consumedData = consumer.Consume(TimeSpan.FromSeconds(3));

                if (consumedData is not null)
                {
                    try
                    {
                        var orderJson = consumedData.Message.Value;
                        Order order = JsonConvert.DeserializeObject<Order>(orderJson);
                        _context.Entry(order).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                        _logger.LogInformation($"Consuming {order}");
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
