using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccessLayer.BaseEntity;
using DataAccessLayer.Enum;

namespace DataAccessLayer.Models;

public class Post : Entity
{
    [ForeignKey(nameof(Account))]
    public string AccountId { get; set; }
    public string Content { get; set; }
    public Status Status { get; set; }

    public Account Account { get; set; } = null!;
    public IEnumerable<PostImage> PostImages { get; set; } 
    public IEnumerable<Comment> Comments { get; set; }
    public IEnumerable<Interaction> Interactions { get; set; }
}