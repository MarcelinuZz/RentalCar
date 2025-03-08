using System;
using System.ComponentModel.DataAnnotations;

namespace RentalCar.Models.Request;

public class CreateRentalPaymentRequest
{   
    [Required]
    public string UserName {get; set;}
    [Required]
    public string CarName {get; set;}
    [Required]
    public DateTime RentalDate {get; set;}
    [Required]
    public DateTime ReturnDate {get; set;}
    [Required]
    public decimal TotalPrice {get; set;}
}
