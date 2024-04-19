using Xacosta.AdminPermissions.Domain.Models;

namespace Xacosta.AdminPermissions.Application.Services
{
    public interface IPublisherBrokerService
    {
        void Publish(string accion, Permission model, CancellationToken cancellationToken);
    }
}