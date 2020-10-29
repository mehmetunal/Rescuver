using Rescuer.DTO.Response.Token;
using Rescuer.DTO.Token;
using Rescuer.DTO.User;

namespace Rescuer.Framework.Security.Token
{
    public interface IAccessTokenHandler
    {
        /// <summary>
        /// Token Almak İçin
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        AccessTokenDTO CreateAccessToken(UserDTO user);

        UserDTO GetAccessToken();
    }
}
