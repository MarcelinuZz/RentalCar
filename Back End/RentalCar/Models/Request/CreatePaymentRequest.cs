using System;
using System.ComponentModel.DataAnnotations;

namespace RentalCar.Models.Request;

public class CreatePaymentRequest
{
    [MaxLength(36)]
    [Required]
    public string Car_Id { get; set; }

    [Required]
    public DateTime Rental_Date { get; set; }

    
    [Required]
    public DateTime Return_Date { get; set; }

    [Required]
    public string Name{get; set;}

}
