﻿namespace LMS_Library.Service.Models.Authentication.User
{
    public class RefreshTokenModel
    {
        public TokenType? AccessToken { get; set; }
        public TokenType? RefreshToken { get; set; }
    }
}
