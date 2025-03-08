using System;

namespace RentalCarClient.Models.Input
{
    public class CreatePaymentRequest
    {
        public string Car_Id { get; set; }
        public DateTime Rental_Date { get; set; }
        public DateTime Return_Date { get; set; }
        public string Name { get; set; }
    }
}