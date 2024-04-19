using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xacosta.AdminPermissions.Domain.Models
{
    [Table("PermissionType")]
    public class PermissionType : IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string? Descripcion { get; set; }        
    }
}
