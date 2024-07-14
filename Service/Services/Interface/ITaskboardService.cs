using CrossCutting.DTOs.RequestDTO;
using Domain.Entities;

namespace Service.Services.Interface;

public interface ITaskboardService
{
    Task<Taskboard> CreateNewTaskboard(TaskboardCreateDTO taskboardCreateDto);
}