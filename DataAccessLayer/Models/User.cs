using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccessLayer.BaseEntities;
using DataAccessLayer.Enum;
using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Models;

public class User : IdentityUser
{
    public User() { 
        Id = Guid.NewGuid().ToString("N");
        CreatedTime = LastUpdatedTime = DateTime.UtcNow;  
    }
    public DateTimeOffset CreatedTime { get; set; }
    public DateTimeOffset LastUpdatedTime { get; set; }
    public string? ProfilePicture { get; set; }
    public string? CoverPhoto { get; set; }
    public string? Content { get; set; }
    public string? Bio { get; set; }
    public AccountStatus Status { get; set; }
    public DateTimeOffset? Verified { get; set; }
    public string? RefreshToken { get; set; }
    public DateTimeOffset? RefreshTokenExpiry { get; set; }
    public bool IsActive => EmailConfirmed && AccessFailedCount < 5;
    public override bool EmailConfirmed => Verified.HasValue;
    [NotMapped]
    public override string SecurityStamp { get => base.SecurityStamp; set => base.SecurityStamp = value; }
    [NotMapped]
    public override string ConcurrencyStamp { get => base.ConcurrencyStamp; set => base.ConcurrencyStamp = value; }
    public IEnumerable<Post> Posts { get; set; }
    public IEnumerable<Team> Teams { get; set; }
}