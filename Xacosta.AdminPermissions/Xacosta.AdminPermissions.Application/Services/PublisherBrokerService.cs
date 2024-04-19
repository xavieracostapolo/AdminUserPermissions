using MediatR;
using Xacosta.AdminPermissions.Application.Events;
using Xacosta.AdminPermissions.Domain.Models;

namespace Xacosta.AdminPermissions.Application.Services
{
    public class PublisherBrokerService(IPublisher _publisher) : IPublisherBrokerService
    {
        public void Publish(string accion, Permission model, CancellationToken cancellationToken)
        {
            _publisher.Publish(new ElasticSearchEvent()
            {
                Accion = accion,
                Model = model
            }, cancellationToken);

            _publisher.Publish(
                new KafkaEvent()
                {
                    Id = Guid.NewGuid().ToString(),
                    NameOperation = accion
                }, cancellationToken);
        }
    }
}
