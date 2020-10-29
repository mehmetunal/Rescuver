using System;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Rescuer.Core.Entities;
using Rescuer.Entites.Mongo;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Rescuer.Mongo.Core.Repository.Interface;

namespace Rescuer.Mongo.Core.Repository
{
    public sealed class MongoRepository<T> : IMongoRepository<T> where T : BaseEntity, IEntity
    {
        #region Fields

        /// <summary>
        /// Gets the collection
        /// </summary>
        public IMongoCollection<T> Collection { get; }

        /// <summary>
        /// Mongo Database
        /// </summary>
        public IMongoDatabase Database { get; }

        #endregion

        #region Ctor

        public MongoRepository(IMongoDatabase database)
        {
            Database = database;
            Collection = Database.GetCollection<T>(typeof(T).Name);
        }

        #endregion

        #region Methods
        public IQueryable<T> Get()
            => Collection.AsQueryable();

        public async Task<IEnumerable<T>> GetAsync()
            => await Collection.Find(_ => true).ToListAsync();

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> @where)
            => Collection.Find(where).ToList();

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> @where)
            => await Collection.Find(where).ToListAsync();

        public T Find(Expression<Func<T, bool>> @where)
            => Collection.Find(where).FirstOrDefault();

        public async Task<T> FindAsync(Expression<Func<T, bool>> @where)
            => await Collection.Find(where).FirstOrDefaultAsync();

        public T FindById(object id)
            => Collection.Find(f => f.ID == (string) id).FirstOrDefault();

        public async Task<T> FindByIdAsync(object id)
            => await Collection.Find(f => f.ID == (string) id).FirstOrDefaultAsync();

        public T Add(T entity)
        {
            Collection.InsertOne(entity);
            return entity;
        }

        public async Task<T> AddAsync(T entity)
        {
            await Collection.InsertOneAsync(entity);
            return entity;
        }

        public void AddRange(IEnumerable<T> entities)
            => Collection.InsertMany(entities);

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            var addRangeAsync = entities.ToList();
            await Collection.InsertManyAsync(addRangeAsync);
            return addRangeAsync;
        }

        public T Update(T entity)
        {
            Collection.ReplaceOne(r => r.ID == entity.ID, entity, new ReplaceOptions() {IsUpsert = false});
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            await Collection.ReplaceOneAsync(r => r.ID == entity.ID, entity, new ReplaceOptions() {IsUpsert = false});
            return entity;
        }

        public IEnumerable<T> UpdateRange(IEnumerable<T> entities)
        {
            var baseEntities = entities.ToList();
            foreach (var entity in baseEntities)
            {
                Update(entity);
            }

            return baseEntities;
        }

        public async Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities)
        {
            var updateRangeAsync = entities.ToList();
            foreach (var entity in updateRangeAsync)
            {
                await UpdateAsync(entity);
            }

            return updateRangeAsync;
        }

        public T Delete(T entity)
            => Collection.FindOneAndDelete(d => d.ID == entity.ID);

        public async Task<T> DeleteAsync(T entity)
        {
            await Collection.DeleteOneAsync(d => d.ID == entity.ID);
            return entity;
        }

        public void Delete(Expression<Func<T, bool>> @where)
            => Collection.DeleteOne(where);

        public async Task DeleteAsync(Expression<Func<T, bool>> @where)
            => await Collection.FindOneAndDeleteAsync(where);

        public void Delete(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Collection.FindOneAndDeleteAsync(d => d.ID == entity.ID);
            }
        }

        public async Task<IEnumerable<T>> DeleteAsync(IEnumerable<T> entities)
        {
            var baseEntities = entities.ToList();
            foreach (var entity in baseEntities)
            {
                await DeleteAsync(entity);
            }

            return baseEntities;
        }

        public T Delete(object id)
            => Collection.FindOneAndDelete(d => d.ID == (string) id);

        public async Task<T> DeleteAsync(object id)
            => await Collection.FindOneAndDeleteAsync(d => d.ID == (string) id);
        #endregion

        #region Properties
        public IMongoQueryable<T> Table => Collection.AsQueryable();

        public IList<T> FindByFilterDefinition(FilterDefinition<T> query)
            => Collection.Find(query).ToList();
        #endregion
    }
}