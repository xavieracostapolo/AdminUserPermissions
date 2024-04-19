using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using Xacosta.AdminPermissions.Domain.ContractsInfraestructure;

namespace Xacosta.AdminPermissions.Infraestructure.Services
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly PersistenceContext _dbContext;
        private readonly ConcurrentDictionary<Type, object> _repositories;

        public UnitOfWork(PersistenceContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new ConcurrentDictionary<Type, object>();
        }

        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IRepository<T> Repository<T>() where T : class
        {
            if (_repositories.ContainsKey(typeof(T)))
            {
                return (IRepository<T>)_repositories[typeof(T)];
            }

            var repository = new Repository<T>(_dbContext);
            _repositories.TryAdd(typeof(T), repository);
            return repository;
        }

        public void Rollback()
        {
            foreach (var entry in _dbContext.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
        }
    }
}
