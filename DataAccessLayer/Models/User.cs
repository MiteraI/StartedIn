using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Models;

public class User : IdentityUser
{
    public virtual Account Account { get; set; }
}