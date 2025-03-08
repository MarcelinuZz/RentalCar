using System;
using System.ComponentModel.DataAnnotations;

namespace RentalCar.Models;

public class MsCustomer
{
    [Key]
    [MaxLength(36)]
    [Required]
    public string Customer_id { get; set; }

    [MaxLength(100)]
    [Required]
    public string email { get; set; }

    [MaxLength(100)]
    [Required]
    public string password { get; set; }

    [MaxLength(200)]
    [Required]
    public string name { get; set; }

    [MaxLength(50)]
    public string phone_number { get; set; }

    [MaxLength(500)]
    public string address { get; set; }

    [MaxLength(100)]
    public string driver_license_number { get; set; }

    public ICollection<TrRental> Rentals {get; set;}
}
