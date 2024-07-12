using CrossCutting.DTOs.RequestDTO;

namespace Service.Services.Interface;

public interface IMajorTaskService
{
    Task<string> CreateNewMajorTask(MajorTaskCreateDTO majorTaskCreateDto);
}