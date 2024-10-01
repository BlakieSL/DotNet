using System.ComponentModel.DataAnnotations;

namespace TestApbd.Models;

public class Client
{
    [Key]
    public int IdClient { get; set; }
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }
    [Required]
    [MaxLength(100)]
    public string Email { get; set; }
    [Required]
    [MaxLength(100)]
    public string Phone { get; set; }
    public ICollection<Discount> Discounts { get; set; }
    public ICollection<Sale> Sales { get; set; }
    public ICollection<Payment> Payments { get; set; }
}