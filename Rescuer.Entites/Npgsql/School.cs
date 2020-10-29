using System;
using System.Collections.Generic;

namespace Rescuer.Entites.Npgsql
{
    public class School : BaseEntity
    {
        public School()
        {
            SchoolSections = new HashSet<SchoolSection>();
        }

        public string SchoolName { get; set; }

        public virtual ICollection<SchoolSection> SchoolSections { get; set; }
    }
}
