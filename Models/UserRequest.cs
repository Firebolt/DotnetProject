﻿namespace FinalProject.Models
{
    public class UserRequest
    {
        public string? Username { get; set; }
        public string? Password { get; set; }

        public string? Email { get; set; }
        public Role UserRole { get; set; }
    }
}