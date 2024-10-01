using System.ComponentModel.DataAnnotations.Schema;

namespace TestPrep2.Models;

public class Sailboat_Reservation
{
    public int IdSailboat { get; set; }
    [ForeignKey(nameof(IdSailboat))] 
    public Sailboat Sailboat { get; set; }
    public int IdReservation { get; set; }
    [ForeignKey(nameof(IdReservation))]
    public Reservation Reservation { get; set; }
}