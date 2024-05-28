using Domain.Entity.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Comment : BaseAuditEntity<string>
{
    [ForeignKey(nameof(User))]
    public string UserId { get; set; }
    public string CommentContent { get; set; }
    [ForeignKey(nameof(Post))]
    public string PostId { get; set; }
    
    public Post Post { get; set; } = null!;
    public User User { get; set; } = null!;
}