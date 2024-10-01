using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace TestPrep2.Models;

public class Reservation
{
    [Key]
    public int IdReservation { get; set; }
    public int IdClient { get; set; }
    [ForeignKey(nameof(IdClient))]
    public Client Client { get; set; }
    [Required]
    public DateTime DateFrom { get; set; }
    [Required]
    public DateTime DateTo { get; set; }
    [Required]
    public int Capacity { get; set; }
    [Required]
    public int NumOfBoats { get; set; }
    [Required]
    public bool FulFilled { get; set; }
    public float Price { get; set; }
    [MaxLength(200)]
    public string CancelReason { get; set; }
    public int IdBoatStandard { get; set; }
    [ForeignKey(nameof(IdBoatStandard))]
    public BoatStandart BoatStandart { get; set; }
    public ICollection<Sailboat_Reservation> SailboatReservations { get; set; }
}