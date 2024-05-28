using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entity.BaseEntities;
using Domain.Enum;

namespace Domain.Entities;

public class Phase : BaseAuditEntity<string>
{
    public string PhaseName { get; set; }
    
    [ForeignKey(nameof(Project))]
    public string ProjectId { get; set; }
    
    public Project Project { get; set; } = null!;
    public IEnumerable<MajorTask> MajorTasks { get; set; }
}