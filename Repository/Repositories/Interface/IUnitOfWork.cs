namespace Repository.Repositories.Interface
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        Task CommitAsync();
        Task RollbackAsync();
        Task SaveChangesAsync();
    }
}
