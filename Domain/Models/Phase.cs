using System.ComponentModel.DataAnnotations.Schema;
using Domain.BaseEntities;
using Domain.Enum;

namespace Domain.Models;

public class Phase : BaseAuditEntity
{
    public string PhaseName { get; set; }
    
    [ForeignKey(nameof(Project))]
    public string ProjectId { get; set; }
    
    public Project Project { get; set; } = null!;
    public IEnumerable<MajorTask> MajorTasks { get; set; }
}