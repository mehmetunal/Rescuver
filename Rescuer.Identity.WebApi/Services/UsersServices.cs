using Microsoft.Extensions.Options;
using Rescuer.Core.CryptoLib;
using Rescuer.DTO.Token;
using Rescuer.Entites.Npgsql;
using Rescuer.Npgsql.Core.UnitOfWork.Interface;
using System;
using Rescuer.Core.IoC;

namespace Rescuer.Identity.WebApi.Services
{
    public class UsersServices : IUsersServices
    {
        private readonly ApiTokenOptions _tokenOptions;
        private readonly IUnitOfWork _unitOfWork;

        public UsersServices(IOptions<ApiTokenOptions> tokenOptions,
            IUnitOfWork unitOfWork)
        {
            _tokenOptions = tokenOptions?.Value;
            _unitOfWork = unitOfWork;
        }

        public User FindByEmailAndPassword(string email, string password)
            => _unitOfWork.GetRepository<User>().Find(f => f.Email == email && f.Password == password.MD5Hash());

        public User GetUserWithRefreshToken(string refreshToken)
            => _unitOfWork.GetRepository<UserToken>().Find(f => f.RefreshToken == refreshToken)?.User;

        public void RevokeRefreshToken(UserToken userToken)
        {
            userToken.RefreshToken = null;
            userToken.RefreshTokenEndDate = null;
            _unitOfWork.GetRepository<UserToken>().Update(userToken);
        }

        public void SaveRefreshToken(Guid userID, string refreshToken)
        {
            var user = _unitOfWork.GetRepository<User>().FindById(userID);
            if (user == null)
            {
                throw new ArgumentNullException("Users not found");
            }

            var userToken = _unitOfWork.GetRepository<UserToken>().Find(f => f.UserID == userID);

            if (userToken == null)
            {
                userToken = new UserToken
                {
                    UserID = userID,
                    CreatedUserID = userID,
                    RefreshToken = refreshToken,
                    RefreshTokenEndDate = DateTime.Now.AddDays(_tokenOptions.RefreshTokenExpiration)
                };

                _unitOfWork.GetRepository<UserToken>().Add(userToken);
            }
            else
            {
                userToken.UpdatedUserID = userID;
                userToken.RefreshToken = refreshToken;
                userToken.RefreshTokenEndDate = DateTime.Now.AddDays(_tokenOptions.RefreshTokenExpiration);

                _unitOfWork.GetRepository<UserToken>().Update(userToken);
            }

            _unitOfWork.SaveChanges();
        }
    }
}