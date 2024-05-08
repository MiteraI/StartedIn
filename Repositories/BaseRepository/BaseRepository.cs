using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;

namespace Repositories.BaseRepository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private DbSet<T> _dbSet;

        public BaseRepository()
        {
            _context = new AppDbContext();
            _dbSet = _context.Set<T>();
        }

        public bool Add(T entity)
        {
            try
            {
                _dbSet.Add(entity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public virtual IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public bool Update(T entity)
        {
            try
            {
                _dbSet.Update(entity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public AppDbContext GetContext()
        {
            return _context;
        }
    }
}
