using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.DTOs.RequestDTO
{
    public class TeamCreateRequestDTO
    {
        [Required(ErrorMessage = "Vui lòng nhập tên team")]
        public string TeamName { get; set; }
        public string Description { get; set; }
    }
}
