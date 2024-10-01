using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace TestPrep2.Models;

public class BoatStandart
{
    [Key]
    public int IdBoatStandard { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    [Required]
    public int Level { get; set; }

    public ICollection<Sailboat> Sailboats { get; set; }
    public ICollection<Reservation> Reservations { get; set; }
}