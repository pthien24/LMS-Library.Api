using FLMS_Library.Api.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_Library.Api.Data.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string? UserId { get; set; }
        public int? CourseId { get; set; }
        [NotMapped]
        public ApplicationUser? User { get; set; }
        public Course? Course { get; set; }
    }
}
