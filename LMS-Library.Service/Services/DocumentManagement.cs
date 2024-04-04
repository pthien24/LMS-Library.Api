using LMS_Library.Api.Data.Models;
using LMS_Library.Service.Models;
using LMS_Library.Service.Models.AwsS3;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
                var objname = await UploadFileToS3Async(file);
                var document = new Document()
                {
                    FilePath = objname,
                    CreatedDate = DateTime.Now,
                    UploadedDate = DateTime.Now,
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

        public async Task<ApiResponse<string>> DeleteDocumentAsync(int id)
        {
            var Document = await _context.Documents.FindAsync(id);
            if (Document == null)
            {
                return new ApiResponse<string>
                {
                    Response = null,
                    IsSuccess = false,
                    StatusCode = (int)HttpStatusCode.NotFound,

                    Message = $"Documents with id {id} not found.Documents reponse ok"
                };
            }

            _context.Documents.Remove(Document);
            await _context.SaveChangesAsync();
            return new ApiResponse<string>
            {
                Response = null,
                IsSuccess = true,
                StatusCode = (int)HttpStatusCode.OK,

                Message = "Document delete ok"
            };
        }

        public async Task<ApiResponse<List<Document>>> GetAllDocumentAsync()
        {
            var Document = await _context.Documents.ToListAsync();
            return new ApiResponse<List<Document>>
            {
                Response = Document,
                IsSuccess = true,
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Document reponse Ok"
            };
        }

        public async Task<ApiResponse<Document>> GetDocumentByIdAsync(int id)
        {
            try
            {
                var courseQuery = _context.Documents;


                var course = await courseQuery.FirstOrDefaultAsync(c => c.Id == id) ;

                if (course == null)
                {
                    return new ApiResponse<Document>
                    {
                        Response = null,
                        IsSuccess = false,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Message = "Document not found."
                    };
                }

                return new ApiResponse<Document>
                {
                    Response = course,
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Course response ok"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Document>
                {
                    Response = null,
                    IsSuccess = false,
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = $"An error occurred while retrieving the course: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<Document>> UpdateDocumentAsync(int id, IFormFile file)
        {
            try
            {
                var existingDocument = await _context.Documents.FindAsync(id);

                if (existingDocument == null)
                {
                    return new ApiResponse<Document>
                    {
                        Response = null,
                        IsSuccess = false,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Message = $"Document with id {id} not found."
                    };
                }
                var objname = await UploadFileToS3Async(file);
                existingDocument.FilePath = objname;
                existingDocument.CreatedDate = DateTime.Now;

                _context.Documents.Update(existingDocument);
                await _context.SaveChangesAsync();

                return new ApiResponse<Document>
                {
                    Response = existingDocument,
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Document updated successfully."
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Document>
                {
                    Response = null,
                    IsSuccess = false,
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = $"An error occurred while updating the document: {ex.Message}"
                };
            }
        }



        #region 

        private async Task<string> UploadFileToS3Async(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is required.");
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

            await _storageService.UploadFileAsyns(s3obj, cred);

            return objname;
        }
        #endregion
    }
}
