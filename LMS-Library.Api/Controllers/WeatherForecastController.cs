using LMS_Library.Service.Models.AwsS3;
using LMS_Library.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Library.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IStorageService _service;
        private readonly IConfiguration _config;
        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IStorageService service,
            IConfiguration config)
        {
            _logger = logger;
            _service = service;
            _config = config;
        }
        [HttpPost(Name = "UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            await using var memoryStr = new MemoryStream();
            await file.CopyToAsync(memoryStr);
            var fileExt = Path.GetExtension(file.FileName);
            var objname = $"{Guid.NewGuid()}.{fileExt}";
            var s3obj = new S3Object()
            {
                BucketName = "thienbuc",
                InputStream = memoryStr,
                Name = objname,
            };
            var cred = new AwsCredentials()
            {
                AwsKey = _config["AwsConfiguration:AccessKey"],
                AwsSecret = _config["AwsConfiguration:SecretKey"]
            };
            var res = await _service.UploadFileAsyns(s3obj, cred);
            return Ok(res);
        }
    }
}
