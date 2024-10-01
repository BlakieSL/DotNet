using System.ComponentModel.DataAnnotations;

namespace TestApbd.Models;

public class Subscription
{
    [Key]
    public int IdSubscription { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    [Required]
    public int RenewalPeriod { get; set; }
    [Required]
    public DateTime EndTime { get; set; }
    [Required]
    public double Price { get; set; }
    public ICollection<Sale> Sales { get; set; }
    public ICollection<Payment> Payments { get; set; }
}