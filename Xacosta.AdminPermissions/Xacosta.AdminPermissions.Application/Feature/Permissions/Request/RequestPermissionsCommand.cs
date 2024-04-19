using MediatR;

namespace Xacosta.AdminPermissions.Application.Feature
{
    public class RequestPermissionsCommand : IRequest<RequestPermissionsResponse>
    {
        public string? NombreEmpleado { get; set; }

        public string? ApellidoEmpleado { get; set; }

        public string? TipoPermiso { get; set; }
    }
}
