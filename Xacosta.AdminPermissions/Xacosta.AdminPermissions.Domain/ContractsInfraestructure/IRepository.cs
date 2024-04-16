namespace Xacosta.AdminPermissions.Domain.ContractsInfraestructure
{
    public interface IRepository<T> where T : class
    {
        T GetById(object id);
        IList<T> GetAll();
        void Add(T entity);
    }
}
