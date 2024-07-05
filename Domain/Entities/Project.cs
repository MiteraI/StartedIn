using Domain.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Project : BaseAuditEntity<string>
{
    public string ProjectName { get; set; }
    
    [ForeignKey(nameof(Team))]
    public string TeamId { get; set; }

    public Team Team { get; set; } = null!;
    
    public IEnumerable<Phase> Phases { get; set; }
}