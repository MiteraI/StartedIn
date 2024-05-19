using System.ComponentModel.DataAnnotations.Schema;
using Domain.BaseEntities;

namespace Domain.Models;

public class MajorTask : BaseAuditEntity
{
    [ForeignKey(nameof(Phase))]
    public string PhaseId { get; set; }
    public string TaskTitle { get; set; }
    public string Description { get; set; }
    
    public Phase Phase { get; set; } = null!;

    public IEnumerable<MinorTask> MinorTasks { get; set; } = null!;
}