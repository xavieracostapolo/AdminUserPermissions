using Elastic.Clients.Elasticsearch;
using Xacosta.AdminPermissions.Domain.ContractsInfraestructure;
using Xacosta.AdminPermissions.Domain.Models;

namespace Xacosta.AdminPermissions.Infraestructure.Services
{
    public class ElasticService(ElasticsearchClient _client) : IElasticService
    {
        public async Task<bool> Send(Permission item)
        {
            var index = $"permission-index-{DateTime.UtcNow.Ticks}";
            var response = await _client.IndexAsync(item, idx => idx.Index(index));

            return response.IsValidResponse;            
        }
    }
}
