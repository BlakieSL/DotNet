using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models;

public class Doctor
{
    [Key]
    public int IdDoctor { get; set; }
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }
    [Required]
    //[EmailAddress]
    [MaxLength(100)]
    public string Email { get; set; }
    public ICollection<Prescription> Prescriptions { get; set; }
}
public class DoctorDtoResponse
{
    public int IdDoctor { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
