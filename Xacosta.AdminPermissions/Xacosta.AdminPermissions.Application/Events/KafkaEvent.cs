using MediatR;

namespace Xacosta.AdminPermissions.Application.Events
{
    public class KafkaEvent : INotification
    {
        public string? Id { get; set; }
        public string? NameOperation { get; set; }
    }
}
