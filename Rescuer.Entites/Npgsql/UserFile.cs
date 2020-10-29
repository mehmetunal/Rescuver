using System;

namespace Rescuer.Entites.Npgsql
{
    public class UserFile : BaseEntity
    {
        public Guid UserID { get; set; }
        public string FileObjectID { get; set; }

        public virtual User User { get; set; }
    }
}
