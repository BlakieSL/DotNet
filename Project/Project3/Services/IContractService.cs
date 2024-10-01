using Project3.DTOs;

namespace Project3.Services;

public interface IContractService
{
    Task<ContractResponseDto> CreateContract(ContractRequestDto dto);
    Task DeleteContract(int id);
    Task MakePayment(int contractId, PaymentDto dto);
}