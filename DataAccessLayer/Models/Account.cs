using System.ComponentModel.DataAnnotations;
using DataAccessLayer.BaseEntity;
using DataAccessLayer.Enum;

namespace DataAccessLayer.Models;

public class Account : Entity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public string profilePicture { get; set; }
    public string coverPhoto { get; set; }
    public string Content { get; set; }
    public string Bio { get; set; }
    public AccountStatus Status { get; set; }
    
    public IEnumerable<Post> Posts { get; set; }
    public IEnumerable<Team> Teams { get; set; }
}