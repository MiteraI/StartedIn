using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.DTOs.ResponseDTO
{
    public class TeamWithMembersResponseDTO : IdentityResponseDTO
    {
        public string TeamName { get; set; }
        public List<TeamMemberResponseDTO> TeamMembers { get; set; }
    }
}
