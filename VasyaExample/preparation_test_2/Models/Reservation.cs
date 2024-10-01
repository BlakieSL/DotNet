namespace preparation_test_2.Models;

public class Reservation
{
    public int IdReservation { get; set; }
    
    public Client Client { get; set; }
    public int IdClient { get; set; }
    
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    
    public BoatStandard BoatStandard { get; set; }
    public int IdBoatStandard { get; set; }
    
    public int Capacity { get; set; }
    public int NumOfBoats { get; set; }
    public bool Fulfilled { get; set; }
    public float? Price { get; set; }
    public string? CancelReason { get; set; }
    
    public ICollection<Sailboat_Reservation> SailboatReservations { get; set; }
}