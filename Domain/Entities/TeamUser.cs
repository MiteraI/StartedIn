using CrossCutting.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TeamUser
    {
        public string TeamId { get; set; }
        public string UserId { get; set; }
        public virtual Team Team { get; set; }
        public virtual User User { get; set; }
        public RoleInTeam RoleInTeam { get; set; }
    }
}
