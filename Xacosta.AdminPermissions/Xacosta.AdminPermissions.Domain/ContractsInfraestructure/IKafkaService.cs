namespace Xacosta.AdminPermissions.Domain.ContractsInfraestructure
{
    public interface IKafkaService
    {
        Task<bool> Send(string message);
    }
}
