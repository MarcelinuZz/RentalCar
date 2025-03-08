using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalCar.Models;

public class TrPayment
{
    [Key]
    [Required]
    [MaxLength(36)]
    public string Payment_id {get; set;}
    public DateTime? payment_date {get; set;}
    public decimal? amount{get; set;}
    [MaxLength(100)]
    public string payment_method {get; set;}
    [ForeignKey("TrRental")]
    public string rental_id {get; set;}
    public TrRental TrRental{get; set;}
}
