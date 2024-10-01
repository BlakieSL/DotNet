using System.ComponentModel.DataAnnotations;

namespace Project3.Models;

public class Role
{
    [Key]
    public int RoleId { get; set; }
    [Required]
    public string Name { get; set; }
    public ICollection<User_Role> UserRoles { get; set; }
}