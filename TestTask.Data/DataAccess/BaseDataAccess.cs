
using Microsoft.EntityFrameworkCore;
using TestTask.Data.Entity;
using TestTask.Helpers;

namespace TestTask.Data.DataAccess
{
    public abstract class BaseDataAccess<T> where T : BaseEntity
    {
        protected readonly TestTaskApplcationContext _appContext;

        internal BaseDataAccess(TestTaskApplcationContext applicationContext)
        {
            _appContext = applicationContext;
        }

        public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _appContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _appContext.Set<T>().ToListAsync(cancellationToken);
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
            entity.Id = Guid.Empty;
            entity.Created = DateTime.Now;
            await _appContext.Set<T>().AddAsync(entity, cancellationToken);
            await _appContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            entities.ForEach(e =>
            {
                e.Id = Guid.Empty;
                e.Created = DateTime.Now;
            });
            await _appContext.Set<T>().AddRangeAsync(entities, cancellationToken);
            await _appContext.SaveChangesAsync(cancellationToken);
            return entities;
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            _appContext.Set<T>().Remove(entity);
            await _appContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task RemoveRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            _appContext.Set<T>().RemoveRange(entities);
            await _appContext.SaveChangesAsync(cancellationToken);
        }

        public void Clean()
        {
            _appContext.ChangeTracker.Clear();
        }
    }
}
