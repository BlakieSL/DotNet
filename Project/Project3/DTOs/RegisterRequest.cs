using System.ComponentModel.DataAnnotations;

namespace Project3.DTOs;

public class RegisterRequest
{
    [Required]
    public string Login { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string Role { get; set; }
}