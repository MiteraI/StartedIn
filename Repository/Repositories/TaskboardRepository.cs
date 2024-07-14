using Domain.Context;
using Domain.Entities;
using Repository.Repositories.Interface;

namespace Repository.Repositories;

public class TaskboardRepository : GenericRepository<Taskboard, string>, ITaskboardRepository
{
    public TaskboardRepository(AppDbContext context) : base(context)
    {
    }
}