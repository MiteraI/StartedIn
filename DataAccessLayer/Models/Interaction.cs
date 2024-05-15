using DataAccessLayer.BaseEntities;
using DataAccessLayer.Enum;

namespace DataAccessLayer.Models;

public class Interaction : BaseAuditEntity
{
    public InteractionType InteractionType { get; set; }
    public IEnumerable<Post> Posts { get; set; }
}