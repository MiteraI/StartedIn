using CrossCutting.DTOs.RequestDTO;
using Domain.Entities;

namespace Service.Services.Interface;

public interface IMajorTaskService
{
    Task<string> CreateNewMajorTask(MajorTaskCreateDTO majorTaskCreateDto);
    Task<MajorTask> MoveMajorTask(string mjTaskId, string phaseId, int position, bool needsReposition);
    Task<MajorTask> GetMajorTaskById(string id);
    Task<MajorTask> UpdateMajorTaskInfo(UpdateMajorTaskInfoDTO updateMajorTaskInfoDTO);
}