using LMS_Library.Api.Data.Models;
using LMS_Library.Service.Models;
using LMS_Library.Service.Models.AwsS3;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net;


namespace LMS_Library.Service.Services
{
    public class DocumentManagement : IDocumentManagement
    {

        private readonly DataBaseContext _context;
        private readonly IStorageService _storageService;
        private readonly ICourseManagment _courseManagment;
        private readonly IConfiguration _config;
        public DocumentManagement(
            DataBaseContext context,
            IStorageService storageService,
            IConfiguration config,
            ICourseManagment courseManagment)
        {
            _context = context;
            _storageService = storageService;
            _config = config;
            _courseManagment = courseManagment;
        }
        public async Task<ApiResponse<Document>> CreateDocumentAsync(IFormFile file, int Courseid)
        {
            try
            {
                var courseResponse = await _courseManagment.GetCourseByIdAsync(Courseid);
                if (!courseResponse.IsSuccess ) {
                    return new ApiResponse<Document>
                    {
                        Response = null,
                        IsSuccess = false,
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Message = "course  is incor."
                    };
                }
                if (file == null || file.Length == 0)
                {
                    return new ApiResponse<Document>
                    {
                        Response = null,
                        IsSuccess = false,
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Message = "File is required."
                    };
                }
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
                var res = await _storageService.UploadFileAsyns(s3obj, cred);
                var document = new Document()
                {
                    FilePath = objname,
                    UploadDate = DateTime.Now,
                    CourseID = Courseid
                };
                _context.Documents.Add(document);
                await _context.SaveChangesAsync();

                return new ApiResponse<Document>
                {
                    Response = document,
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.Created,
                    Message = "Document created successfully."
                };
            }
            catch (Exception ex)
            {

                return new ApiResponse<Document>
                {
                    Response = null,
                    IsSuccess = false,
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = $"An error occurred while creating the document: {ex.Message}"
                };
            }
        }
    }
}
