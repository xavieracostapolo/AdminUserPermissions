using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Xacosta.AdminPermissions.Application.Events
{
    public class ElasticSearchEventHandler(ILogger<ElasticSearchEventHandler> _logger) : INotificationHandler<ElasticSearchEvent>
    {
        public Task Handle(ElasticSearchEvent notification, CancellationToken cancellationToken)
        {            
            string jsonString = JsonSerializer.Serialize(notification);
            _logger.LogInformation($"Confirmation {jsonString}");

            return Task.CompletedTask;
        }
    }
}
