using Domain.Entities;

namespace Repository.Repositories.Interface;

public interface IMajorTaskRepository : IGenericRepository<MajorTask, string>
{
    Task<IEnumerable<MinorTask>> GetAssignableMinorTasks(string id);
}