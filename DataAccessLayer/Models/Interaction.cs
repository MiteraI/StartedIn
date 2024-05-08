using DataAccessLayer.BaseEntity;
using DataAccessLayer.Enum;

namespace DataAccessLayer.Models;

public class Interaction : Entity
{
    public InteractionType InteractionType { get; set; }
    public IEnumerable<Post> Posts { get; set; }
}