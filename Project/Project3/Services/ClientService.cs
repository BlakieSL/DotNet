using Project3.Context;
using Project3.DTOs;
using Project3.Exceptions;
using Project3.Models;

namespace Project3.Services;

public class ClientService : IClientService
{
    private readonly LocalDbContext _context;

    public ClientService(LocalDbContext context)
    {
        _context = context;
    }
    public async Task<IndividualDto> AddIndividual(IndividualRequestDto dto)
    {
        var individual = new Individual
        {
            Name = dto.Name,
            Surname = dto.Surname,
            Address = dto.Address,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            PESEL = dto.PESEL
        };

        _context.Individuals.Add(individual);
        await _context.SaveChangesAsync();

        return new IndividualDto
        {
            ClientId = individual.ClientId,
            Name = individual.Name,
            Surname = individual.Surname,
            Address = individual.Address,
            Email = individual.Email,
            PhoneNumber = individual.PhoneNumber,
            PESEL = individual.PESEL,
        };
    }

    public async Task<CompanyDto> AddCompany(CompanyRequestDto dto)
    {
        var company = new Company
        {
            CompanyName = dto.CompanyName,
            Address = dto.Address,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            KRS = dto.KRS,
        };

        _context.Companies.Add(company);
        await _context.SaveChangesAsync();

        return new CompanyDto
        {
            ClientId = company.ClientId,
            CompanyName = company.CompanyName,
            Address = company.Address,
            Email = company.Email,
            PhoneNumber = company.PhoneNumber,
            KRS = company.KRS
        };
    }

    public async Task<IndividualDto?> UpdateIndividual(int id, IndividualRequestUpdateDto dto)
    {
        var individual = await _context.Individuals.FindAsync(id);

        if (individual == null || individual.isDeleted)
        {
            throw new NotFoundException("Client not found");
        }

        individual.Name = dto.Name;
        individual.Surname = dto.Surname;
        individual.Address = dto.Address;
        individual.Email = dto.Email;
        individual.PhoneNumber = dto.PhoneNumber;

        _context.Individuals.Update(individual);
        await _context.SaveChangesAsync();

        return new IndividualDto
        {
            ClientId = individual.ClientId,
            Name = individual.Name,
            Surname = individual.Surname,
            Address = individual.Address,
            Email = individual.Email,
            PhoneNumber = individual.PhoneNumber,
            PESEL = individual.PESEL,
        };
    }

    public async Task<CompanyDto?> UpdateCompany(int id, CompanyRequestUpdateDto dto)
    {
        var company = await _context.Companies.FindAsync(id);

        if (company == null)
        {
            throw new NotFoundException("Client not found");
        }

        company.CompanyName = dto.CompanyName;
        company.Address = dto.Address;
        company.Email = dto.Email;
        company.PhoneNumber = dto.PhoneNumber;

        _context.Companies.Update(company);
        await _context.SaveChangesAsync();

        return new CompanyDto
        {
            ClientId = company.ClientId,
            CompanyName = company.CompanyName,
            Address = company.Address,
            Email = company.Email,
            PhoneNumber = company.PhoneNumber,
            KRS = company.KRS,
        };
    }
    public async Task DeleteIndividual(int id)
    {
        var individual = await _context.Individuals.FindAsync(id);

        if (individual == null || individual.isDeleted)
        {
            throw new NotFoundException("Client not found");
        }
        individual.isDeleted = true;
        //i'm not sure if soft delete means that we change all values to DELETE,
        //but i guess the idea of soft delete is to make account deleted(user can no longer login etc), but we can still hold user data(for analytics, for spam etc)
        _context.Individuals.Update(individual);
        await _context.SaveChangesAsync();
    }
}