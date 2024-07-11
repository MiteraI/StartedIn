using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.DTOs.ResponseDTO
{
    public class ProjectResponseDTO
    {
        public string Id { get; set; }
        public string ProjectName { get; set; }
        public string TeamId { get; set; }
        public ProjectLeaderResponseDTO Leader { get; set; }
        public IEnumerable<PhaseResponseDTO> Phases { get; set; }
    }
}
