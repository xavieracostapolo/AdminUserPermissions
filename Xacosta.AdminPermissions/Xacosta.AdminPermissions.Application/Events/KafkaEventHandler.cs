using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Xacosta.AdminPermissions.Application.Events
{
    public class KafkaEventHandler(ILogger<KafkaEventHandler> _logger) : INotificationHandler<KafkaEvent>
    {
        public Task Handle(KafkaEvent notification, CancellationToken cancellationToken)
        {
            string jsonString = JsonSerializer.Serialize(notification);
            _logger.LogInformation($"Confirmation {jsonString}");

            return Task.CompletedTask;
        }
    }
}
