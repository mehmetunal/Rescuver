using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Rescuer.Entites;
using Rescuer.Entites.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Rescuer.Core.Entities;
using Rescuer.Core.Repository;

namespace Rescuer.Mongo.Core.Repository.Interface
{
    public interface IMongoRepository<T> : IRepository<T> where T : BaseEntity, IEntity
    {
        IMongoCollection<T> Collection { get; }

        IMongoDatabase Database { get; }


        /// <summary>
        /// Gets a table
        /// </summary>
        IMongoQueryable<T> Table { get; }

        /// <summary>
        /// Get collection by filter definitions
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IList<T> FindByFilterDefinition(FilterDefinition<T> query);
    }
}