using Domain.Context;
using Domain.Entities;
using Repository.Repositories.Interface;

namespace Repository.Repositories;

public class MinorTaskRepository : GenericRepository<MinorTask, string>, IMinorTaskRepository
{
    private readonly AppDbContext _appDbContext;
    public MinorTaskRepository(AppDbContext context) : base(context)
    {
        _appDbContext = context;
    }
}