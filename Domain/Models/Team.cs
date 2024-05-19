using System.ComponentModel.DataAnnotations.Schema;
using Domain.BaseEntities;

namespace Domain.Models;

public class Team : BaseAuditEntity
{
    public string TeamName { get; set; }
    [ForeignKey(nameof(User))]
    public string TeamLeaderId { get; set; }
    public string Description { get; set; }
    
    public IEnumerable<Project> Projects { get; set; }
    public IEnumerable<User> Users { get; set; }
}