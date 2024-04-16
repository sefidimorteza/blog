using weblog.CoreLayer.Utilities;
using weblog.CoreLayer.DTOs.User;
using weblog.DataLayer.Context;
using weblog.DataLayer.Entities;

namespace weblog.CoreLayer.Services.User
{
    public class UserService : IUserService
    {
        //inject kardan DataBase
        private readonly BlogContext _context;
        public UserService(BlogContext context)
        {
            _context = context;
        }

        public UserDTO? LoginUser(LoginUserDto loginDto)
        {
            var passwordHash = loginDto.Password.EncodeToMd5();
            var user = _context.Users.FirstOrDefault(u =>
            u.UserName == loginDto.UserName &&
            u.Password == passwordHash);
            if (user == null)
                return null;
            var userDto = new UserDTO()
            {
                UserName = user.UserName,
                Password = user.Password,
                Role = user.Role,
                FullName = user.FullName,
                RegisteDate = user.CreationDate,
                userId = user.Id
            };
            return userDto;

        }
        public OperationResult RegisterUser(UserRegisterDto RegisterDto)
        {
            var isUserExist = _context.Users.Any(u => u.UserName == RegisterDto.UserName);
            if (isUserExist)
                return OperationResult.Error("نام کاربری تکراری است");

            var paswordHash = RegisterDto.Password.EncodeToMd5();
            _context.Users.Add(new DataLayer.Entities.User
            {
                FullName = RegisterDto.FullName,
                IsDelete = false,
                UserName = RegisterDto.UserName,
                Role = UserRole.User,
                CreationDate = DateTime.Now,
                Password = paswordHash
            });
            _context.SaveChanges();
            return OperationResult.Success();
        }
    }
}
