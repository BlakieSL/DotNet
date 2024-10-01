using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebApplication5.Models;

public class Prescription
{
    [Key]
    public int IdPrescription { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public DateTime DueDate { get; set; }
    
    public int IdPatient { get; set; }
    [ForeignKey(nameof(IdPatient))]
    public Patient Patient { get; set; }
    public int IdDoctor { get; set; }
    [ForeignKey(nameof(IdDoctor))]
    public Doctor Doctor { get; set; }
    
    public ICollection<Prescription_Medicament> Prescription_Medicaments { get; set; }
}
public class Prescription_Medicament
{
    public int IdPrescription { get; set; }
        
    [ForeignKey(nameof(IdPrescription))]
    public Prescription Prescription { get; set; }
    
    public int IdMedicament { get; set; }
        
    [ForeignKey(nameof(IdMedicament))]
    public Medicament Medicament { get; set; }

    public int? Dose { get; set; } 
        
    [MaxLength(100)]
    public string Details { get; set; }
    
    
}
public class PrescriptionDTO
{
    public PatientDTO Patient { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public int IdDoctor { get; set; }
    public List<MedicamentDTO> Medicaments { get; set; }
}
public class PrescriptionDtoResponse
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public List<MedicamentDtoResponse> Medicaments { get; set; }
    public DoctorDtoResponse Doctor { get; set; }
}