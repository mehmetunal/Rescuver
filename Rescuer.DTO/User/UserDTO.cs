using System;
using System.Collections.Generic;
using System.Text;

namespace Rescuer.DTO.User
{
    public class UserDTO
    {
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
