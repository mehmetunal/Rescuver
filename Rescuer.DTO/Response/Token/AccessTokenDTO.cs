using Rescuer.DTO.User;
using System;

namespace Rescuer.DTO.Response.Token
{
    public class AccessTokenDTO
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
        public UserDTO User { get; set; }
    }
}
