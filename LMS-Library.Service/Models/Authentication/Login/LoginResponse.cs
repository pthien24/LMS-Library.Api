
using LMS_Library.Models;
using LMS_Library.Service.Models.Authentication.User;

namespace LMS_Library.Service.Models.Authentication.Login
{
    public class LoginResponse
    {
        public UserDto? User { get; set; }
        public TokenType? Token { get; set; }
        public TokenType? RefreshToken { get; set; }
    }
}
