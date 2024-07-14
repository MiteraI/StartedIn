using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.DTOs.RequestDTO
{
    public class TeamAndProjectCreateDTO
    {
        public TeamCreateRequestDTO Team { get; set; }
        public ProjectCreateDTO Project { get; set; }
    }
}
