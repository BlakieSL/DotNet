namespace preparation_test_2.Models;

public class Client
{
    public int IdClient { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public DateTime Birthday { get; set; }
    public string Pesel { get; set; }
    public string Email { get; set; }
    
    public ClientCategory ClientCategory { get; set; }
    public int IdClientCategory { get; set; }
    
    public ICollection<Reservation> Reservations { get; set; }
    
}