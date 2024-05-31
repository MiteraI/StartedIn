using Domain.Entities.BaseEntities;
using CrossCutting.Enum;

namespace Domain.Entities;

public class Interaction : BaseAuditEntity<string>
{
    public InteractionType InteractionType { get; set; }
    public IEnumerable<Post> Posts { get; set; }
}