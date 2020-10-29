using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rescuer.DTO.Request.User
{
    public class UserRequestDTO
    {
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string OldPassword { get; set; }

        public Guid SectionID { get; set; }
         
        public IFormFile UserAvatar { get; set; }
    }
}
