using DataAccessLayer.DTOs.RequestDTO;
using DataAccessLayer.DTOs.ResponseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IUserService
    {
        Task<LoginResponseDTO<string>> Login(LoginDTO loginDto);

        //Task<ResponseDTO<string>> Register(RegisterDTO registerDto);
    }
}
