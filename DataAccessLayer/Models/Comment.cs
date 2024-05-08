using System.ComponentModel.DataAnnotations.Schema;
using DataAccessLayer.BaseEntity;

namespace DataAccessLayer.Models;

public class Comment : Entity
{
    [ForeignKey(nameof(Account))]
    public string AccountId { get; set; }
    public string CommentContent { get; set; }
    [ForeignKey(nameof(Post))]
    public string PostId { get; set; }
    
    public Post Post { get; set; } = null!;
    public Account Account { get; set; } = null!;
}