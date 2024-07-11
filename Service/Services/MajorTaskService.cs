using CrossCutting.DTOs.RequestDTO;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Repository.Repositories.Interface;
using Service.Services.Interface;

namespace Service.Services;

public class MajorTaskService : IMajorTaskService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMajorTaskRepository _majorTaskRepository;
    private readonly ILogger<MajorTask> _logger;

    public MajorTaskService(IUnitOfWork unitOfWork, IMajorTaskRepository majorTaskRepository, ILogger<MajorTask> logger)
    {
        _unitOfWork = unitOfWork;
        _majorTaskRepository = majorTaskRepository;
        _logger = logger;
    }
    public async Task CreateNewMajorTask(MajorTaskCreateDTO majorTaskCreateDto)
    {
        try
        {
            _unitOfWork.BeginTransaction();
            MajorTask majorTask = new MajorTask
            {
                Position = majorTaskCreateDto.Position,
                PhaseId = majorTaskCreateDto.PhaseId,
                TaskTitle = majorTaskCreateDto.TaskTitle,
                Description = majorTaskCreateDto.Description
            };
            var majorTaskEntity = _majorTaskRepository.Add(majorTask);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating Major Task");
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }
}