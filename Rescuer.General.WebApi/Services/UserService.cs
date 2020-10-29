using Rescuer.Core.CryptoLib;
using Rescuer.Core.Extensions;
using Rescuer.DTO.Request.User;
using Rescuer.DTO.Response.User;
using Rescuer.Entites.Npgsql;
using Rescuer.Npgsql.Core.UnitOfWork.Interface;
using System;
using System.Linq;
using Rescuer.Core.IoC;

namespace Rescuer.General.WebApi.Services
{
    public class UserService : IUserService, IBaseRegisterType
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public UserResponseDTO AddNewUser(UserRequestDTO userCreate)
        {
            var user = ObjectHelper.Map<User, UserRequestDTO>(userCreate);

            user.Password = user.Password.MD5Hash();
            user.SchoolSectionUsers.Add(new SchoolSectionUser {SchoolSectionID = userCreate.SectionID});

            user = _unitOfWork.GetRepository<User>().Add(user);
            _unitOfWork.SaveChanges();

            if (user == null)
            {
                return null;
            }
            //user kayıt

            //user kayıt onaylı ise eventbus(rabbitmq) ile photo serviseni istek göndericez
            var userResponseDto = ObjectHelper.Map<UserResponseDTO, User>(user);

            return userResponseDto;
        }

        public UserResponseDTO UpdateUser(UserRequestDTO userUpdate)
        {
            var userRepo = _unitOfWork.GetRepository<User>();

            var user = userRepo.FindById(userUpdate.ID);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(userUpdate));
            }

            if (userUpdate.OldPassword.MD5Hash() == user.Password)
            {
                user.Password = userUpdate.Password.MD5Hash();
            }

            user = ObjectHelper.Map<User, UserRequestDTO>(userUpdate);

            foreach (var item in user.SchoolSectionUsers.Where(x => x.IsDeleted == false))
            {
                item.IsDeleted = true;
            }

            user.SchoolSectionUsers.Add(new SchoolSectionUser {SchoolSectionID = userUpdate.SectionID});

            user = userRepo.Update(user);
            _unitOfWork.SaveChanges();

            if (user == null)
            {
                return null;
            }
            //user kayıt

            //user kayıt onaylı ise eventbus(rabbitmq) ile photo serviseni istek göndericez
            var userResponseDto = ObjectHelper.Map<UserResponseDTO, User>(user);

            return userResponseDto;
        }
    }
}