using Domain.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Entities;

public class Taskboard : BaseAuditEntity<string>
{
    public int Position { get; set; }
    [ForeignKey(nameof(Phase))]
    public string PhaseId { get; set; }
    
    public string Title { get; set; }
    
    public ICollection<MinorTask> MinorTasks { get; set; }
    public Phase Phase { get; set; }
}