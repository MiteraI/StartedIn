using Domain.BaseEntities;
using Domain.Enum;

namespace Domain.Models;

public class Interaction : BaseAuditEntity
{
    public InteractionType InteractionType { get; set; }
    public IEnumerable<Post> Posts { get; set; }
}