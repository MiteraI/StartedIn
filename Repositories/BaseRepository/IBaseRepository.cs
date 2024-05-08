using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.BaseRepository
{
    public interface IBaseRepository<T>
    {
        IQueryable<T> GetAll();
        bool Add(T entity);
        bool Update(T entity);
        void Delete(T entity);
    }
}
