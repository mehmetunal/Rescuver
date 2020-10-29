using Rescuer.Core.Extensions;
using Rescuer.DTO.Request.Login;
using Rescuer.DTO.Response.Token;
using Rescuer.DTO.User;
using Rescuer.Entites.Npgsql;
using Rescuer.Framework.Security.Token;
using System;
using System.Linq;

namespace Rescuer.Identity.WebApi.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAccessTokenHandler _tokenHandler;
        private readonly IUsersServices _usersServices;

        public AuthenticationService(IUsersServices usersServices,
            IAccessTokenHandler tokenHandler)
        {
            _usersServices = usersServices;
            _tokenHandler = tokenHandler;
        }

        public AccessTokenDTO CreateAccessToken(LoginDTO login)
        {
            try
            {
                var user = _usersServices.FindByEmailAndPassword(login.Email, login.Password);
                if (user == null)
                {
                    throw new ArgumentNullException($"User not found");
                }

                var userDto = ObjectHelper.Map<UserDTO, User>(user);

                var newToken = _tokenHandler.CreateAccessToken(userDto);
                if (newToken == null)
                {
                    throw new ArgumentNullException($"accessToken");
                }

                _usersServices.SaveRefreshToken(user.ID, newToken.RefreshToken);

                return newToken;
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
        }

        public AccessTokenDTO CreateAccessTokenByRefreshToken(string refreshToken)
        {
            var user = _usersServices.GetUserWithRefreshToken(refreshToken);
            if (user == null)
            {
                throw new ArgumentNullException($"Token Not Found ");
            }

            var userToken = user.UserToken?.LastOrDefault();
            if (userToken != null && userToken.RefreshTokenEndDate < DateTime.UtcNow)
            {
                var userDto = ObjectHelper.Map<UserDTO, User>(user);
                var accessToken = _tokenHandler.CreateAccessToken(userDto);

                _usersServices.SaveRefreshToken(userDto.ID, accessToken.RefreshToken);
                return accessToken;
            }

            throw new ArgumentNullException($"RefreshToken expired");
        }

        public AccessTokenDTO RevokeRefreshToken(string refreshToken)
        {
            var user = _usersServices.GetUserWithRefreshToken(refreshToken);
            if (user == null)
            {
                throw new ArgumentNullException($"Token Not Found");
            }

            _usersServices.RevokeRefreshToken(user.UserToken?.LastOrDefault());
            return new AccessTokenDTO();
        }
    }
}