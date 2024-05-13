using DataAccessLayer.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class RefreshToken : BaseAuditEntity
    {
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
        public DateTimeOffset Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public bool IsActive => !IsExpired;
    }
}
