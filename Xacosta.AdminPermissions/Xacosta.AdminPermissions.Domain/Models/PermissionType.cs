namespace Xacosta.AdminPermissions.Domain.Models
{
    public class PermissionType : IEntity
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
    }
}
