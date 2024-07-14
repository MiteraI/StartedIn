using Domain.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class TeamRepository : GenericRepository<Team, string>, ITeamRepository
    {
        private readonly AppDbContext _appDbContext;
        public TeamRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        public async Task<IEnumerable<Team>> GetTeamByUserGuestIdAsync(string userId)
        {
            var teams = await _appDbContext.Teams
                .Where(t => t.TeamUsers.Any(tu => tu.UserId == userId) && t.TeamLeaderId != userId)
                .Include(t => t.Projects)
                .Include(t => t.TeamUsers)
                .ThenInclude(tu => tu.User).OrderByDescending(t => t.CreatedTime)
                .ToListAsync();
            return teams;
        }

        public async Task<IEnumerable<Team>> GetTeamByUserLeaderIdAsync(string userId)
        {
            var teams = await _appDbContext.Teams
                .Where(t => t.TeamLeaderId == userId)
                .Include(t => t.Projects)
                .Include(t => t.TeamUsers)
                .ThenInclude(tu => tu.User).OrderByDescending(t => t.CreatedTime)
                .ToListAsync();
            return teams;
        }

        public async Task<Team> GetTeamByIdAsync(string teamId)
        {
            var team = await _appDbContext.Teams
                .Where(t => t.Id.Equals(teamId))
                .Include(t => t.Projects)
                .Include(t => t.TeamLeader)
                .Include(t => t.TeamUsers)
                .ThenInclude(tu => tu.User).FirstOrDefaultAsync();
            return team;
        }

        public async Task<IEnumerable<Team>> GetTeamsByUserIdAsync(string userId)
        {
            var teams = await _appDbContext.Teams
                .Where(t => t.TeamUsers.Any(tu => tu.UserId == userId))
                .Include(t => t.Projects)
                .Include(t => t.TeamUsers)
                .ThenInclude(tu => tu.User).OrderByDescending(t => t.CreatedTime)
                .ToListAsync();
            return teams;
        }
    }
}
