namespace Xacosta.AdminPermissions.Application.Feature.Permissions.Get
{
    public class GetPermissionsResponse
    {
        public string? NombreEmpleado { get; set; }
        public string? ApellidoEmpleado { get; set; }
        public int? TipoPermiso { get; set; }
        public DateOnly FechaPermiso { get; set; }
    }
}
