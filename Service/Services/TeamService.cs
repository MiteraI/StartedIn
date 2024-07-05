using CrossCutting.Enum;
using CrossCutting.Exceptions;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Repository.Repositories.Interface;
using Service.Services.Interface;
using Services.Exceptions;

namespace Service.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ILogger<TeamService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;

        public TeamService(ITeamRepository teamRepository, IProjectRepository projectRepository,ILogger<TeamService> logger, IUnitOfWork unitOfWork,IUserRepository userRepository, UserManager<User> userManager)
        {
            _logger = logger;
            _teamRepository = teamRepository;
            _projectRepository = projectRepository;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task CreateNewTeam(string userId, Team team, Project project)
        {
            try {
                _unitOfWork.BeginTransaction();
                var user = await _userManager.FindByIdAsync(userId);
                var teamList = await _teamRepository.QueryHelper().Filter(team => team.TeamLeaderId.Equals(user.Id)).GetAllAsync();
                if (teamList.Any())
                {
                    throw new ExistedRecordException("Bạn không thể tạo thêm team");
                }
                else {
                    team.TeamLeaderId = userId;
                    team.CreatedBy = user.FullName;
                    var teamEntity = _teamRepository.Add(team);
                    project.TeamId = team.Id;
                    project.CreatedBy = user.FullName;
                    var projectEntity = _projectRepository.Add(project);
                    await _userRepository.AddUserToTeam(userId, teamEntity.Id, RoleInTeam.Leader);
                    await _unitOfWork.SaveChangesAsync();
                    await _unitOfWork.CommitAsync();
                }
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error while creating project and team");
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<Team>> GetTeamByUserId(string userId)
        {
            var team = await _teamRepository.GetTeamsByUserIdAsync(userId);
            if (!team.Any()) 
            {
                throw new NotFoundException("Không có team nào.");
            }
            return team;
        }
    }
}
