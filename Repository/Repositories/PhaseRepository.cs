using Domain.Context;
using Domain.Entities;
using Repository.Repositories.Interface;

namespace Repository.Repositories;

public class PhaseRepository : GenericRepository<Phase, string>, IPhaseRepository
{
    private readonly AppDbContext _context;
    public PhaseRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}