using System.ComponentModel.DataAnnotations;

namespace Project3.Models;

public class User
{
    [Key]
    public int UserId { get; set; }
    [Required]
    public string Login { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string Salt { get; set; }
    [Required]
    public string RefreshToken { get; set; }
    public DateTime? RefreshTokenExp { get; set; }
    public ICollection<User_Role> UserRoles { get; set; }
}