using Domain.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interface;

namespace Repository.Repositories;

public class MinorTaskRepository : GenericRepository<MinorTask, string>, IMinorTaskRepository
{
    private readonly AppDbContext _appDbContext;
    public MinorTaskRepository(AppDbContext context) : base(context)
    {
        _appDbContext = context;
    }

    public async Task<IEnumerable<MajorTask>> GetAssignableMajorTasks(string id)
    {
        MinorTask? task = await _dbSet
            .Where(mt => mt.Id == id)
            .Include(mt => mt.Taskboard)
            .ThenInclude(t => t.Phase)
            .ThenInclude(p => p.MajorTasks)
            .FirstOrDefaultAsync();
        if (task == null)
        {
            return [];
        }
        return task.Taskboard.Phase.MajorTasks;
    }

    public async Task BatchUpdateMajorTaskId(IEnumerable<string> ids, string? majorTaskId)
    {
        await _dbSet
            .Where(mt => ids.Contains(mt.Id))
            .ExecuteUpdateAsync(s => s
                .SetProperty(mt => mt.MajorTaskId, mt => majorTaskId)
                .SetProperty(mt => mt.LastUpdatedTime, mt => DateTimeOffset.UtcNow));
    }
}