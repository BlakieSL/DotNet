using System.ComponentModel.DataAnnotations;

namespace Project3.Models;

public class Company : Client
{
    [Required]
    public string CompanyName { get; set; }
    [Required]
    public string KRS { get; set; }
    public ICollection<SoftwareSystem> SoftwareSystems { get; set; }
}