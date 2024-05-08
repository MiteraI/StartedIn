using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.BaseEntity;

public class Entity
{
    protected Entity()
    {
        Id = Guid.NewGuid().ToString("N");
        CreatedTime = DateTime.Now;
        LastUpdatedTime = DateTime.Now;
    }
    [Key]
    public string Id { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime LastUpdatedTime { get; set; }
}