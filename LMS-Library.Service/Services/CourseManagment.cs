using LMS_Library.Api.Data.Models;
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
    public class CourseManagment : ICourseManagment
    {
        private readonly DataBaseContext _context;
        public CourseManagment(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<Course>> CreateCourseAsync(Course course)
        {
            _context.Courses.Add(course);
            var res = await _context.SaveChangesAsync();
            if (res > 0)
            {
                return new ApiResponse<Course>
                {
                    Response = course,
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.Created,
                    Message = "Course Created"
                };
            }
            return new ApiResponse<Course>
            {
                Response = null,
                IsSuccess = false,
                StatusCode = (int)HttpStatusCode.NotFound,
                Message = "Course Failed to Created"
            };
        }

        public async Task<ApiResponse<Course>> GetCourseByIdAsync(int id)
        {
            try
            {
                var courseQuery = _context.Courses.AsQueryable();

                    // Nếu includeDocuments là true, bao gồm cả danh sách các tài liệu của khóa học
                 courseQuery = courseQuery.Include(c => c.Documents);

                var course = await courseQuery.FirstOrDefaultAsync(c => c.Id == id);

                if (course == null)
                {
                    return new ApiResponse<Course>
                    {
                        Response = null,
                        IsSuccess = false,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Message = "Course not found."
                    };
                }

                return new ApiResponse<Course>
                {
                    Response = course,
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Course response ok"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Course>
                {
                    Response = null,
                    IsSuccess = false,
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = $"An error occurred while retrieving the course: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<List<Course>>> GetAllCoursesAsync()
        {
            var course = await _context.Courses.Include(c => c.Documents).ToListAsync();
            return new ApiResponse<List<Course>>
            {
                Response = course,
                IsSuccess = true,
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Course reponse Ok"
            };
        }
        public async Task<ApiResponse<Course>> UpdateCourseAsync(int id, Course updatedCourse)
        {
            var existingCourse = await _context.Courses.FindAsync(id);
            if (existingCourse == null)
            {
                return new ApiResponse<Course>
                {
                    Response = null,
                    IsSuccess = false,
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = $"Course with id {id} not found."
                };
            }

            existingCourse.Name = updatedCourse.Name;
            existingCourse.Description = updatedCourse.Description;
            existingCourse.Author = updatedCourse.Author;

            await _context.SaveChangesAsync();
            return new ApiResponse<Course>
            {
                Response = existingCourse,
                IsSuccess = true,
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Course update Ok"
            };
        }
        public async Task<ApiResponse<string>> DeleteCourseAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return new ApiResponse<string>
                {
                    Response = null,
                    IsSuccess = false,
                    StatusCode = (int)HttpStatusCode.NotFound,

                    Message = $"Course with id {id} not found.Course reponse ok"
                };
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return new ApiResponse<string>
            {
                Response = null,
                IsSuccess = true,
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Course delete ok"
            };
        }
    }
}
