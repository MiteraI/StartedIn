using CrossCutting.Enum;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interface
{
    public interface IUserRepository : IUserStore<User>
    {
        Task AddUserToTeam(string userId, string teamId, RoleInTeam roleInTeam);
    }
}
