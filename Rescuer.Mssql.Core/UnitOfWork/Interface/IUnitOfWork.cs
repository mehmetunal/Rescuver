using System;
using System.Threading.Tasks;
using Rescuer.Core.Entities;
using Rescuer.Entites.Npgsql;
using Rescuer.Npgsql.Core.Respository.Interface;

namespace Rescuer.Npgsql.Core.UnitOfWork.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        INpgsqlRepository<T> GetRepository<T>() where T : BaseEntity, IEntity;

        bool BeginNewTransaction();
        Task<bool> BeginNewTransactionAsync();
        bool RollBackTransaction();
        Task<bool> RollBackTransactionAsync();

        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
