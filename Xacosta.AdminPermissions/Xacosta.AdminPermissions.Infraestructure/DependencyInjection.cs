using Microsoft.Extensions.DependencyInjection;
using Xacosta.AdminPermissions.Domain.ContractsInfraestructure;
using Xacosta.AdminPermissions.Domain.Models;
using Xacosta.AdminPermissions.Infraestructure.Services;

namespace Xacosta.AdminPermissions.Infraestructure
{
    public static class DependencyInjection
    {
        public static void AddDependencyInfraestructure(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRepository<Permission>, Repository<Permission>>();
            services.AddScoped<IRepository<PermissionType>, Repository<PermissionType>>();
        }
    }
}
