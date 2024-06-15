using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class User : IdentityUser
{
    public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset LastUpdatedTime { get; set; } = DateTimeOffset.UtcNow;
    public string FullName { get; set; }
    public string? ProfilePicture { get; set; }
    public string? CoverPhoto { get; set; }
    
    [StringLength(120)]
    public string? Bio { get; set; }
    public DateTimeOffset? Verified { get; set; }
    public string? RefreshToken { get; set; }
    [JsonIgnore] public IEnumerable<Post> Posts { get; set; }
    [JsonIgnore] public virtual IEnumerable<UserRole> UserRoles { get;}
    public ICollection<TeamUser> TeamUsers { get; set; }
}