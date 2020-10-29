using Rescuer.Entites.Npgsql;
using System;

namespace Rescuer.Identity.WebApi.Services
{
    public interface IUsersServices
    {
        void SaveRefreshToken(Guid userID, string refreshToken);
        void RevokeRefreshToken(UserToken userToken);
        User FindByEmailAndPassword(string email, string password);
        User GetUserWithRefreshToken(string refreshToken);
    }
}
