using System.Linq;
using Rescuer.Entites.Npgsql;
using System.Threading.Tasks;
using Rescuer.Core.Entities;
using Rescuer.Core.Repository;

namespace Rescuer.Npgsql.Core.Respository.Interface
{
    public interface INpgsqlRepository<T> : IRepository<T> where T : BaseEntity, IEntity
    {
        IQueryable<T> FromSqlRaw(string sql, params object[] par);
        int Execute(string sql, params object[] par);
        Task<int> ExecuteAsync(string sql, params object[] par);
    }
}