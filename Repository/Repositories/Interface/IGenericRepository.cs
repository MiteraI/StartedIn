using Domain.Entities.BaseEntities;
namespace Repository.Repositories.Interface
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<TEntity> GetOneAsync(TKey id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetPagingAsync(int position, int size);
        Task DeleteByIdAsync(TKey id);
        Task DeleteAsync(TEntity entity);
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        Task<int> SaveChangesAsync();
        IFluentRepository<TEntity> QueryHelper();
    }
}
