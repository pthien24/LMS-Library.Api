using LMS_Library.Api.Data.Models;
using LMS_Library.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseManagment _courseManagement;

        public CourseController(ICourseManagment courseManagement)
        {
            _courseManagement = courseManagement;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _courseManagement.GetAllCoursesAsync();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            var course = await _courseManagement.GetCourseByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(Course course)
        {
            var response = await _courseManagement.CreateCourseAsync(course);
            if (response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, Course course)
        {
            var existingCourse = await _courseManagement.GetCourseByIdAsync(id);
            if (existingCourse.IsSuccess == false)
            {
                return BadRequest(existingCourse);
            }
            var updatedCourse = await _courseManagement.UpdateCourseAsync(id, course);
            return Ok(updatedCourse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var res = await _courseManagement.DeleteCourseAsync(id);
            return Ok(res);
        }
    }
}
