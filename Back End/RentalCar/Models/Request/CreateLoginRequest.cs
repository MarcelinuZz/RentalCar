using System;
using System.ComponentModel.DataAnnotations;

namespace RentalCar.Models.Request;

public class CreateLoginRequest
{
    [MaxLength(100)]
    [Required]
    public string email { get; set; }

    [MaxLength(100)]
    [Required]
    public string password { get; set; }
}
