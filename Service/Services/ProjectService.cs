using CrossCutting.DTOs.RequestDTO;
using CrossCutting.Exceptions;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Repository.Repositories.Interface;
using Service.Services.Interface;

namespace Service.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITeamRepository _teamRepository;
    private readonly UserManager<User> _userManager;
    private readonly ILogger<ProjectService> _logger;

    public ProjectService(IProjectRepository projectRepository, IUnitOfWork unitOfWork,
        ITeamRepository teamRepository, UserManager<User> userManager,
        ILogger<ProjectService> logger)
    {
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
        _teamRepository = teamRepository;
        _userManager = userManager;
        _logger = logger;
    }
    public async Task CreateNewProject(NewProjectCreateDTO projectCreateDto)
    {
        var team = await _teamRepository.GetOneAsync(projectCreateDto.TeamId);
        var teamLeaderId = team.TeamLeaderId;
        var leader = await _userManager.FindByIdAsync(teamLeaderId);
        var leaderName = leader.FullName;
        var teamProjects = await _projectRepository.GetProjectsByTeamIdAsync(projectCreateDto.TeamId);
        var existingProject = teamProjects.Where(p => p.ProjectName.Equals(projectCreateDto.ProjectName));
        if (existingProject != null)
        {
            throw new ExistedRecordException("Trùng tên dự án");
        }
        try
        {
            _unitOfWork.BeginTransaction();
            Project project = new Project
            {
                ProjectName = projectCreateDto.ProjectName,
                StartDate = projectCreateDto.StartDate,
                EndDate = projectCreateDto.EndDate,
                TeamId = projectCreateDto.TeamId,
                EstimateDuration = (int)((projectCreateDto.EndDate - projectCreateDto.StartDate).TotalDays),
                ActualCost = 0,
                Progress = 0,
                CreatedBy = leaderName
            };
            var projectEntity = _projectRepository.Add(project);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex) 
        {
            _logger.LogError(ex, "Error while creating project");
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }

    public async Task<IEnumerable<Project>> GetProjectsByTeamId(string teamId)
    {
        return await _projectRepository.GetProjectsByTeamIdAsync(teamId);
    }
}