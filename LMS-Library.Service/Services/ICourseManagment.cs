using LMS_Library.Api.Data.Models;
using LMS_Library.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_Library.Service.Services
{
    public interface ICourseManagment
    {
        Task<ApiResponse<Course>> CreateCourseAsync(Course course);
        Task<ApiResponse<Course>> GetCourseByIdAsync(int id);
        Task<ApiResponse<List<Course>>> GetAllCoursesAsync();
        Task<ApiResponse<string>> DeleteCourseAsync(int id);
        Task<ApiResponse<Course>> UpdateCourseAsync(int id, Course updatedCourse);
    }
}
