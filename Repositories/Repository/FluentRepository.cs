using Microsoft.EntityFrameworkCore;
using Repositories.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class FluentRepository<TEntity> : IFluentRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet;
        private List<Expression<Func<TEntity, object>>> _includeProperties;
        private Expression<Func<TEntity, bool>> _filter;
        private Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> _orderBy;
        private bool _disableTracking;

        public FluentRepository(DbSet<TEntity> dbset)
        {
            _dbSet = dbset;
            _includeProperties = new List<Expression<Func<TEntity, object>>>();
        }

        public IFluentRepository<TEntity> AsNoTracking()
        {
            _disableTracking = true;
            return this;
        }

        public IFluentRepository<TEntity> Filter(Expression<Func<TEntity, bool>> filter)
        {
            _filter = filter;
            return this;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            IQueryable<TEntity> query = BuildQuery();
            return await query.ToListAsync();
        }

        public async Task<TEntity?> GetOneAsync()
        {
            IQueryable<TEntity> query = BuildQuery();
            return await query.SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> GetPagingAsync(int position, int size)
        {
            IQueryable<TEntity> query = BuildQuery();
            return await query.Skip(position).Take(size).ToListAsync();
        }

        public IFluentRepository<TEntity> Include(Expression<Func<TEntity, object>> expression)
        {
            _includeProperties.Add(expression);
            return this;
        }

        public IFluentRepository<TEntity> OrderBy(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            _orderBy = orderBy;
            return this;
        }

        private IQueryable<TEntity> BuildQuery()
        {
            IQueryable<TEntity> query = _dbSet;

            if (_disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (_includeProperties != null)
            {
                _includeProperties.ForEach(i => { query = query.Include(i); });
            }

            if (_filter != null)
            {
                query = query.Where(_filter);
            }

            if (_orderBy != null)
            {
                query = _orderBy(query);
            }

            return query;
        }
    }
}
