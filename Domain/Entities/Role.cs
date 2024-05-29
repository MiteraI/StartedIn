using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class Role: IdentityRole<string>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
