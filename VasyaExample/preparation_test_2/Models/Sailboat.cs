namespace preparation_test_2.Models;

public class Sailboat
{
    public int IdSailboat { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public string  Description { get; set; }
    
    public BoatStandard BoatStandard { get; set; }
    public int IdBoatStandard { get; set; }
    
    public float Price{ get; set; }
    
    public ICollection<Sailboat_Reservation> SailboatReservations { get; set; }
}