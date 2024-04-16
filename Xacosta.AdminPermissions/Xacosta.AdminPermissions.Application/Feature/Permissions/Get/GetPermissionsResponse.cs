namespace Xacosta.AdminPermissions.Application.Feature.Permissions.Get
{
    public class GetPermissionsResponse
    {
        public int Id { get; set; }
        public string? NombreEmpleado { get; set; }
        public string? ApellidoEmpleado { get; set; }
        public string? TipoPermiso { get; set; }
        public DateOnly FechaPermiso { get; set; }
    }
}
