using System.ComponentModel.DataAnnotations.Schema;
using DataAccessLayer.BaseEntity;
using DataAccessLayer.Enum;

namespace DataAccessLayer.Models;

public class Phase : Entity
{
    public string PhaseName { get; set; }
    
    [ForeignKey(nameof(Project))]
    public string ProjectId { get; set; }
    public Status Status { get; set; }
    
    public Project Project { get; set; } = null!;
    public IEnumerable<MajorTask> MajorTasks { get; set; }
}