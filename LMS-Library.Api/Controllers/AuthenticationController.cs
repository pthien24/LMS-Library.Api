using FLMS_Library.Api.Data.Models;
using LMS_Library.Models;
using LMS_Library.Service.Models;
using LMS_Library.Service.Models.Authentication.Login;
using LMS_Library.Service.Models.Authentication.SignUp;
using LMS_Library.Service.Models.Authentication.User;
using LMS_Library.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;


namespace FDS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserManagement _user;

        public AuthenticationController(UserManager<ApplicationUser> userManager, IUserManagement user)
        {
            _userManager = userManager;
            _user = user;
        }


        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<UserDto>>>> GetUsers()
        {
            var response = await _user.GetUsersAsync();

            if (!response.IsSuccess)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser registerUser)
        {
            if (registerUser == null)
            {
                return BadRequest();
            }
            var tokenResponse = await _user.CreateUserAsync(registerUser);

            if (tokenResponse.IsSuccess && tokenResponse.Response != null)
            {
                if (registerUser.Roles != null && tokenResponse.Response.User !=null)
                {
                    await _user.AsignRoleAsync(registerUser.Roles, tokenResponse.Response.User);
                }
                return Ok(tokenResponse);
                    
            }
            return BadRequest(tokenResponse);
                
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var jwt = await _user.LoginUserWithJWTokenAsync(loginModel);
            if (jwt.IsSuccess)
            {
                return Ok(jwt);
            }
            return BadRequest(jwt); 
        }
        [HttpPost]
        [Route("Refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenModel token)
        {
            var jwt = await _user.RenewAccessToken(token);
            if (jwt.IsSuccess)
            {
                return Ok(jwt);
            }
            return BadRequest(jwt);
        }
    }
}
