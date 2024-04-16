namespace Xacosta.AdminPermissions.Domain.ContractsInfraestructure
{
    public interface IUnitOfWork : IDisposable
    {
        Task Commit();
        void Rollback();
        IRepository<T> Repository<T>() where T : class;
    }
}
