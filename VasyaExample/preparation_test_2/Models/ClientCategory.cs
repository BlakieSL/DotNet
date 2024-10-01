namespace preparation_test_2.Models;

public class ClientCategory
{
    public int IdClientCategory { get; set; }
    public string Name { get; set; }
    public int DiscountPerc { get; set; }
    
    public ICollection<Client> Clients { get; set; }
}