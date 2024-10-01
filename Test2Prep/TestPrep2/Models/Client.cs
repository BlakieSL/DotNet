using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestPrep2.Models;

public class Client
{
    [Key]
    public int IdClient { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    [Required]
    [MaxLength(100)]
    public string LastNmae { get; set; }
    [Required]
    public DateTime Birthday { get; set; }
    [Required]
    [MaxLength(100)]
    public string Pesel { get; set; }
    [Required]
    [MaxLength(100)]
    public string Email { get; set; }
    public int IdClientCategory { get; set; }
    [ForeignKey(nameof(IdClientCategory))]
    public ClientCategory ClientCategory { get; set; }
    public ICollection<Reservation> Reservations { get; set; }
}