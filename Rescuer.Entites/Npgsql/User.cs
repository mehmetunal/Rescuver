using System;
using System.Collections;
using System.Collections.Generic;

namespace Rescuer.Entites.Npgsql
{
    public class User : BaseEntity
    {
        public User()
        {
            UserFiles = new HashSet<UserFile>();
            SchoolSectionUsers = new HashSet<SchoolSectionUser>();
            UserToken = new HashSet<UserToken>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsValid { get; set; }


        public virtual ICollection<UserFile> UserFiles { get; set; }
        public virtual ICollection<SchoolSectionUser> SchoolSectionUsers { get; set; }
        public virtual ICollection<UserToken> UserToken { get; set; }
    }
}
