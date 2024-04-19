using Elastic.Clients.Elasticsearch;
using Microsoft.Extensions.DependencyInjection;
using Xacosta.AdminPermissions.Domain.ContractsInfraestructure;
using Xacosta.AdminPermissions.Domain.Models;
using Xacosta.AdminPermissions.Infraestructure.Services;

namespace Xacosta.AdminPermissions.Infraestructure
{
    public static class DependencyInjection
    {
        public static void AddDependencyInfraestructure(this IServiceCollection services, ElasticsearchClient clientElk)
        {
            services.AddSingleton<IElasticService>(new ElasticService(clientElk));
            services.AddSingleton<IKafkaService, KafkaService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRepository<Permission>, Repository<Permission>>();
            services.AddScoped<IRepository<PermissionType>, Repository<PermissionType>>();            
        }
    }
}
