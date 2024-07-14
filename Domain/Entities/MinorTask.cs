using Domain.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class MinorTask : BaseAuditEntity<string>
{
    public int Position { get; set; }
    public string TaskTitle { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    
    [ForeignKey(nameof(MajorTask))]
    public string? MajorTaskId { get; set; }
    
    [ForeignKey(nameof(Taskboard))]
    public string TaskboardId { get; set; }
    public MajorTask MajorTask { get; set; }
    public Taskboard Taskboard { get; set; } = null!;
}