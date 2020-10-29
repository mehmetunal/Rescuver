using System;

namespace Rescuer.Entites.Npgsql
{
    public class SchoolSectionUser : BaseEntity
    {
        public Guid UserID { get; set; }
        public Guid SchoolSectionID { get; set; }



        public virtual SchoolSection SchoolSectionSchool { get; set; }
        public virtual User User { get; set; }
    }
}
