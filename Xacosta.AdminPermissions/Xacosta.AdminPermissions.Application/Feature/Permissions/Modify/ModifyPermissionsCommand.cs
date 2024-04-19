using MediatR;

namespace Xacosta.AdminPermissions.Application.Feature
{
    public class ModifyPermissionsCommand : IRequest<ModifyPermissionsResponse>
    {
        public int IdPermiso { get; set; }
        public string? NombreEmpleado { get; set; }
        public string? ApellidoEmpleado { get; set; }
        public int? TipoPermiso { get; set; }
    }
}
