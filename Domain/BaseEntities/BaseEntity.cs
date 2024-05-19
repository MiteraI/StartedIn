using System.ComponentModel.DataAnnotations;

namespace Domain.BaseEntities;

public class BaseEntity
{
    protected BaseEntity()
    {
        Id = Guid.NewGuid().ToString("N");
    }
    [Key]
    public string Id { get; set; }
}