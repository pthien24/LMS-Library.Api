using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_Library.Service.Models.AwsS3;
    public class S3Object
    {
        public string? Name { get; set; } 
        public MemoryStream? InputStream { get; set; }
        public string? BucketName { get; set; }
    }
