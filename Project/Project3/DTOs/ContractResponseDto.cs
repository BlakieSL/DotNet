namespace Project3.DTOs;

public class ContractResponseDto
{
    public int ContractId { get; set; }
    public int ClientId { get; set; }
    public int SoftwareSystemId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Price { get; set; }
    public bool IsPaid { get; set; }
    public bool IsSigned { get; set; }
    public int SupportYears { get; set; }
}