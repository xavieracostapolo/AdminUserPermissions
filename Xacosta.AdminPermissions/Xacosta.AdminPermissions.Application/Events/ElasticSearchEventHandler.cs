using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Xacosta.AdminPermissions.Domain.ContractsInfraestructure;

namespace Xacosta.AdminPermissions.Application.Events
{
    public class ElasticSearchEventHandler(
        IElasticService _elasticService,
        ILogger<ElasticSearchEventHandler> _logger) : INotificationHandler<ElasticSearchEvent>
    {
        public async Task Handle(ElasticSearchEvent notification, CancellationToken cancellationToken)
        {            
            string jsonString = JsonSerializer.Serialize(notification);
            _logger.LogInformation($"Confirmation {jsonString}");

            if (await _elasticService.Send(notification.Model))
            {
                _logger.LogInformation($"Index document with ID {notification.Model.Id} succeeded.");
            }
            else
            {
                _logger.LogWarning($"Index document with ID {notification.Model.Id} succeeded.");
            }
        }
    }
}
