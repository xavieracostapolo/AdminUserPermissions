using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Xacosta.AdminPermissions.Domain.ContractsInfraestructure;

namespace Xacosta.AdminPermissions.Application.Events
{
    public class KafkaEventHandler(
        ILogger<KafkaEventHandler> _logger,
        IKafkaService _kafkaService
        ) : INotificationHandler<KafkaEvent>
    {
        public async Task Handle(KafkaEvent notification, CancellationToken cancellationToken)
        {
            string message = JsonSerializer.Serialize(notification);
            _logger.LogInformation($"Publicando en Kafka {message}");

            if (await _kafkaService.Send(message))
            {
                // delivery might have failed after retries. This message requires manual processing.
                _logger.LogWarning($"ERROR: Message not ack'd by all brokers (value: '{message}').");                
            }
            else
            {
                _logger.LogInformation($"Message sent (value: '{message}').");
            }            
        }
    }
}
