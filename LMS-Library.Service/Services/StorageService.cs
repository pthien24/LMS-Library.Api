using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;
using LMS_Library.Service.Models.AwsS3;
using System;

namespace LMS_Library.Service.Services;
public class StorageService : IStorageService
{
    public async Task<S3ReponseDto> UploadFileAsyns(S3Object s3obj, AwsCredentials awsCredentials)
    {
        var Credentials = new BasicAWSCredentials(awsCredentials.AwsKey, awsCredentials.AwsSecret);
        var config = new AmazonS3Config(){
            RegionEndpoint = Amazon.RegionEndpoint.APSoutheast1,

        };
        var reponse = new S3ReponseDto();
        try
        {
            var uploadRequest = new TransferUtilityUploadRequest()
            {
                InputStream = s3obj.InputStream,
                Key = s3obj.Name,
                
                BucketName = s3obj.BucketName,
                CannedACL = S3CannedACL.NoACL,
            };
            using var client = new AmazonS3Client(Credentials, config);
            var  transferUtility = new TransferUtility(client);
            await transferUtility.UploadAsync(uploadRequest);
            reponse.StatusCode = 200;
            reponse.Message = $"{s3obj.Name} upload ok";
        }
        catch (AmazonS3Exception ex)
        {
            reponse.StatusCode = (int)ex.StatusCode;
            reponse.Message = ex.Message;
        }
        catch (Exception ex )
        {
            reponse.StatusCode = 500;
            reponse.Message = ex.Message;
        }
        return reponse;
    }
}
