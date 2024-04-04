using LMS_Library.Api.Data.Models;
using LMS_Library.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentManagement  _service;
        public DocumentController(IDocumentManagement service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDocument()
        {
            var document = await _service.GetAllDocumentAsync();
            return Ok(document);
        }
        [HttpPost("upload")]
        public async Task<IActionResult> UploadDocument(IFormFile file, int courseId)
        {
            var response = await _service.CreateDocumentAsync(file, courseId);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocumentById(int id)
        {
            var course = await _service.GetDocumentByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            var res = await _service.DeleteDocumentAsync(id);
            return Ok(res);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, IFormFile file)
        {
            var existingCourse = await _service.GetDocumentByIdAsync(id);
            if (existingCourse.IsSuccess == false)
            {
                return BadRequest(existingCourse);
            }
            var updatedCourse = await _service.UpdateDocumentAsync(id, file);
            return Ok(updatedCourse);
        }
    }
}
