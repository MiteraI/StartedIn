using System.ComponentModel.DataAnnotations.Schema;
using DataAccessLayer.BaseEntity;

namespace DataAccessLayer.Models;

public class Taskboard : Entity
{
    [ForeignKey(nameof(Phase))]
    public string PhaseId { get; set; }
    
    public string Title { get; set; }
    
    public IEnumerable<MinorTask> MinorTasks { get; set; }
    public Phase Phase { get; set; }
}