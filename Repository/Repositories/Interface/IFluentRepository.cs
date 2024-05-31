using System.Linq.Expressions;
namespace Repository.Repositories.Interface
{
    public interface IFluentRepository<TEntity> where TEntity : class
    {
        IFluentRepository<TEntity> Filter(Expression<Func<TEntity, bool>> filter);
        IFluentRepository<TEntity> OrderBy(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);
        Task<TEntity?> GetOneAsync();
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetPagingAsync(int position, int size);
        IFluentRepository<TEntity> Include(Expression<Func<TEntity, object>> expression);
        IFluentRepository<TEntity> AsNoTracking();
    }
}
