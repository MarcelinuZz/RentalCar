using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalCar.Models;

public class MsCarImages
{
    [Key]
    [MaxLength(36)]
    [Required]
    public string Image_car_id { get; set; }

    [MaxLength(2000)]
    public string image_link { get; set; }

    [ForeignKey("MsCar")]
    public string Car_id { get; set; }
    public MsCar MsCar { get; set; }
}
