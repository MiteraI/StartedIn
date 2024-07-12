using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.DTOs.ResponseDTO
{
    public class TeamLeaderResponseDTO : IdentityResponseDTO
    {
        public string Email { get; set; }
        public string FullName { get; set; }
    }
}
