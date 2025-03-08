using System;

namespace RentalCar.Models.Result;

public class GetPaymentResult
{
     public string Car_name { get; set; }
     public string transmission{get; set;}
     public int? Number_Seat{get; set;}
     public string Name_Customer {get; set;}
     public string RentalDate {get; set;}
     public decimal? Price_Per_Day{get; set;}
     public decimal? TotalPrice{get; set;}
     public string ImageLink {get; set;}
     
}
