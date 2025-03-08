using System;
using System.ComponentModel.DataAnnotations;

namespace RentalCar.Models.Request;

public class CreateRegisterRequest
{   [Required]
    [MaxLength(200)]
    public string name { get; set; }
    [Required]
    [MaxLength(100)]
    public string email { get; set; }
    [Required]
    [MaxLength(100)]
    public string password { get; set; }
    [Required]
    [MaxLength(100)]
    public string RePassword { get; set; }
    [Required]
    [MaxLength(50)]
    public string phone_number { get; set; }
    [Required]
    [MaxLength(500)]
    public string address { get; set; }
}
