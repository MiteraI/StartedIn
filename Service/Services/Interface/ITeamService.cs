using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interface
{
    public interface ITeamService
    {
        Task CreateNewTeam(string userId, Team team, Project project); 
        Task<IEnumerable<Team>> GetTeamByUserId(string userId);
        Task<Team> GetTeamById(string teamId);
        Task SendJoinTeamInvitation(string userId, List<string> inviteEmails, string teamId);
        Task AddUserToTeam(string teamId, string userId);
        Task<IEnumerable<Team>> GetTeamByUserIfLeader(string userId);
        Task<IEnumerable<Team>> GetTeamByUserIfGuest(string userId);
    }
}
