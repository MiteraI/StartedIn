using Domain.Entity.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class MajorTask : BaseAuditEntity<string>
{
    [ForeignKey(nameof(Phase))]
    public string PhaseId { get; set; }
    public string TaskTitle { get; set; }
    public string Description { get; set; }
    
    public Phase Phase { get; set; } = null!;

    public IEnumerable<MinorTask> MinorTasks { get; set; } = null!;
}