using System;

namespace RentalCarClient.Models.Input;

public class CreateRentalPaymentRequest
{
    public string UserName {get; set;}
    public string CarName {get; set;}
    public DateTime RentalDate {get; set;}
    public DateTime ReturnDate {get; set;}
    public decimal TotalPrice {get; set;}
}
