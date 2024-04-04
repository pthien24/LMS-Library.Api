using LMS_Library.Api.Data.Models;
using LMS_Library.Models;
using LMS_Library.Service.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LMS_Library.Service.Services
{
    public class EnrollmentManagment : IEnrollmentManagment
    {
        private readonly DataBaseContext _context;

        public EnrollmentManagment(DataBaseContext context)
        {
            _context = context;
        }
        public async Task<ApiResponse<string>> CreateEnrollmentAsync(int courseId, string userId)
        {

            if (_context.Enrollments.Any(e => e.CourseId == courseId && e.UserId == userId))
            {
                return new ApiResponse<string>
                {
                    Response = "User is already enrolled in this course.",
                    IsSuccess = false,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "User is already enrolled in this course."
                };
            }

            var enrollment = new Enrollment
            {
                EnrollmentDate = DateTime.Now,
                CourseId = courseId,
                UserId = userId
            };

            _context.Enrollments.Add(enrollment);
            var res = await _context.SaveChangesAsync();
            if (res > 0)
            {
                return new ApiResponse<string>
                {
                    Response = "Enrollment Created",
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.Created,
                    Message = "Enrollment Created"
                };
            }
            return new ApiResponse<string>
            {
                Response = "Enrollment Failed to Created",
                IsSuccess = false,
                StatusCode = (int)HttpStatusCode.NotFound,
                Message = "Enrollment Failed to Created"
            };
        }

        public async Task<ApiResponse<List<UserDto>>> GetEnrolledUsersAsync(int courseId)
        {
            var course = await _context.Courses.FindAsync(courseId);
            if (course == null)
            {
                return new ApiResponse<List<UserDto>>
                {
                    IsSuccess = false,
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Course not found.",
                    Response = null
                };
            }
            var enrolledUsers = await _context.Enrollments
                .Where(e => e.CourseId == courseId)
                .Include(e => e.User)
                .Select(e => new UserDto
                {
                    ID = e.User.Id,
                    Email = e.User.Email,
                    Name = e.User.UserName
                })
                .ToListAsync();

            if (enrolledUsers == null || enrolledUsers.Count == 0)
            {
                return new ApiResponse<List<UserDto>>
                {
                    IsSuccess = false,
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "No users enrolled in this course.",
                    Response = null
                };
            }

            return new ApiResponse<List<UserDto>>
            {
                IsSuccess = true,
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Enrolled users retrieved successfully.",
                Response = enrolledUsers
            };
        }
    }
}
