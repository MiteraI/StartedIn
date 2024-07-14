using Domain.Entities;

namespace Repository.Repositories.Interface;

public interface IPhaseRepository : IGenericRepository<Phase, string>
{
    Task<Phase> GetPhaseDetailById(string id);
    
}