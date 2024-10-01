namespace preparation_test_2.Models;

public class Sailboat_Reservation
{
    public Sailboat Sailboat { get; set; }
    public int IdSailboat { get; set; }
    
    public Reservation Reservation{ get; set; }
    public int IdReservation { get; set; }
    
}