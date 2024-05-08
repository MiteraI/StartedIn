using System.ComponentModel.DataAnnotations.Schema;
using DataAccessLayer.BaseEntity;

namespace DataAccessLayer.Models;

public class MinorTask : Entity
{
    public string TaskTitle { get; set; }
    public string Description { get; set; }
    
    [ForeignKey(nameof(MajorTask))]
    public string MajorTaskId { get; set; }
    
    public MajorTask MajorTask { get; set; } = null!;
    public Taskboard Taskboard { get; set; } = null!;
}