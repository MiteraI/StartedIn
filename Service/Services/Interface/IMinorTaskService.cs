using CrossCutting.DTOs.RequestDTO;
using Domain.Entities;

namespace Service.Services.Interface;

public interface IMinorTaskService
{
    Task<MinorTask> CreateMinorTask(MinorTaskCreateDTO minorTaskCreateDto);
    Task<MinorTask> MoveMinorTask(string mnTaskId, string taskBoardId, int position, bool needsReposition);
    Task<MinorTask> UpdateMinorTaskInfo(string id, UpdateMinorTaskInfoDTO updateMinorTaskInfoDTO);
    Task<IEnumerable<MajorTask>> GetAssignableMajorTasks(string id);
}