using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs.ResponseDTO
{
    public class LoginResponseDTO<T>
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string? JwtToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
