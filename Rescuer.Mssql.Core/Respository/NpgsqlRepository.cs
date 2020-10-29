using System;
using System.Linq;
using Rescuer.Entites.Npgsql;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Rescuer.Core.Entities;
using Rescuer.Npgsql.Core.Respository.Interface;

namespace Rescuer.Npgsql.Core.Respository
{
    public sealed class NpgsqlRepository<T> : INpgsqlRepository<T> where T : BaseEntity, IEntity
    {
        #region Variables

        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        #endregion

        #region Constructor

        public NpgsqlRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        #endregion


        #region Methos

        public IQueryable<T> Get()
            => _dbSet;

        public async Task<IEnumerable<T>> GetAsync()
            => await _dbSet.ToListAsync();

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> @where)
            => _dbSet.Where(where);

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> @where)
            => await _dbSet.Where(where).ToListAsync();

        public T Find(Expression<Func<T, bool>> @where)
            => _dbSet.FirstOrDefault(where);

        public async Task<T> FindAsync(Expression<Func<T, bool>> @where)
            => await _dbSet.FirstOrDefaultAsync(where);

        public T FindById(object id)
            => _dbSet.Find(id);

        public async Task<T> FindByIdAsync(object id)
            => await _dbSet.FindAsync(id);

        public T Add(T entity)
            => _dbSet.Add(entity).Entity;

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public void AddRange(IEnumerable<T> entities)
            => _dbSet.AddRange(entities);

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            var addRangeAsync = entities.ToList();
            await _dbSet.AddRangeAsync(addRangeAsync);
            return addRangeAsync;
        }

        public T Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            return await Task.Run(() =>
            {
                _dbSet.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
                return entity;
            });
        }

        public IEnumerable<T> UpdateRange(IEnumerable<T> entities)
        {
            var baseEntities = entities.ToList();
            _dbSet.UpdateRange(baseEntities);
            return baseEntities;
        }

        public async Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities)
        {
            return await Task.Run(() =>
            {
                var baseEntities = entities.ToList();
                _dbSet.UpdateRange(baseEntities);
                return baseEntities;
            });
        }

        public T Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Attach(entity);
            }

            var remove = _dbSet.Remove(entity);
            return remove.Entity;
        }

        public async Task<T> DeleteAsync(T entity)
        {
            return await Task.Run(() =>
            {
                if (_context.Entry(entity).State == EntityState.Detached)
                {
                    _context.Attach(entity);
                }

                var remove = _dbSet.Remove(entity);
                return remove.Entity;
            });
        }

        public void Delete(Expression<Func<T, bool>> @where)
            => Delete(Find(where));

        public async Task DeleteAsync(Expression<Func<T, bool>> @where)
            => await DeleteAsync(await FindAsync(where));

        public void Delete(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task<IEnumerable<T>> DeleteAsync(IEnumerable<T> entities)
        {
            return await Task.Run(() =>
            {
                var baseEntities = entities.ToList();
                _dbSet.RemoveRange(baseEntities);
                return baseEntities;
            });
        }

        public T Delete(object id)
            => Delete(FindById(id));

        public async Task<T> DeleteAsync(object id)
            => await DeleteAsync(await FindByIdAsync(id));

        #endregion


        public IQueryable<T> FromSqlRaw(string sql, params object[] par)
            => _dbSet.FromSqlRaw(sql, par);

        public int Execute(string sql, params object[] par)
            => _context.Database.ExecuteSqlRaw(sql, par);

        public async Task<int> ExecuteAsync(string sql, params object[] par)
            => await _context.Database.ExecuteSqlRawAsync(sql, par);
    }
}