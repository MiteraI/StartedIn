using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Role : IdentityRole
    {
        public Role() {
            Id = Guid.NewGuid().ToString("N");
        }
        [NotMapped]
        public override string ConcurrencyStamp { get => base.ConcurrencyStamp; set => base.ConcurrencyStamp = value; }
        [NotMapped]
        public override string NormalizedName { get => base.NormalizedName; set => base.NormalizedName = value; }
    }
}
