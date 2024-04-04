﻿using System.ComponentModel.DataAnnotations;

namespace LMS_Library.Service.Models.Authentication.Login
{
    public class LoginModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "User Name is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
