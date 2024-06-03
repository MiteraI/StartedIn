using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CrossCutting.Enum;
using Domain.Entities.BaseEntities;

namespace Domain.Entities;

public class Post : BaseAuditEntity<string>
{
    [ForeignKey(nameof(User))]
    public string UserId { get; set; }
    public string? Content { get; set; }
    public int CommentCount { get; set; } = 0;
    public int InteractionCount { get; set; } = 0;
    public Status PostStatus { get; set; }
    public User User { get; set; } = null!;
    public IEnumerable<PostImage>? PostImages { get; set; } 
    public IEnumerable<Comment>? Comments { get; set; }
    public IEnumerable<Interaction>? Interactions { get; set; }
}