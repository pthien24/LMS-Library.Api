using LMS_Library.Service.Models.AwsS3;
using System;

namespace LMS_Library.Service.Services;
public interface IStorageService
{
    Task<S3ReponseDto> UploadFileAsyns(S3Object s3obj, AwsCredentials awsCredentials);
}
