using DataAccessLayer.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataAccessLayer.Models;

public class Taskboard : BaseAuditEntity
{
    [ForeignKey(nameof(Phase))]
    public string PhaseId { get; set; }
    
    public string Title { get; set; }
    
    public IEnumerable<MinorTask> MinorTasks { get; set; }
    public Phase Phase { get; set; }
}