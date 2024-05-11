using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.BaseRepository;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public async Task<Account?> Login(string email, string password)
        {
            return await GetAll().FirstOrDefaultAsync(a => a.Email.Equals(email) && a.Password.Equals(password));  
        }
    }
}
