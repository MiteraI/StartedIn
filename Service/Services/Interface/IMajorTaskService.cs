using CrossCutting.DTOs.RequestDTO;

namespace Service.Services.Interface;

public interface IMajorTaskService
{
    Task CreateNewMajorTask(MajorTaskCreateDTO majorTaskCreateDto);
}