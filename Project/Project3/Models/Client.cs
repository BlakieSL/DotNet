using System.ComponentModel.DataAnnotations;

namespace Project3.Models;

public class Client
{
    [Key]
    public int ClientId { get; set; }
    [Required]
    public string Address { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [Phone]
    public string PhoneNumber { get; set; }
}