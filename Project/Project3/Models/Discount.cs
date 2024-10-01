using System.ComponentModel.DataAnnotations;

namespace Project3.Models;

public class Discount
{
    [Key]
    public int DiscountId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public decimal Value { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    public ICollection<SoftwareSystem_Discount> SoftwareSystemDiscounts { get; set; }
}