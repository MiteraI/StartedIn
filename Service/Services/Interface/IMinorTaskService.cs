using CrossCutting.DTOs.RequestDTO;
using Domain.Entities;

namespace Service.Services.Interface;

public interface IMinorTaskService
{
    Task<MinorTask> CreateMinorTask(MinorTaskCreateDTO minorTaskCreateDto);
}