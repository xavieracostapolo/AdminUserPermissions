using MediatR;
using Xacosta.AdminPermissions.Domain.Models;

namespace Xacosta.AdminPermissions.Application.Events
{
    public class ElasticSearchEvent : INotification
    {
        public string? Accion { get; set; }
        public Permission? Model { get; set; }
    }
}
