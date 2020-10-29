using System;

namespace Rescuer.Entites.Npgsql
{
    public class SchoolSection : BaseEntity
    {
        public Guid SchoolID { get; set; }
        public Guid SectionID { get; set; }

        public virtual School School { get; set; }
        public virtual Section Section { get; set; }
    }
}
