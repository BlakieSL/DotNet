using System.ComponentModel.DataAnnotations;

namespace TestPrep2.Models;

public class ClientCategory
{
    [Key]
    public int IdClientCategory { get; set; }
    [Required]
    [MaxLength (100)]
    public string Name { get; set; }
    [Required]
    public int DiscountPerc { get; set; }
    public ICollection<Client> Clients { get; set; }
}