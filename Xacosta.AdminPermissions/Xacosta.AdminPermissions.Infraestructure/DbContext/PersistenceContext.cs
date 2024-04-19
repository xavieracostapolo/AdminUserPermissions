using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
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
            /*
            modelBuilder.Entity<Permission>(
                entityBuilder =>
                {
                    entityBuilder
                        .ToTable("Permissions")
                        .SplitToTable(
                            "PermissionTypes",
                            tableBuilder =>
                            {
                                tableBuilder.Property(table => table.TipoPermiso).HasColumnName("Descripcion");
                            });
                });

            modelBuilder.Entity<PermissionType>().HasData(new List<PermissionType>
            {
                new()
                {
                    Id = 1,
                    Descripcion = "Descripcion"
                }
            });

            modelBuilder.Entity<Permission>().HasData(new List<Permission>
            {
                new() {
                    Id = 1,
                    ApellidoEmpleado = "ApellidoEmpleado1",
                    FechaPermiso = DateOnly.FromDateTime(DateTime.Now),
                    NombreEmpleado = "NombreEmpleado1",
                    TipoPermiso = 1
                },
                new() {
                    Id = 2,
                    ApellidoEmpleado = "ApellidoEmpleado2",
                    FechaPermiso = DateOnly.FromDateTime(DateTime.Now),
                    NombreEmpleado = "NombreEmpleado2",
                    TipoPermiso = 1
                }
            });
            */

            base.OnModelCreating(modelBuilder);
        }
    }    
}
