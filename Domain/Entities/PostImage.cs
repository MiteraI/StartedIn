using Domain.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class PostImage : BaseAuditEntity<string>
{
    [ForeignKey(nameof(Post))]
    public string PostId { get; set; }
    public string ImageLink { get; set; }

    public Post Post { get; set; } = null!;
}