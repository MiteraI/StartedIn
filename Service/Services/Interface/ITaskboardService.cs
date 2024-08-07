using CrossCutting.DTOs.RequestDTO;
using Domain.Entities;

namespace Service.Services.Interface;

public interface ITaskboardService
{
    Task<Taskboard> CreateNewTaskboard(TaskboardCreateDTO taskboardCreateDto);
    Task<Taskboard> MoveTaskBoard(string taskBoardId, int position, bool needsReposition);
    Task<Taskboard> GetTaskboardById(string id);
    Task<Taskboard> UpdateTaskboard(string id, TaskboardInfoUpdateDTO taskboardInfoUpdateDTO);
}