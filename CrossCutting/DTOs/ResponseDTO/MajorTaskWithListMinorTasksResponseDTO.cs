using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.DTOs.ResponseDTO
{
    public class MajorTaskWithListMinorTasksResponseDTO
    {
        public MajorTaskResponseDTO MajorTask { get; set; }
        public List<MinorTaskResponseDTO> MinorTasks { get; set; }
    }
}
