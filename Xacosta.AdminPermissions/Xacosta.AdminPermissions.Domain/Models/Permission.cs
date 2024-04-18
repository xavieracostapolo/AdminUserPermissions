namespace Xacosta.AdminPermissions.Domain.Models
{
    public class Permission : IEntity
    {
        public int Id { get; set; }
        public string? NombreEmpleado { get; set; }
        public string? ApellidoEmpleado { get; set; }
        public int? TipoPermiso { get; set; }
        public DateOnly FechaPermiso { get; set; }
    }
}
