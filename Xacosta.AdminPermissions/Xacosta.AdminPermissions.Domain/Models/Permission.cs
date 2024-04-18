using System.ComponentModel.DataAnnotations.Schema;

namespace Xacosta.AdminPermissions.Domain.Models
{
    [Table("Permission")]
    public class Permission : IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? NombreEmpleado { get; set; }
        public string? ApellidoEmpleado { get; set; }
        public int? TipoPermiso { get; set; }
        public DateOnly FechaPermiso { get; set; }
    }
}
