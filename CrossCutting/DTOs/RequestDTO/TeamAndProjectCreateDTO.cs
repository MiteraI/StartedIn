using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.DTOs.RequestDTO
{
    public class TeamAndProjectCreateDTO
    {
        public TeamCreateRequestDTO TeamCreateRequestDTO { get; set; }
        public ProjectCreateDTO ProjectCreateDTO { get; set; }
    }
}
