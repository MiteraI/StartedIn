using CrossCutting.Enum;
using Domain.Context;
using Domain.Entities;
using Repository.Repositories.Interface;

namespace Repository.Repositories;

public class ConnectionRepository : GenericRepository<Connection,string>, IConnectionRepository
{
    private readonly AppDbContext _appDbContext;
    
    public ConnectionRepository(AppDbContext context) : base(context)
    {
        _appDbContext = context;
    }


    
}