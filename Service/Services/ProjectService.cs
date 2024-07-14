using CrossCutting.DTOs.RequestDTO;
using CrossCutting.DTOs.ResponseDTO;
using CrossCutting.Exceptions;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Repository.Repositories.Interface;
using Service.Services.Interface;
using Services.Exceptions;

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
    public async Task<Project> CreateNewProject(NewProjectCreateDTO projectCreateDto)
    {
        var team = await _teamRepository.GetOneAsync(projectCreateDto.TeamId);
        var teamLeaderId = team.TeamLeaderId;
        var leader = await _userManager.FindByIdAsync(teamLeaderId);
        var leaderName = leader.FullName;
        var teamProjects = await _projectRepository.GetProjectsByTeamIdAsync(projectCreateDto.TeamId);
        if (teamProjects.Any())
        {
            var existingProject = teamProjects.FirstOrDefault(p => p.ProjectName.ToLower().Equals(projectCreateDto.ProjectName.ToLower()));
            if (existingProject != null)
            {
                throw new ExistedRecordException("Trùng tên dự án");
            }
        }

        try
        {
            _unitOfWork.BeginTransaction();
            Project project = new Project
            {
                ProjectName = projectCreateDto.ProjectName,
                TeamId = projectCreateDto.TeamId,
                CreatedBy = leaderName
            };
            var projectEntity = _projectRepository.Add(project);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();
            return project;
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

    public async Task<Project> GetProjectById(string id)
    {
        var project = await _projectRepository.GetProjectById(id);
        if (project == null)
        {
            throw new NotFoundException("Không có project nào");
        }

        return project;
    }
}