using System.Linq.Expressions;

namespace Xacosta.AdminPermissions.Domain.ContractsInfraestructure
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task Delete(object id);
        void Delete(TEntity entityToDelete);
        Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        Task<TEntity> GetByID(object id);
        Task Insert(TEntity entity);
        void Update(TEntity entityToUpdate);
    }
}
