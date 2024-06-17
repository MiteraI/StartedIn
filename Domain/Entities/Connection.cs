using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CrossCutting.Enum;
using Domain.Entities.BaseEntities;

namespace Domain.Entities;

public class Connection : BaseEntity<string>
{
    public string SenderId { get; set; }
    public string ReceiverId { get; set; }
    public ConnectionStatus ConnectionStatus { get; set; }
    public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedTime { get; set; } = DateTimeOffset.UtcNow;
    public virtual User Sender { get; set; }
    public virtual User Receiver { get; set; }
}