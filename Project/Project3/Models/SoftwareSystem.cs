using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project3.Models;

public class SoftwareSystem
{
    [Key]
    public int SoftwareSystemId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string Version { get; set; }
    [Required]
    public string Category { get; set; }
    public ICollection<Contract> Contracts { get; set; }
    public ICollection<SoftwareSystem_Discount> SoftwareSystemDiscounts { get; set; }
    [Required]
    public decimal Price { get; set; }//  For a specific software, we should know exactly what
    // the cost of buying a license for this software is for one year
    public int CompanyId { get; set; }
    [ForeignKey(nameof(CompanyId))]
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Company Company { get; set; }
}