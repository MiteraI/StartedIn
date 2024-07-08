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
        private readonly IEmailService _emailService;
        private readonly UserManager<User> _userManager;

        public TeamService(ITeamRepository teamRepository, 
            IProjectRepository projectRepository,
            ILogger<TeamService> logger, 
            IUnitOfWork unitOfWork,
            IUserRepository userRepository, 
            UserManager<User> userManager, 
            IEmailService emailService)
        {
            _logger = logger;
            _teamRepository = teamRepository;
            _projectRepository = projectRepository;
            _emailService = emailService;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _userManager = userManager;
        }
        public async Task SendJoinTeamInvitation(string userId, List<string> inviteEmails, string teamId)
        {
           var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
               throw new NotFoundException($"User with ID: {userId} not found.");
            }
            var team = await _teamRepository.QueryHelper()
                .Filter(team => team.Id.Equals(teamId))
                .Include(team => team.TeamUsers)
                .GetOneAsync();
            if (team == null)
            {
               throw new NotFoundException("Không tìm thấy team");
            }
            if (!team.TeamLeaderId.Equals(user.Id))
            {
               throw new InviteException("Bạn không có quyền mời thành viên vào nhóm");
            }
            if (team.TeamUsers.Count() >= 5)
            {
               throw new TeamLimitException("Đội đã có đủ 5 thành viên. Vui lòng nâng cấp gói Premium");
            }
            if (team.TeamUsers.Count() + inviteEmails.Count > 5)
            {
               throw new TeamLimitException("Thêm người dùng này sẽ làm số thành viên của đội vượt quá 5 người. Vui lòng nâng cấp lên gói Premium");
            }
            foreach (var inviteEmail in inviteEmails)
            {
               _emailService.SendInvitationToTeam(inviteEmail, teamId);
            }
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

        public async Task AddUserToTeam(string teamId, string userId)
        {
              var user = await _userManager.FindByIdAsync(userId);
              if (user == null)
              {
                throw new NotFoundException("Không tìm thấy người dùng");
              }
              var userInTeam = await _userRepository.GetAUserInTeam(teamId, user.Id);
              if (userInTeam != null)
              {
                throw new InviteException("Người dùng đã có trong nhóm");
              }
              await _userRepository.AddUserToTeam(userId, teamId, RoleInTeam.Member);
              await _unitOfWork.SaveChangesAsync();
        }
    }
}
