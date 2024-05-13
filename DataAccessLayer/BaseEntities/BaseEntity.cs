using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.BaseEntities;

public class BaseEntity
{
    protected BaseEntity()
    {
        Id = Guid.NewGuid().ToString("N");
    }
    [Key]
    public string Id { get; set; }
}