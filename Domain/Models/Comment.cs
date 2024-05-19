using System.ComponentModel.DataAnnotations.Schema;
using Domain.BaseEntities;

namespace Domain.Models;

public class Comment : BaseAuditEntity
{
    [ForeignKey(nameof(User))]
    public string UserId { get; set; }
    public string CommentContent { get; set; }
    [ForeignKey(nameof(Post))]
    public string PostId { get; set; }
    
    public Post Post { get; set; } = null!;
    public User User { get; set; } = null!;
}