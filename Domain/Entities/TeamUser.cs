using CrossCutting.Enum;
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
