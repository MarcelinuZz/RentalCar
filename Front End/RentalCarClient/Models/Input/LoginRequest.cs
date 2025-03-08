using System;

namespace RentalCarClient.Models.Input
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}