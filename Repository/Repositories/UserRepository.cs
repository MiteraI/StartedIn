using CrossCutting.Enum;
using Domain.Context;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interface;

namespace Repository.Repositories
{
    public class UserRepository : UserStore<User, Role, AppDbContext>, IUserRepository
    {
        private readonly AppDbContext _appDbContext;
        public UserRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        public async Task AddUserToTeam(string userId, string teamId, RoleInTeam roleInTeam)
        {
            var userTeam = new TeamUser
            {
                UserId = userId,
                TeamId = teamId,
                RoleInTeam = roleInTeam
            };
            await _appDbContext.Set<TeamUser>().AddAsync(userTeam);
        }

        public async Task<TeamUser> GetAUserInTeam(string teamId, string userId)
        {
            return await _appDbContext.Set<TeamUser>().Where(x => x.TeamId.Equals(teamId) && x.UserId.Equals(userId)).FirstOrDefaultAsync();
        }
       
    }


}
