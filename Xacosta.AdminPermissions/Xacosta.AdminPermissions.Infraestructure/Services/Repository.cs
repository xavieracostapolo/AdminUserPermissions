using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Xacosta.AdminPermissions.Domain.ContractsInfraestructure;

namespace Xacosta.AdminPermissions.Infraestructure.Services
{
    public class Repository<TEntity> :  IRepository<TEntity> where TEntity : class
    {
        internal PersistenceContext context;
        internal DbSet<TEntity> dbSet;

        public Repository(PersistenceContext _context)
        {
            context = _context;
            dbSet = context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {

            //Todo: test kafka
            var config = new ProducerConfig
            {
                BootstrapServers = "138.197.116.22:9092"
            };

            using var producer = new ProducerBuilder<Null, string>(config).Build();

            var topic = "quickstart-events";
            var message = "Hola, Kafka!";

            var deliveryReport = producer.ProduceAsync(topic, new Message<Null, string> { Value = message }).GetAwaiter().GetResult();

            Console.WriteLine($"Mensaje enviado a {deliveryReport.TopicPartitionOffset}");



            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).AsNoTracking().ToListAsync();
            }
            else
            {
                return await query.AsNoTracking().ToListAsync();
            }
        }

        public async Task<TEntity> GetByID(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task Insert(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task Delete(object id)
        {
            TEntity entityToDelete = await dbSet.FindAsync(id);
            Delete(entityToDelete);
        }

        public void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
