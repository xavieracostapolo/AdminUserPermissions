using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Reflection.Metadata;
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

            base.OnModelCreating(modelBuilder);
        }
    }

    public partial class LinkPermissionTypePermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
            name: "IX_Permissions_TipoPermiso",
            table: "Permissions",
            column: "TipoPermiso");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_PermissionTypes_TipoPermiso",
                table: "Permissions",
                column: "TipoPermiso",
                principalTable: "PermissionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_PermissionTypes_TipoPermiso",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_TipoPermiso",
                table: "Permissions");
        }
    }
}
