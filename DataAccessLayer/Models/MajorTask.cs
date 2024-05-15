using System.ComponentModel.DataAnnotations.Schema;
using DataAccessLayer.BaseEntities;

namespace DataAccessLayer.Models;

public class MajorTask : BaseAuditEntity
{
    [ForeignKey(nameof(Phase))]
    public string PhaseId { get; set; }
    public string TaskTitle { get; set; }
    public string Description { get; set; }
    
    public Phase Phase { get; set; } = null!;

    public IEnumerable<MinorTask> MinorTasks { get; set; } = null!;
}