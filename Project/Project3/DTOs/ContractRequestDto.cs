using System.ComponentModel.DataAnnotations;

namespace Project3.DTOs;

public class ContractRequestDto
{
    public int ClientId { get; set; }
    public string SoftwareSystemName { get; set; }
    public string Version { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    [Range(1,3)]
    public int SupportYears { get; set; }
}