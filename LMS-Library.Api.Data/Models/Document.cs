using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_Library.Api.Data.Models
{
    public class Document
    {
        public int? Id { get; set; }
        public string? FilePath { get; set; }
        public DateTime CreatedDate { get; set; } 
        public DateTime UploadedDate { get; set; } 
        public int? CourseID { get; set; }
        public Course? Course { get; set; }
    }
}
