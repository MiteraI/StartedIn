using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.BaseEntities;
using Domain.Enum;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models;

public class User : IdentityUser
{
    public User() { 
        Id = Guid.NewGuid().ToString("N");
        CreatedTime = LastUpdatedTime = DateTime.UtcNow;  
    }
    public DateTimeOffset CreatedTime { get; set; }
    public DateTimeOffset LastUpdatedTime { get; set; }
    public string FullName { get; set; }
    public string? ProfilePicture { get; set; }
    public string? CoverPhoto { get; set; }
    public string? Content { get; set; }
    public string? Bio { get; set; }
    public DateTimeOffset? Verified { get; set; }
    public string? RefreshToken { get; set; }
    public DateTimeOffset? RefreshTokenExpiry { get; set; }
    public bool IsActive { get; set; }
    public override bool EmailConfirmed => Verified.HasValue;
    public IEnumerable<Post> Posts { get; set; }
    public IEnumerable<Team> Teams { get; set; }
}