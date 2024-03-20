using LMS_Library.Service.Services;
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

    }
}
