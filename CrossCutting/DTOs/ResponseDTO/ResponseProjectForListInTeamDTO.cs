using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.DTOs.ResponseDTO
{
    public class ResponseProjectForListInTeamDTO : IdentityResponseDTO
    {
        public string ProjectName { get; set; }
    }
}
