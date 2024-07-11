using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.DTOs.ResponseDTO
{
    public class TeamResponseDTO
    {
        public string Id { get; set; }
        public string TeamName { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public string? CreatedBy { get; set; }
        public IEnumerable<string> Users { get; set; }
        public IEnumerable<ProjectResponseDTO> Projects { get; set; }
    }
}
