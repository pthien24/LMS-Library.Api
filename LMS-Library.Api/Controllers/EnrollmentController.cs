using LMS_Library.Api.Data.Models;
using LMS_Library.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {

        private readonly IEnrollmentManagment _enrollment;

        public EnrollmentController(IEnrollmentManagment enrollment)
        {
            _enrollment = enrollment;
        }

        [HttpPost]
        public async Task<IActionResult> EnrollmentCourse(int courseId, string userId)
        {
            var response = await _enrollment.CreateEnrollmentAsync(courseId,userId);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetEnrolledUsers(int courseId)
        {
            var response = await _enrollment.GetEnrolledUsersAsync(courseId);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
