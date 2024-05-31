using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Extensions
{
    public static class UserManagementExtensions
    {
        public static async Task<User>? FindRefreshTokenAsync(this UserManager<User> userManager, string refreshToken) {
            return await userManager?.Users?.FirstOrDefaultAsync(u => u.RefreshToken.Equals(refreshToken));
        }
    }
}
