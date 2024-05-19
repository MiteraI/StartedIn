using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BaseEntities
{
    public class BaseAuditEntity : BaseEntity
    {
        protected BaseAuditEntity() { 
            CreatedTime = DateTimeOffset.UtcNow;
            LastUpdatedTime  = DateTimeOffset.UtcNow;
        }
        public DateTimeOffset CreatedTime { get; set;}
        public DateTimeOffset LastUpdatedTime { get; set;}
        public string? CreatedBy { get; set; }
        public string? LastUpdatedBy { get; set;}
        public Status? Status { get; set;}
    }
}
