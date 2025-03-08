using System;

namespace RentalCarClient.Models.Input
{
    public class RegisterRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public string Phone_Number { get; set; }
        public string Address { get; set; }
    }
}