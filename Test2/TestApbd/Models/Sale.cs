using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApbd.Models;

public class Sale
{
    [Key]
    public int IdSale { get; set; }
    public int IdClient { get; set; }
    [ForeignKey(nameof(IdClient))]
    public Client Client { get; set; }
    public int IdSubscription { get; set; }
    [ForeignKey(nameof(IdSubscription))] 
    public Subscription Subscription { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
}