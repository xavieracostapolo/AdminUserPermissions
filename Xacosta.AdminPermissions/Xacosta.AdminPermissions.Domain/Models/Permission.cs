using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xacosta.AdminPermissions.Domain.Models
{
    [Table("Permission")]
    public class Permission : IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string? NombreEmpleado { get; set; }

        [Required]
        public string? ApellidoEmpleado { get; set; }

        [ForeignKey("TipoPermiso")]
        public PermissionType PermissionType { get; set; }

        public DateOnly FechaPermiso { get; set; }
    }
}
