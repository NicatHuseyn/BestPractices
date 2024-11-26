using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.GenericRepositories
{
    public interface IGenericRepository<T> where T: class
    {
        #region Get Methods
        IQueryable<T> GetAll();
        Task<T> GetByIdAsync(Guid id);
        IQueryable<T> GetWhere(Expression<Func<T,bool>> expression);
        #endregion

        #region Set Methods
        ValueTask<bool> AddAsync(T model);
        bool Update(T model);
        bool Delete(T model);
        #endregion

    }
}
