using System.ComponentModel.DataAnnotations.Schema;
using DataAccessLayer.BaseEntity;

namespace DataAccessLayer.Models;

public class Team : Entity
{
    public string TeamName { get; set; }
    [ForeignKey(nameof(Account))]
    public string TeamLeaderId { get; set; }
    public string Description { get; set; }
    
    public IEnumerable<Project> Projects { get; set; }
    public IEnumerable<Account> Accounts { get; set; }
}