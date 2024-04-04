using LMS_Library.Api.Data.Models;
using LMS_Library.Service.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_Library.Service.Services
{
    public interface IDocumentManagement
    {
        Task<ApiResponse<Document>> CreateDocumentAsync(IFormFile file, int Documentid);
        Task<ApiResponse<List<Document>>> GetAllDocumentAsync();
        Task<ApiResponse<Document>> GetDocumentByIdAsync(int id);
        Task<ApiResponse<string>> DeleteDocumentAsync(int id);
        Task<ApiResponse<Document>> UpdateDocumentAsync(int id, IFormFile file);
    }
}
