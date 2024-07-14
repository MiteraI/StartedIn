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
        if (chosenMajorTask == null)
        {
            throw new NotFoundException("Không có Task lớn được tìm thấy");
        }

        var chosenPhase = await _phaseRepository.QueryHelper()
            .Filter(p => p.Id.Equals(phaseId))
            .Include(p => p.MajorTasks)
            .GetOneAsync();
        if (chosenPhase == null)
        {
            throw new NotFoundException("Không có giai đoạn được tìm thấy");
        }

        var oldPhase = await _phaseRepository.GetOneAsync(chosenMajorTask.PhaseId);
        if (oldPhase == null)
        {
            throw new NotFoundException("Không có giai đoạn cũ được tìm thấy");
        }

        try
        {
            // Remove the task from the old phase's task list
            oldPhase.MajorTasks.Remove(chosenMajorTask);
            _phaseRepository.Update(oldPhase);

            // Update the chosen major task's new phase information
            chosenMajorTask.Position = position;
            chosenMajorTask.PhaseId = phaseId;
            chosenMajorTask.Phase = chosenPhase;
            _majorTaskRepository.Update(chosenMajorTask);

            // Add the task to the new phase's task list
            chosenPhase.MajorTasks.Add(chosenMajorTask);
            _phaseRepository.Update(chosenPhase);

            await _unitOfWork.SaveChangesAsync();

            if (needsReposition)
            {
                _unitOfWork.BeginTransaction();

                // Reposition tasks in the old phase
                var oldPhaseTasks = await _majorTaskRepository.QueryHelper()
                    .Filter(p => p.PhaseId.Equals(oldPhase.Id))
                    .OrderBy(p => p.OrderBy(p => p.Position))
                    .GetAllAsync();

                int oldPhaseIncrement = (int)Math.Pow(2, 16);
                int oldPhaseCurrentPosition = (int)Math.Pow(2, 16);

                foreach (var majorTask in oldPhaseTasks)
                {
                    majorTask.Position = oldPhaseCurrentPosition;
                    _majorTaskRepository.Update(majorTask);
                    oldPhaseCurrentPosition += oldPhaseIncrement;
                }

                // Reposition tasks in the new phase
                var newPhaseTasks = await _majorTaskRepository.QueryHelper()
                    .Filter(p => p.PhaseId.Equals(chosenPhase.Id))
                    .OrderBy(p => p.OrderBy(p => p.Position))
                    .GetAllAsync();

                int newPhaseIncrement = (int)Math.Pow(2, 16);
                int newPhaseCurrentPosition = (int)Math.Pow(2, 16);

                foreach (var majorTask in newPhaseTasks)
                {
                    majorTask.Position = newPhaseCurrentPosition;
                    _majorTaskRepository.Update(majorTask);
                    newPhaseCurrentPosition += newPhaseIncrement;
                }

                await _unitOfWork.SaveChangesAsync();
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