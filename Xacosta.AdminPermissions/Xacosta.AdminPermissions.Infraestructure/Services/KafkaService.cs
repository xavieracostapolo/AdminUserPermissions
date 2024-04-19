using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using System.Net;
using Xacosta.AdminPermissions.Domain.ContractsInfraestructure;

namespace Xacosta.AdminPermissions.Infraestructure.Services
{
    public class KafkaService : IKafkaService
    {
        private readonly IProducer<long, string> _producerBuilder;
        private readonly ILogger<KafkaService> _logger;

        public KafkaService(ILogger<KafkaService> logger)
        {
            _logger = logger;
            var _producerConfig = new ProducerConfig
            {
                BootstrapServers = "138.197.116.22:9092",
                EnableDeliveryReports = true,
                ClientId = Dns.GetHostName(),
                // Emit debug logs for message writer process, remove this setting in production
                Debug = "msg",

                // retry settings:
                // Receive acknowledgement from all sync replicas
                Acks = Acks.All,
                // Number of times to retry before giving up
                MessageSendMaxRetries = 3,
                // Duration to retry before next attempt
                RetryBackoffMs = 1000,
                // Set to true if you don't want to reorder messages on retry
                EnableIdempotence = true
            };

            _producerBuilder = new ProducerBuilder<long, string>(_producerConfig)
            .SetKeySerializer(Serializers.Int64)
            .SetValueSerializer(Serializers.Utf8)
            .SetLogHandler((_, message) =>
                Console.WriteLine($"Facility: {message.Facility}-{message.Level} Message: {message.Message}"))
            .SetErrorHandler((_, e) => Console.WriteLine($"Error: {e.Reason}. Is Fatal: {e.IsFatal}"))
            .Build();
        }

        public async Task<bool> Send(string message)
        {
            try
            {
                var topic = "operation-permission";
                var deliveryReport = await _producerBuilder.ProduceAsync(topic,
                new Message<long, string>
                {
                    Key = DateTime.UtcNow.Ticks,
                    Value = message
                });

                _logger.LogInformation($"Message sent (value: '{message}'). Delivery status: {deliveryReport.Status}");

                return deliveryReport.Status != PersistenceStatus.Persisted;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }                     
        }
    }
}
