using System;
using Rescuer.Core.Entities;

namespace Rescuer.Entites.Npgsql
{
    public abstract class BaseEntity : IEntity
    {
        protected BaseEntity()
        {
            this.ID = Guid.NewGuid();
            this.CreatedDate = DateTime.UtcNow;
            this.CreatedUserID = Guid.Empty;
            this.IsActive = true;
            this.IsDeleted = false;
        }
        public Guid ID { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid CreatedUserID { get; set; }
        public Guid? UpdatedUserID { get; set; }
    }
}
