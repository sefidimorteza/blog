using weblog.DataLayer.Entities;

namespace weblog.CoreLayer.DTOs.User
{
    public class UserDTO
    {
        public int userId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public DateTime RegisteDate { get; set; }
    }
}
