using CrossCutting.Enum;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Repository.Repositories.Interface;
using Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ILogger<TeamService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public TeamService(ITeamRepository teamRepository, IProjectRepository projectRepository,ILogger<TeamService> logger, IUnitOfWork unitOfWork,IUserRepository userRepository)
        {
            _logger = logger;
            _teamRepository = teamRepository;
            _projectRepository = projectRepository;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task CreateNewTeam(string userId, Team team, Project project)
        {
            try {
                _unitOfWork.BeginTransaction();
                team.TeamLeaderId = userId;
                var teamEntity = _teamRepository.Add(team);
                project.TeamId = team.Id;
                project.EstimateDuration = (int)((project.EndDate - project.StartDate).TotalDays);
                project.ActualDuration = project.EstimateDuration;
                project.ActualCost = 0;
                project.Progress = 0;
                var projectEntity = _projectRepository.Add(project);
                await _userRepository.AddUserToTeam(userId, teamEntity.Id, RoleInTeam.Leader);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error while creating project and team");
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
        

    }
}
