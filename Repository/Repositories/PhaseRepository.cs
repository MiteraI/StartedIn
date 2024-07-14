using Domain.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interface;

namespace Repository.Repositories;

public class PhaseRepository : GenericRepository<Phase, string>, IPhaseRepository
{
    private readonly AppDbContext _appDbContext;
    public PhaseRepository(AppDbContext context) : base(context)
    {
        _appDbContext = context;
    }

    public async Task<Phase> GetPhaseDetailById(string id)
    {
        var phase = await _appDbContext.Phases
            .Include(t => t.Taskboards)
            .ThenInclude(t => t.MinorTasks)
            .FirstOrDefaultAsync(p => p.Id.Equals(id));
        return phase;
    }
}