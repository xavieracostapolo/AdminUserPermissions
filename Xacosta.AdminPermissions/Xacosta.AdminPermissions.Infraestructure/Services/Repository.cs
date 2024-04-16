using Microsoft.EntityFrameworkCore;
using Xacosta.AdminPermissions.Domain.ContractsInfraestructure;

namespace Xacosta.AdminPermissions.Infraestructure.Services
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly PersistenceContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public Repository(PersistenceContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public IList<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T GetById(object id)
        {
            throw new NotImplementedException();
        }
    }
}
