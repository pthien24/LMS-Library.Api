using LMS_Library.Api.Data.Models;
using LMS_Library.Models;
using LMS_Library.Service.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_Library.Service.Services
{
    public interface IEnrollmentManagment
    {
        Task<ApiResponse<string>> CreateEnrollmentAsync(int courseId, string userId);
        Task<ApiResponse<List<UserDto>>> GetEnrolledUsersAsync(int courseId);
    }
}
