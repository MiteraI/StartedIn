using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.BaseEntities;

namespace Domain.Entities;

public class Phase : BaseAuditEntity<string>
{
    public int Position { get; set; }
    public string PhaseName { get; set; }
    
    [ForeignKey(nameof(Project))]
    public string ProjectId { get; set; }
    
    public Project Project { get; set; } = null!;
    public IEnumerable<MajorTask> MajorTasks { get; set; }
}