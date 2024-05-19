using System.ComponentModel.DataAnnotations.Schema;
using Domain.BaseEntities;

namespace Domain.Models;

public class Project : BaseAuditEntity
{
    public string ProjectName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int EstimateDuration { get; set; }
    public int ActualDuration { get; set; }
    public decimal ActualCost { get; set; }
    public float Progress { get; set; }
    
    [ForeignKey(nameof(Team))]
    public string TeamId { get; set; }

    public Team Team { get; set; } = null!;
    
    public IEnumerable<Phase> Phases { get; set; }
}