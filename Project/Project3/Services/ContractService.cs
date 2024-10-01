using Microsoft.EntityFrameworkCore;
using Project3.Context;
using Project3.DTOs;
using Project3.Exceptions;
using Project3.Models;

namespace Project3.Services;

public class ContractService : IContractService
{
    private readonly LocalDbContext _context;

    public ContractService(LocalDbContext context)
    {
        _context = context;
    }
    public async Task<ContractResponseDto> CreateContract(ContractRequestDto dto)
    {
            var client = await _context.Clients.FindAsync(dto.ClientId);
            var softwareSystem = await _context.SoftwareSystems
                .Include(s => s.SoftwareSystemDiscounts)
                .ThenInclude(ssd => ssd.Discount)
                .FirstOrDefaultAsync(s => s.Name == dto.SoftwareSystemName && s.Version == dto.Version);

            if (client == null || softwareSystem == null)
            {
                throw new NotFoundException("Client or SoftwareSystem not found");
            }
         
            if ((dto.EndDate - dto.StartDate).Days < 3 || 30 < (dto.EndDate - dto.StartDate).Days)
            {
                throw new ValidationException("Should be min 3 days and max 30 days");
            }
            
            var hasActiveContract = await _context.Contracts.AnyAsync(c => c.IndividualId == dto.ClientId &&
                                                                           c.SoftwareSystemId == softwareSystem.SoftwareSystemId 
                                                                           && c.IsSigned);
            if (hasActiveContract)
            {
                throw new ValidationException("The client have active contract");
            }
            
            var isReturningClient = _context.Contracts.Any(c => c.IndividualId == dto.ClientId && (c.IsSigned || c.IsPaid));
            var activeDiscounts = softwareSystem.SoftwareSystemDiscounts
                .Where(ssd => ssd.Discount.StartDate <= DateTime.Now && ssd.Discount.EndDate >= DateTime.Now)
                .Select(ssd => ssd.Discount.Value)
                .ToList();
            
            decimal bestDiscount = activeDiscounts.Any() ? activeDiscounts.Max() : 0;
            
            Console.Write(bestDiscount);
            if (isReturningClient)
            {
                bestDiscount += 5; 
            }

            decimal basePrice = softwareSystem.Price + (dto.SupportYears - 1) * 1000;
            decimal discountedPrice = basePrice - (basePrice * bestDiscount / 100);

            var contract = new Contract
            {
                IndividualId = dto.ClientId,
                SoftwareSystemId = softwareSystem.SoftwareSystemId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Price = discountedPrice,
                SupportedYears = dto.SupportYears,
                IsPaid = false,
                IsSigned = false
            };

            _context.Contracts.Add(contract);
            await _context.SaveChangesAsync();

            return new ContractResponseDto
            {
                ContractId = contract.ContractId,
                ClientId = contract.IndividualId,
                SoftwareSystemId = contract.SoftwareSystemId,
                StartDate = contract.StartDate,
                EndDate = contract.EndDate,
                Price = contract.Price,
                IsPaid = contract.IsPaid,
                IsSigned = contract.IsSigned,
                SupportYears = contract.SupportedYears,
            };
    }

    public async Task DeleteContract(int id)
    {
        var contract = await _context.Contracts.FindAsync(id);

        if (contract == null)
        {
            throw new NotFoundException("Contract not found");
        }

        _context.Contracts.Remove(contract);
        await _context.SaveChangesAsync();
    }

    public async Task MakePayment(int contractId, PaymentDto dto)
    {
        var contract = await _context.Contracts.FindAsync(contractId);
        
        if (contract == null)
        {
            throw new NotFoundException("Contract not found");
        }

        if (contract.EndDate < DateTime.Now)
        {
            throw new PaymentException("Invoice expired");
        }

        contract.Paid += dto.Amount;
        
        await _context.SaveChangesAsync();
        
        if (contract.Paid < contract.Price)
        {
            throw new PaymentException("Not enough paid");
        }

        contract.IsPaid = true;
        contract.IsSigned = true;

        await _context.SaveChangesAsync();
    }
}