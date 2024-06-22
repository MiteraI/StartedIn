using CrossCutting.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Exceptions;
using System.Reflection.Metadata;

namespace Repository.Repositories.Extensions
{
    public static class UserManagementExtensions
    {
        public static async Task<User>? FindRefreshTokenAsync(this UserManager<User> userManager, string refreshToken) {
            return await userManager?.Users?.FirstOrDefaultAsync(u => u.RefreshToken.Equals(refreshToken));
        }
        public static async Task<IEnumerable<User>> GetUsersAsync(this UserManager<User> userManager, int pageIndex = 1 , int pageSize = 1, string roleName = RoleConstants.USER)
        {
            var userList = await userManager?.GetUsersInRoleAsync(roleName);
            pageIndex = pageIndex < 1 ? 0 : pageIndex - 1;
            pageSize = pageSize < 1 ? 10 : pageSize;
            var pagedUsers = userList.Skip(pageIndex * pageSize).Take(pageSize);
            return pagedUsers;
        }
        public static async Task<IEnumerable<User>> GetNonConnectedUsersAsync(this UserManager<User> userManager,RoleManager<Role> roleManager,User currentUser, int pageIndex = 1, int pageSize = 1, string roleName = RoleConstants.USER)
        {
            // Ensure valid pagination parameters
            // Ensure valid pagination parameters
            pageIndex = pageIndex < 1 ? 0 : pageIndex - 1;
            pageSize = pageSize < 1 ? 10 : pageSize;

            // Get the current user with their connections
            var currentUserWithConnections = await userManager.Users
                .Include(u => u.SentConnections)
                .ThenInclude(c => c.Receiver)
                .Include(u => u.ReceivedConnections)
                .ThenInclude(c => c.Sender)
                .FirstOrDefaultAsync(u => u.Id == currentUser.Id);

            if (currentUserWithConnections == null)
            {
                return Enumerable.Empty<User>();
            }

            // Collect all connected user IDs
            var connectedUserIds = new HashSet<string>(
                currentUserWithConnections.SentConnections.Select(c => c.ReceiverId)
                .Concat(currentUserWithConnections.ReceivedConnections.Select(c => c.SenderId))
            );

            // Get role ID for the given role name
            var role = await roleManager.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
            if (role == null)
            {
                return Enumerable.Empty<User>();
            }
            var roleId = role.Id;

            // Get all users excluding the current user and connected users, and ensure they have the specified role
            var nonConnectedUsers = await userManager.Users
                .Where(u => u.Id != currentUser.Id && !connectedUserIds.Contains(u.Id))
                .Where(u => u.UserRoles.Any(ur => ur.RoleId == roleId))
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return nonConnectedUsers;
        }

    }
}
