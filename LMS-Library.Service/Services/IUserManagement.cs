

using FLMS_Library.Api.Data.Models;
using LMS_Library.Models;
using LMS_Library.Service.Models;
using LMS_Library.Service.Models.Authentication.Login;
using LMS_Library.Service.Models.Authentication.SignUp;
using LMS_Library.Service.Models.Authentication.User;

namespace LMS_Library.Service.Services
{
    public interface IUserManagement
    {

        Task<ApplicationUser> FindUserByEmailAsync(string email);
        Task<ApiResponse<string>> SetRolesAsync(string email, List<string> roles);
        Task<ApiResponse<RoleResponse>> GetUserRolesAsync(string email);
        Task<ApiResponse<string>> RemoveRolesAsync(string email, List<string> roles);
        Task<ApiResponse<CreateUserResponse>> CreateUserAsync(RegisterUser registerUser);
        Task<ApiResponse<List<string>>> AsignRoleAsync(List<string> role, ApplicationUser user);
        Task<ApiResponse<LoginResponse>> GetJwtTokenAsync(ApplicationUser user);
        Task<ApiResponse<LoginResponse>> LoginUserWithJWTokenAsync(LoginModel loginmodel);
        Task<ApiResponse<ResetPasswordResponse>> ForgotPasswordAsync(string email);
        Task<ApiResponse<string>> ResetPasswordAsync(ResetPassword resetPassword);
        Task<ApiResponse<LoginResponse>> RenewAccessToken(RefreshTokenModel tokens);




        Task<ApiResponse<List<UserDto>>> GetUsersAsync();
    }
}
