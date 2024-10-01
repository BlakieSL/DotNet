using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project3.Models;
[PrimaryKey(nameof(DiscountId), nameof(SoftwareSystemId))]
public class SoftwareSystem_Discount
{
    public int DiscountId { get; set; }
    [ForeignKey(nameof(DiscountId))]
    public Discount Discount { get; set; }
    
    public int SoftwareSystemId { get; set; }
    [ForeignKey(nameof(SoftwareSystemId))]
    public SoftwareSystem SoftwareSystem { get; set; }
}