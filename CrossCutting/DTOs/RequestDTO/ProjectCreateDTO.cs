using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.DTOs.RequestDTO
{
    public class ProjectCreateDTO
    {
        [Required(ErrorMessage = "Vui lòng điền tên dự án")]
        public string ProjectName { get; set; }
    }
}
