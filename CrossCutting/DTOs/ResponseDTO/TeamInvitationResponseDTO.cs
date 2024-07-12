using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.DTOs.ResponseDTO
{
    public class TeamInvitationResponseDTO : IdentityResponseDTO
    {
        public string TeamName { get; set; }
        public TeamLeaderResponseDTO Leader { get; set; }
    }
}
