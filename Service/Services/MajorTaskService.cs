using CrossCutting.DTOs.RequestDTO;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Repository.Repositories;
using Repository.Repositories.Interface;
using Service.Services.Interface;
using Services.Exceptions;

namespace Service.Services;

public class MajorTaskService : IMajorTaskService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMajorTaskRepository _majorTaskRepository;
    private readonly IPhaseRepository _phaseRepository;
    private readonly ILogger<MajorTask> _logger;

    public MajorTaskService(IUnitOfWork unitOfWork, IMajorTaskRepository majorTaskRepository, ILogger<MajorTask> logger, IPhaseRepository phaseRepository)
    {
        _unitOfWork = unitOfWork;
        _majorTaskRepository = majorTaskRepository;
        _logger = logger;
        _phaseRepository = phaseRepository;
    }
    public async Task<string> CreateNewMajorTask(MajorTaskCreateDTO majorTaskCreateDto)
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
            return majorTaskEntity.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating Major Task");
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }

    public async Task<MajorTask> MoveMajorTask(string mjTaskId, string phaseId, int position, bool needsReposition)
    {
        var chosenMajorTask = await _majorTaskRepository.GetOneAsync(mjTaskId);
        if(chosenMajorTask == null)
        {
            throw new NotFoundException("Không có Task lớn được tìm thấy");
        }
        var chosenPhase = await _phaseRepository.GetOneAsync(phaseId);
        if (chosenPhase == null)
        {
            throw new NotFoundException("Không có giai đoạn được tìm thấy");
        }
        try
        {
            chosenMajorTask.Position = position;
            chosenMajorTask.PhaseId = phaseId;
            chosenMajorTask.Phase = chosenPhase;
            _majorTaskRepository.Update(chosenMajorTask);
            await _unitOfWork.SaveChangesAsync();
            if (needsReposition)
            {
                _unitOfWork.BeginTransaction();
                // Get all phases for the project and sort them by position
                var majorTasks = await _majorTaskRepository.QueryHelper()
                    .Filter(p => p.PhaseId.Equals(chosenPhase.Id))
                    .OrderBy(p => p.OrderBy(p => p.Position))
                    .GetAllAsync();

                // Update positions starting from 2^16 (65536)
                int increment = (int)Math.Pow(2, 16);
                int currentPosition = (int)Math.Pow(2, 16);

                foreach (var majorTask in majorTasks)
                {
                    majorTask.Position = currentPosition;
                    _majorTaskRepository.Update(majorTask);
                    currentPosition += increment;
                    await _unitOfWork.SaveChangesAsync();
                }
                await _unitOfWork.CommitAsync();
            }
            return chosenMajorTask;
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while moving major task");
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }
}