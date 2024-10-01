using System.ComponentModel.DataAnnotations;

namespace Project3.Models;

public class Individual: Client
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Surname { get; set; }
    [Required]
    public string PESEL { get; set; }
    public bool isDeleted { get; set; } //true(1) - isDeleted, false(0) - !isDeleted
    public ICollection<Contract> Contracts { get; set;}
}