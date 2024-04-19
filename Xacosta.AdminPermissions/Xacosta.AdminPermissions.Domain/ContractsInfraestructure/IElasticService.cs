using Xacosta.AdminPermissions.Domain.Models;

namespace Xacosta.AdminPermissions.Domain.ContractsInfraestructure
{
    public interface IElasticService
    {
        Task<bool> Send(Permission item);
    }
}
