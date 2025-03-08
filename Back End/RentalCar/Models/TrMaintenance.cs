using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalCar.Models;

public class TrMaintenance
{
    [Key]
    [MaxLength(36)]
    [Required]
    public string Maintenance_id { get; set; }

    public DateTime? maintenance_date { get; set; }

    [MaxLength(4000)]
    public string description { get; set; }

    public decimal? cost { get; set; }

    [ForeignKey("MsCar")]
    public string car_id { get; set; }

    public MsCar MsCar { get; set; }

    [ForeignKey("MsEmployee")]
    public string employee_id { get; set; }

    public MsEmployee MsEmployee { get; set; }

}
