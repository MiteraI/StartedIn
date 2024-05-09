using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs.ResponseDTO
{
    public class ResponseDTO<T>
    {
        public int statusCode { get; set; }
        public string? message { get; set; }
        public T? payLoad { get; set; }
    }
}
