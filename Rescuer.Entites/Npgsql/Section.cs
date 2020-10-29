using System.Collections.Generic;

namespace Rescuer.Entites.Npgsql
{
    public class Section : BaseEntity
    {
        public Section()
        {
            SchoolSections = new HashSet<SchoolSection>();
        }
        public string SectionName { get; set; }

        public virtual ICollection<SchoolSection> SchoolSections { get; set; }
    }
}
