using Rescuer.DTO.Request.Login;
using Rescuer.DTO.Response.Token;

namespace Rescuer.Identity.WebApi.Services
{
    public interface IAuthenticationService 
    {
        AccessTokenDTO CreateAccessToken(LoginDTO login);
        AccessTokenDTO CreateAccessTokenByRefreshToken(string refreshToken);
        AccessTokenDTO RevokeRefreshToken(string refreshToken);
    }
}
