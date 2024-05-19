using Domain.Context;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Domain.Data;

public static class DBInitializer
{
    public static async Task Initialize(AppDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        // if (!context.Accounts.Any())
        // {
        //     var admin = new User
        //     {
        //         UserName = "admin",
        //         Email = "admin@gmail.com",
        //     };
        //     await userManager.CreateAsync(admin, "Admin@123");
        //     await userManager.AddToRoleAsync(admin, "Admin");
        //
        //     var accounts = new List<Account>()
        //     {
        //         new Account
        //         {
        //             Id = admin.Id,
        //             Name = "admin",
        //             Email = admin.Email,
        //             Status = Enum.AccountStatus.Active,
        //             CreatedTime = DateTime.Now,
        //             LastUpdatedTime = DateTime.Now,
        //         },
        //
        //     };
        //     
        //     foreach (var account in accounts)
        //     {
        //         context.Accounts.Add(account);
        //     }
        // }

        var adminRoleExists = await roleManager.RoleExistsAsync("Admin");
        var userRoleExists = await roleManager.RoleExistsAsync("User");
        if (!adminRoleExists && !userRoleExists)
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            await roleManager.CreateAsync(new IdentityRole("User"));
        }

    }
}