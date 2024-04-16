using weblog.CoreLayer.Utilities;
using weblog.CoreLayer.DTOs.User;

namespace weblog.CoreLayer.Services.User
{
    public interface IUserService
    {
        OperationResult RegisterUser(UserRegisterDto register);
        UserDTO LoginUser(LoginUserDto loginDto);

    }
}
