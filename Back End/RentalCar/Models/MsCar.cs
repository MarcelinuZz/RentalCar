using System;
using System.ComponentModel.DataAnnotations;

namespace RentalCar.Models;

public class MsCar
{
    [Key]
    [MaxLength(36)]
    [Required]
    public string Car_id { get; set; }

    [MaxLength(200)]
    public string name { get; set; }

    [MaxLength(100)]
    public string model { get; set; }

    public int? year { get; set; }

    [MaxLength(50)]
    public string license_plate { get; set; }

    public int? number_of_car_seats { get; set; }

    [MaxLength(100)]
    public string transmission { get; set; }

    public decimal? price_per_day { get; set; }

    public bool? status { get; set; }

    public ICollection<TrRental> Rentals {get;set;}
    public ICollection<TrMaintenance> Maintenances {get; set;}
}
