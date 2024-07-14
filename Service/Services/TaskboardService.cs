using CrossCutting.DTOs.RequestDTO;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Repository.Repositories.Interface;
using Service.Services.Interface;

namespace Service.Services;

public class TaskboardService : ITaskboardService
{
    private readonly ITaskboardRepository _taskboardRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<Taskboard> _logger;

    public TaskboardService(ITaskboardRepository taskboardRepository, IUnitOfWork unitOfWork, ILogger<Taskboard> logger)
    {
        _taskboardRepository = taskboardRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    public async Task<Taskboard> CreateNewTaskboard(TaskboardCreateDTO taskboardCreateDto)
    {
        try
        {
            _unitOfWork.BeginTransaction();
            Taskboard taskboard = new Taskboard
            {
                Position = taskboardCreateDto.Position,
                PhaseId = taskboardCreateDto.PhaseId,
                Title = taskboardCreateDto.Title
                
            };
            var taskboardEntity = _taskboardRepository.Add(taskboard);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();
            return taskboard;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating Taskboard");
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }
}