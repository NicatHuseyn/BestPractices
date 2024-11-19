using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Repositories.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.GenericRepositories
{
    public class GenericRepository<T>(AppDbContext context) : IGenericRepository<T> where T : class
    {
        protected AppDbContext Context = context;
        private readonly DbSet<T> _dbSet = context.Set<T>();


        #region Get Methods
        public IQueryable<T> GetAll()=> _dbSet.AsQueryable().AsNoTracking();

        public async Task<T?> GetByIdAsync(string id)=> await _dbSet.FindAsync(id);

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> expression)=> _dbSet.Where(expression).AsQueryable().AsNoTracking();
        #endregion


        #region Set Methods
        public async ValueTask<bool> AddAsync(T model)
        {
            EntityEntry<T> entry = await _dbSet.AddAsync(model);
            return entry.State == EntityState.Added;
        }

        public bool Delete(T model)
        {
            EntityEntry<T> entry = _dbSet.Remove(model);
            return entry.State == EntityState.Deleted;
        }

        public bool Update(T model)
        {
            EntityEntry<T> entry = _dbSet.Update(model);
            return entry.State == EntityState.Modified;
        }
        #endregion

        
    }
}
