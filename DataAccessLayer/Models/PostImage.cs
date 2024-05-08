using System.ComponentModel.DataAnnotations.Schema;
using DataAccessLayer.BaseEntity;

namespace DataAccessLayer.Models;

public class PostImage : Entity
{
    [ForeignKey(nameof(Post))]
    public string PostId { get; set; }
    public string ImageLink { get; set; }

    public Post Post { get; set; } = null!;
}