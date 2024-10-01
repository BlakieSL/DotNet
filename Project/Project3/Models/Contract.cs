using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project3.Models;

public class Contract
{
    [Key]
    public int ContractId { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    [Required]
    public decimal Price { get; set; }
    public bool IsPaid { get; set; } = false;
    public bool IsSigned { get; set; } = false;
    public int SupportedYears { get; set; } = 1;
    public int IndividualId { get; set; }
    [ForeignKey(nameof(IndividualId))]
    public Individual Individual { get; set; }
    public int SoftwareSystemId { get; set; }
    [ForeignKey(nameof(SoftwareSystemId))]
    public SoftwareSystem SoftwareSystem { get; set; }
    public decimal Paid { get; set; }
}