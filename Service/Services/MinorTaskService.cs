using CrossCutting.DTOs.RequestDTO;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Repository.Repositories.Interface;
using Service.Services.Interface;

namespace Service.Services;

public class MinorTaskService : IMinorTaskService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMinorTaskRepository _minorTaskRepository;
    private readonly ILogger<MinorTask> _logger;

    public MinorTaskService(IUnitOfWork unitOfWork, IMinorTaskRepository minorTaskRepository, ILogger<MinorTask> logger)
    {
        _unitOfWork = unitOfWork;
        _minorTaskRepository = minorTaskRepository;
        _logger = logger;
    }
    public async Task<MinorTask> CreateMinorTask(MinorTaskCreateDTO minorTaskCreateDto)
    {
        try
        {
            _unitOfWork.BeginTransaction();
            MinorTask minorTask = new MinorTask
            {
                Position = minorTaskCreateDto.Position,
                MajorTaskId = minorTaskCreateDto.MajorTaskId,
                TaskboardId = minorTaskCreateDto.TaskboardId,
                TaskTitle = minorTaskCreateDto.TaskTitle,
                Description = minorTaskCreateDto.Description,
                Status = minorTaskCreateDto.Status
                
            };
            var minorTaskEntity = _minorTaskRepository.Add(minorTask);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();
            return minorTask;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating Major Task");
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }
}