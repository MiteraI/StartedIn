using AutoMapper;
using DataAccessLayer.DTOs.RequestDTO;
using DataAccessLayer.DTOs.ResponseDTO;
using Repositories.Interface;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public AccountService(IAccountRepository accountRepository, IMapper mapper, ITokenService tokenService)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<ResponseDTO<string>> Login(LoginDTO loginDto)
        {
            var loginUser = await _accountRepository.Login(loginDto.Email ?? string.Empty, loginDto.Password ?? string.Empty);
            if (loginUser == null)
            {
                return new ResponseDTO<string> {
                    statusCode = 400,
                    message = "Wrong email or password"
                };
            }
            var jwtToken = _tokenService.CreateTokenForAccount(loginUser);
            return new ResponseDTO<string>
            {
                statusCode = 200,
                message = jwtToken
            };
        }
    }
}
