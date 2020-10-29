using System;
using System.Linq;
using Rescuer.Core.Entities;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Rescuer.Core.Repository
{
    public interface IRepository<T> where T : IEntity
    {
        #region GET
        IQueryable<T> Get();
        Task<IEnumerable<T>> GetAsync();

        IEnumerable<T> FindAll(Expression<Func<T, bool>> where);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> where);

        T Find(Expression<Func<T, bool>> where);
        Task<T> FindAsync(Expression<Func<T, bool>> where);

        T FindById(object id);
        Task<T> FindByIdAsync(object id);
        #endregion

        #region ADD
        T Add(T entity);
        Task<T> AddAsync(T entity);
        void AddRange(IEnumerable<T> entities);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        #endregion

        #region UPDATE
        T Update(T entity);
        Task<T> UpdateAsync(T entity);
        IEnumerable<T> UpdateRange(IEnumerable<T> entities);
        Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities);
        #endregion

        #region DELETE
        T Delete(T entity);
        Task<T> DeleteAsync(T entity);

        void Delete(Expression<Func<T, bool>> where);
        Task DeleteAsync(Expression<Func<T, bool>> where);

        void Delete(IEnumerable<T> entities);
        Task<IEnumerable<T>> DeleteAsync(IEnumerable<T> entities);

        T Delete(object id);
        Task<T> DeleteAsync(object id);
        #endregion
    }
}