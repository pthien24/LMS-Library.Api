using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_Library.Service.Models
{
    public class ApiResponse<T>
    {
        public T? Response{ get; set; }
        public bool IsSuccess { get; set; }
        public int? StatusCode { get; set; }
        public string? Message { get; set; }
    }
}
