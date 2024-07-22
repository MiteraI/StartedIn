using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.DTOs.RequestDTO
{
    public class TaskboardInfoUpdateDTO
    {
        [Required(ErrorMessage = "Vui lòng điền tên bảng công việc")]
        public string Title { get; set; }
    }
}
