using Microsoft.EntityFrameworkCore;
using Xacosta.AdminPermissions.Domain.Models;

namespace Xacosta.AdminPermissions.Infraestructure
{

    public class PersistenceContext : DbContext
    {
        public PersistenceContext(DbContextOptions<PersistenceContext> options) : base(options)
        {
        }

        public DbSet<Permission> Permission { get; set; }
        public DbSet<PermissionType> PermissionType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }    
}
