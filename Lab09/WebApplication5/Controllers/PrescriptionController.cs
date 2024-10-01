using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Context;
using WebApplication5.Models;

namespace WebApplication5.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PrescriptionController: ControllerBase
    {
        private readonly PrescriptionDbContext _context;

        public PrescriptionController(PrescriptionDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddPrescription([FromBody] PrescriptionDTO prescriptionDto)
        {
            var patient = await _context.Patients.FindAsync(prescriptionDto.Patient.IdPatient);
            if (patient == null)
            {
                patient = new Patient
                {
                    FirstName = prescriptionDto.Patient.FirstName,
                    LastName = prescriptionDto.Patient.LastName,
                    BirthDate = prescriptionDto.Patient.BirthDate
                };
                _context.Patients.Add(patient);
                await _context.SaveChangesAsync();
            }

        
            if (prescriptionDto.Medicaments.Count > 10)
            {
                return BadRequest();
            }
            var medicationIds = prescriptionDto.Medicaments.Select(m => m.IdMedicament).ToList();
            var medications = await _context.Medicaments.Where(m => medicationIds.Contains(m.IdMedicament)).ToListAsync();
            if (medications.Count != prescriptionDto.Medicaments.Count)
            {
                return BadRequest();
            }
            if (prescriptionDto.DueDate < prescriptionDto.Date)
            {
                return BadRequest();
            }

            var prescription = new Prescription
            {
                Date = prescriptionDto.Date,
                DueDate = prescriptionDto.DueDate,
                IdPatient = patient.IdPatient,
                IdDoctor = prescriptionDto.IdDoctor
            };
            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();
            
            foreach (var medicamentDto in prescriptionDto.Medicaments)
            {
                var prescriptionMedicament = new Prescription_Medicament
                {
                    IdPrescription = prescription.IdPrescription,
                    IdMedicament = medicamentDto.IdMedicament,
                    Dose = medicamentDto.Dose,
                    Details = medicamentDto.Description
                };
                _context.PrescriptionMedicaments.Add(prescriptionMedicament);
            }

            await _context.SaveChangesAsync();

            return Ok(prescription);
        }
        
        [HttpGet("patient/{id}")]
        public async Task<IActionResult> GetPatientData(int id)
        {
            var patient = await _context.Patients
                .Include(p => p.Prescriptions)
                .ThenInclude(pr => pr.Prescription_Medicaments)
                .ThenInclude(pm => pm.Medicament)
                .Include(p => p.Prescriptions)
                .ThenInclude(pr => pr.Doctor)
                .FirstOrDefaultAsync(p => p.IdPatient == id);

            if (patient == null)
            {
                return NotFound("Patient not found.");
            }

            var response = new PatientDtoResponse()
            {
                IdPatient = patient.IdPatient,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                BirthDate = patient.BirthDate,
                Prescriptions = patient.Prescriptions
                    .OrderBy(pr => pr.DueDate)
                    .Select(pr => new PrescriptionDtoResponse()
                    {
                        IdPrescription = pr.IdPrescription,
                        Date = pr.Date,
                        DueDate = pr.DueDate,
                        Medicaments = pr.Prescription_Medicaments.Select(pm => new MedicamentDtoResponse()
                        {
                            IdMedicament = pm.Medicament.IdMedicament,
                            Name = pm.Medicament.Name,
                            Dose = pm.Dose.GetValueOrDefault(),
                            Description = pm.Details
                        }).ToList(),
                        Doctor = new DoctorDtoResponse()
                        {
                            IdDoctor = pr.Doctor.IdDoctor,
                            FirstName = pr.Doctor.FirstName,
                            LastName = pr.Doctor.LastName
                        }
                    }).ToList()
            };

            return Ok(response);
        }
    }