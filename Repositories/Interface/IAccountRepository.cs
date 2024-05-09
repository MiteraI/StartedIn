using DataAccessLayer.Models;
using Repositories.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
        Task<Account?> Login (string email, string password);
    }
}
