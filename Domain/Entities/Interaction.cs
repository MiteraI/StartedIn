using Domain.Entity.BaseEntities;
using Domain.Enum;

namespace Domain.Entities;

public class Interaction : BaseAuditEntity<string>
{
    public InteractionType InteractionType { get; set; }
    public IEnumerable<Post> Posts { get; set; }
}