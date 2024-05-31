using Microsoft.EntityFrameworkCore;
using Domain.Context;
using Repository.Repositories.Interface;
using Domain.Entities.BaseEntities;
namespace Repository.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>, IDisposable where TEntity : BaseEntity<TKey>
    {
        protected internal readonly AppDbContext _context;
        protected internal readonly DbSet<TEntity> _dbSet;

        //Even though there is only one other repository, it is better to have a generic repository to avoid code duplication and extend codebase easily in the future.
        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public TEntity Add(TEntity entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await Task.FromResult(_dbSet.Remove(entity));
        }

        public async Task DeleteByIdAsync(TKey id)
        {
            var entity = await GetOneAsync(id);
            _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> GetOneAsync(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        public IFluentRepository<TEntity> QueryHelper()
        {
            var repository = new FluentRepository<TEntity>(_dbSet);
            return repository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public TEntity Update(TEntity entity)
        {
            _dbSet.Update(entity);
            return entity;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<IEnumerable<TEntity>> GetPagingAsync(int pageIndex = 1, int pageSize = 1)
        {
            pageIndex = pageIndex < 1 ? 0 : pageIndex - 1;
            pageSize = pageSize < 1 ? 10 : pageSize;
            return _dbSet.Skip(pageIndex * pageSize).Take(pageSize).ToList();

        }
    }
}
