using System;
using System.ComponentModel.DataAnnotations;

namespace RentalCar.Models.Request;

public class CreateRentalQueryRequest
{   
    [Required]
    public DateTime rental_date { get; set; }
    [Required]
    public DateTime? return_date { get; set; }
    [Required]
    public int? year { get; set; }

}
