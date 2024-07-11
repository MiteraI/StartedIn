using Domain.Context;
using Domain.Entities;
using Repository.Repositories.Interface;

namespace Repository.Repositories;

public class MajorTaskRepository : GenericRepository<MajorTask, string>, IMajorTaskRepository
{
    private readonly AppDbContext _context;
    public MajorTaskRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}