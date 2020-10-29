using System;

namespace Rescuer.Entites.Npgsql
{
    public class UserToken : BaseEntity
    {
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
        public Guid UserID { get; set; }

        public virtual User User { get; set; }
    }
}
