using Domain.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interface;

namespace Repository.Repositories;

public class MajorTaskRepository : GenericRepository<MajorTask, string>, IMajorTaskRepository
{
    private readonly AppDbContext _context;
    public MajorTaskRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MinorTask>> GetAssignableMinorTasks(string id)
    {
        MajorTask? task = await _dbSet
            .Where(mt => mt.Id == id)
            .Include(mt => mt.Phase)
            .ThenInclude(p => p.Taskboards)
            .ThenInclude(t => t.MinorTasks)
            .FirstOrDefaultAsync();
        if (task == null)
        {
            return [];
        }
        List<MinorTask> result = [];
        foreach (Taskboard taskboard in task.Phase.Taskboards)
        {
            foreach (MinorTask mt in taskboard.MinorTasks)
            {
                if (mt.MajorTaskId == null)
                {
                    result.Add(mt);
                }
            }
        }
        return result;
    }
}