using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ResponseDTO
{
    public class ResponseDTO<T>
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public T? payLoad { get; set; }
    }
}
