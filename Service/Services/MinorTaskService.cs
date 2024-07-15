using CrossCutting.DTOs.RequestDTO;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Repository.Repositories;
using Repository.Repositories.Interface;
using Service.Services.Interface;
using Services.Exceptions;

namespace Service.Services;

public class MinorTaskService : IMinorTaskService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMinorTaskRepository _minorTaskRepository;
    private readonly ILogger<MinorTask> _logger;
    private readonly ITaskboardRepository _taskboardRepository;

    public MinorTaskService(IUnitOfWork unitOfWork, IMinorTaskRepository minorTaskRepository, ILogger<MinorTask> logger, ITaskboardRepository taskboardRepository)
    {
        _unitOfWork = unitOfWork;
        _minorTaskRepository = minorTaskRepository;
        _logger = logger;
        _taskboardRepository = taskboardRepository;
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
            _logger.LogError(ex, "Error while creating Minor Task");
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }

    public async Task<MinorTask> MoveMinorTask(string mnTaskId, string taskBoardId, int position, bool needsReposition)
    {
        var chosenMinorTask = await _minorTaskRepository.GetOneAsync(mnTaskId);
        if (chosenMinorTask == null)
        {
            throw new NotFoundException("Không có Task nhỏ được tìm thấy");
        }

        var chosenTaskBoard = await _taskboardRepository.QueryHelper()
            .Filter(p => p.Id.Equals(taskBoardId))
            .Include(p => p.MinorTasks)
            .GetOneAsync();
        if (chosenTaskBoard == null)
        {
            throw new NotFoundException("Không có bảng công việc được tìm thấy");
        }

        var oldTaskBoard = await _taskboardRepository.GetOneAsync(chosenMinorTask.TaskboardId);
        if (oldTaskBoard == null)
        {
            throw new NotFoundException("Không có bảng công việc cũ được tìm thấy");
        }
        if (oldTaskBoard.PhaseId != chosenTaskBoard.PhaseId)
        {
            throw new Exception("Bảng công việc cũ không cùng giai đoạn với bảng công việc mới");
        }

        try
        {
            // Update the chosen major task's new phase information
            chosenMinorTask.Position = position;
            chosenMinorTask.TaskboardId = taskBoardId;
            _minorTaskRepository.Update(chosenMinorTask);

            // Add the task to the new phase's task list
            chosenTaskBoard.MinorTasks.Add(chosenMinorTask);
            _taskboardRepository.Update(chosenTaskBoard);

            await _unitOfWork.SaveChangesAsync();

            if (needsReposition)
            {
                _unitOfWork.BeginTransaction();

                // Reposition tasks in the new phase
                var newTaskBoardTasks = await _minorTaskRepository.QueryHelper()
                    .Filter(p => p.TaskboardId.Equals(chosenTaskBoard.Id))
                    .OrderBy(p => p.OrderBy(p => p.Position))
                    .GetAllAsync();

                int newTaskBoardIncrement = (int)Math.Pow(2, 16);
                int newTaskBoardCurrentPosition = (int)Math.Pow(2, 16);

                foreach (var minorTask in newTaskBoardTasks)
                {
                    minorTask.Position = newTaskBoardCurrentPosition;
                    _minorTaskRepository.Update(minorTask);
                    newTaskBoardCurrentPosition += newTaskBoardIncrement;
                }

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }

            return chosenMinorTask;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while moving minor task");
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }
}