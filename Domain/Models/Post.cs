using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.BaseEntities;
using Domain.Enum;

namespace Domain.Models;

public class Post : BaseAuditEntity
{
    [ForeignKey(nameof(User))]
    public string UserId { get; set; }
    public string Content { get; set; }

    public User User { get; set; } = null!;
    public IEnumerable<PostImage> PostImages { get; set; } 
    public IEnumerable<Comment> Comments { get; set; }
    public IEnumerable<Interaction> Interactions { get; set; }
}