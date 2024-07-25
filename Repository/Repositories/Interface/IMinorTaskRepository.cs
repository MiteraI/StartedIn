using Domain.Entities;

namespace Repository.Repositories.Interface;

public interface IMinorTaskRepository : IGenericRepository<MinorTask, string>
{
    Task<IEnumerable<MajorTask>> GetAssignableMajorTasks(string id);
    Task BatchUpdateMajorTaskId(IEnumerable<string> ids, string majorTaskId);
}