using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestPrep2.Models;

public class Sailboat
{
    [Key]
    public int IsSailboat { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    [Required]
    public int Capacity { get; set; }
    [Required]
    [MaxLength(100)]
    public string Description { get; set; }
    [Required]
    public float Price { get; set; }
    public int IdBoatStandartd { get; set; }
    [ForeignKey(nameof(IdBoatStandartd))]
    public BoatStandart BoatStandart { get; set; }

    public ICollection<Sailboat_Reservation> SailboatReservations { get; set; }
}