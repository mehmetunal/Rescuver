using Rescuer.DTO.Request.User;
using Rescuer.DTO.Response.User;

namespace Rescuer.General.WebApi.Services
{
    public interface IUserService
    {
        UserResponseDTO AddNewUser(UserRequestDTO userCreate);
        UserResponseDTO UpdateUser(UserRequestDTO userUpdate);
    }
}
