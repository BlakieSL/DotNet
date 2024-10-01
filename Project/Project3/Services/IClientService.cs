using Project3.DTOs;

namespace Project3.Services;

public interface IClientService
{
    Task<IndividualDto> AddIndividual(IndividualRequestDto dto);
    Task<CompanyDto> AddCompany(CompanyRequestDto dto);
    Task<IndividualDto?> UpdateIndividual(int id, IndividualRequestUpdateDto dto);
    Task<CompanyDto?> UpdateCompany(int id, CompanyRequestUpdateDto dto);
    Task DeleteIndividual(int id);
}