using Confluent.Kafka;

namespace API.Integrations.Services.Kafka
{
    public class ProducerService
    {
        private readonly IConfiguration _configuration;

        private readonly IProducer<Null, string> _producer;

        public ProducerService(IConfiguration configuration)
        {
            _configuration = configuration;

            var producerconfig = new ProducerConfig
            {
                BootstrapServers = _configuration["Kafka:BootstrapServers"],
                Acks = Acks.All
            };

            _producer = new ProducerBuilder<Null, string>(producerconfig).Build();
        }

        public async Task ProduceAsync(string topic, string message)
        {
            
            var kafkaMessage = new Message<Null, string> { Value = message, };

            await _producer.ProduceAsync(topic, kafkaMessage);
            
        }

        public void Flush(TimeSpan timeout) => this._producer.Flush(timeout);

    }
}
