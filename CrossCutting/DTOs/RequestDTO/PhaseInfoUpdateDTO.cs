using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.DTOs.RequestDTO
{
    public class PhaseInfoUpdateDTO
    {
        [Required(ErrorMessage = "Vui lòng điền tên giai đoạn")]
        public string PhaseName { get; set; }
    }
}
