using System;
namespace LMS_Library.Service.Models.AwsS3;

public class S3ReponseDto
{
    public int StatusCode { get; set; } = 200;
    public string Message { get; set; } = "";
}
