using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Project3.Context;
using Project3.DTOs;
using Project3.Exceptions;
using Project3.Models;
using Project3.Services;
using Xunit;

namespace Project3.Tests;

[TestSubject(typeof(ClientService))]
public class ContractTests
{
    private readonly LocalDbContext _context;
    private readonly ContractService _contractService;

    public ContractTests()
    {
        var options = new DbContextOptionsBuilder<LocalDbContext>()
            .UseInMemoryDatabase(databaseName: "DB")
            .Options;
        _context = new LocalDbContext(options);
        _contractService = new ContractService(_context);
    }

    [Fact]
    public async Task CreateContract_ShouldCreateContract()
    {
        // Arrange
        var client = new Individual
        {
            Address = "TestAddress",
            Email = "TestEmail@gmail.com",
            PhoneNumber = "123456",
            Name = "TestName",
            Surname = "TestSurname",
            PESEL = "TestPESEL",
            isDeleted = false,
        };
        var softwareSystem = new SoftwareSystem
        {
            Name = "TestName",
            Description = "TestDescription",
            Version = "1.1",
            Category = "TestCategory",
            Price = 1000m,
            SoftwareSystemDiscounts = new List<SoftwareSystem_Discount>()
        };

        _context.Clients.Add(client);
        _context.SoftwareSystems.Add(softwareSystem);
        await _context.SaveChangesAsync();

        var dto = new ContractRequestDto
        {
            ClientId = client.ClientId,
            SoftwareSystemName = softwareSystem.Name,
            Version = softwareSystem.Version,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(29),
            SupportYears = 1
        };
        
        // Act
        var result = await _contractService.CreateContract(dto);
        
        // Assert
        Assert.Equal(client.ClientId, result.ClientId);
        Assert.Equal(softwareSystem.SoftwareSystemId, result.SoftwareSystemId);
        Assert.Equal(dto.StartDate, result.StartDate);
        Assert.Equal(dto.EndDate, result.EndDate);
        //Assert.Equal(1000m, result.Price); hardcoded, should be or not???
        Assert.False(result.IsPaid);
        Assert.False(result.IsSigned);
        Assert.Equal(dto.SupportYears, result.SupportYears);
    }

    [Fact]
    public async Task DeleteContract_ShouldDeleteContract()
    {
        // Arrange
        var client = new Individual
        {
            Address = "TestAddress",
            Email = "TestEmail@gmail.com",
            PhoneNumber = "123456",
            Name = "TestName",
            Surname = "TestSurname",
            PESEL = "TestPESEL",
            isDeleted = false,
        };
        var softwareSystem = new SoftwareSystem
        {
            Name = "TestName",
            Description = "TestDescription",
            Version = "1.1",
            Category = "TestCategory",
            Price = 1000m,
            SoftwareSystemDiscounts = new List<SoftwareSystem_Discount>()
        };

        _context.Clients.Add(client);
        _context.SoftwareSystems.Add(softwareSystem);
        await _context.SaveChangesAsync();

        var dto = new ContractRequestDto
        {
            ClientId = client.ClientId,
            SoftwareSystemName = softwareSystem.Name,
            Version = softwareSystem.Version,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(29),
            SupportYears = 1
        };
        var contract = await _contractService.CreateContract(dto); 
        
        // Act
        await _contractService.DeleteContract(contract.ContractId); //hardcoded
        
        // Assert
        var result = await _context.Contracts.FindAsync(contract.ContractId);
        Assert.Null(result);
    }

    [Fact]
    public async Task MakePayment_ShouldChangeContractToPaid()  
    {
        // Arrange
        var client = new Individual
        {
            Address = "TestAddress",
            Email = "TestEmail@gmail.com",
            PhoneNumber = "123456",
            Name = "TestName",
            Surname = "TestSurname",
            PESEL = "TestPESEL",
            isDeleted = false,
        };
        var softwareSystem = new SoftwareSystem
        {
            Name = "TestName",
            Description = "TestDescription",
            Version = "1.1",
            Category = "TestCategory",
            Price = 1000m,
            SoftwareSystemDiscounts = new List<SoftwareSystem_Discount>()
        };
        var contract = new Contract
        {
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(29),
            Price = 1000m,
            IsPaid = false,
            IsSigned = false,
            SupportedYears = 1,
            IndividualId = client.ClientId,
            SoftwareSystemId = softwareSystem.SoftwareSystemId,
            Paid = 0
        };
        
        _context.Clients.Add(client);
        _context.SoftwareSystems.Add(softwareSystem);
        _context.Contracts.Add(contract);
        await _context.SaveChangesAsync();

        var dto = new PaymentDto
        {
            Amount = 1000m
        };

        // Act
        await _contractService.MakePayment(contract.ContractId, dto);
        
        //Assert
        var changedContract = await _context.Contracts.FindAsync(contract.ContractId);
        Assert.True(changedContract!.IsPaid);
        Assert.True(changedContract.IsSigned);
    }

    [Fact]
    public async Task CreateContract_ShouldThrowNotFoundExceptionForClient()
    {
        // Arrange
        var softwareSystem = new SoftwareSystem
        {
            Name = "TestName",
            Description = "TestDescription",
            Version = "1.1",
            Category = "TestCategory",
            Price = 1000m,
            SoftwareSystemDiscounts = new List<SoftwareSystem_Discount>()
        };
        var dto = new ContractRequestDto
        {
            ClientId = 2343, 
            SoftwareSystemName = "TestName",
            Version = "1.1",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(29),
            SupportYears = 1
        };
        _context.SoftwareSystems.Add(softwareSystem);
        await _context.SaveChangesAsync();

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _contractService.CreateContract(dto));
    }
    
    [Fact]
    public async Task CreateContract_ShouldThrowNotFoundExceptionForSoftwareSystem()
    {
        // Arrange
        var client = new Individual
        {
            Address = "TestAddress",
            Email = "TestEmail@gmail.com",
            PhoneNumber = "123456",
            Name = "TestName",
            Surname = "TestSurname",
            PESEL = "TestPESEL",
            isDeleted = false,
        };
        var dto = new ContractRequestDto
        {
            ClientId = client.ClientId, 
            SoftwareSystemName = "TestName",
            Version = "1.0",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(29),
            SupportYears = 1
        };
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _contractService.CreateContract(dto));
    }
    
    [Fact]
    public async Task CreateContract_ShouldThrowValidationExceptionForTime()
    {
        // Arrange
        var client = new Individual
        {
            Address = "TestAddress",
            Email = "TestEmail@gmail.com",
            PhoneNumber = "123456",
            Name = "TestName",
            Surname = "TestSurname",
            PESEL = "TestPESEL",
            isDeleted = false,
        };
        var softwareSystem = new SoftwareSystem
        {
            Name = "TestName",
            Description = "TestDescription",
            Version = "1.1",
            Category = "TestCategory",
            Price = 1000m,
            SoftwareSystemDiscounts = new List<SoftwareSystem_Discount>()
        };

        _context.Clients.Add(client);
        _context.SoftwareSystems.Add(softwareSystem);
        await _context.SaveChangesAsync();

        var dto = new ContractRequestDto
        {
            ClientId = client.ClientId,
            SoftwareSystemName = softwareSystem.Name,
            Version = softwareSystem.Version,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1), 
            SupportYears = 1
        };

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() => _contractService.CreateContract(dto));
    }
    
    [Fact]
    public async Task CreateContract_ShouldThrowValidationExceptionForActiveContract()
    {
        // Arrange
        var client = new Individual
        {
            Address = "TestAddress",
            Email = "TestEmail@gmail.com",
            PhoneNumber = "123456",
            Name = "TestName",
            Surname = "TestSurname",
            PESEL = "TestPESEL",
            isDeleted = false,
        };
        var softwareSystem = new SoftwareSystem
        {
            Name = "TestName",
            Description = "TestDescription",
            Version = "1.1",
            Category = "TestCategory",
            Price = 1000m,
            SoftwareSystemDiscounts = new List<SoftwareSystem_Discount>()
        };
        
        _context.Clients.Add(client);
        _context.SoftwareSystems.Add(softwareSystem);
        await _context.SaveChangesAsync();
        
        var existingContract = new Contract
        {
            IndividualId = client.ClientId,
            SoftwareSystemId = softwareSystem.SoftwareSystemId,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(29),
            Price = 1000m,
            SupportedYears = 1,
            IsSigned = true,
            IsPaid = true,
            Paid = 1000m
        };
        _context.Contracts.Add(existingContract);
        await _context.SaveChangesAsync();

        var dto = new ContractRequestDto
        {
            ClientId = client.ClientId,
            SoftwareSystemName = softwareSystem.Name,
            Version = softwareSystem.Version,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(29),
            SupportYears = 1
        };

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() => _contractService.CreateContract(dto));
    }

    [Fact]
    public async Task DeleteContract_ShouldThrowNotFoundException()
    {
       // Arrange & Act & Assert
       await Assert.ThrowsAsync<NotFoundException>(() => _contractService.DeleteContract(2342));
    }

    [Fact]
    public async Task MakePayment_ShouldThrowNotFoundException()
    {
        // Arrange
        var dto = new PaymentDto
        {
            Amount = 1000m
        };

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _contractService.MakePayment(234234, dto));
    }

    [Fact]
    public async Task MakePayment_ShouldThrowPaymentExceptionForInvoiceExpired()
    {
        // Arrange
        var client = new Individual
        {
            Address = "TestAddress",
            Email = "TestEmail@gmail.com",
            PhoneNumber = "123456",
            Name = "TestName",
            Surname = "TestSurname",
            PESEL = "TestPESEL",
            isDeleted = false,
        };
        var softwareSystem = new SoftwareSystem
        {
            Name = "TestName",
            Description = "TestDescription",
            Version = "1.1",
            Category = "TestCategory",
            Price = 1000m,
            SoftwareSystemDiscounts = new List<SoftwareSystem_Discount>()
        };
        var contract = new Contract
        {
            IndividualId = client.ClientId,
            SoftwareSystemId = softwareSystem.SoftwareSystemId,
            StartDate = DateTime.Now.AddDays(-100),
            EndDate = DateTime.Now.AddDays(-71),
            Price = 1000m,
            SupportedYears = 1,
            IsSigned = false,
            IsPaid = false,
            Paid = 0
        };

        _context.Clients.Add(client);
        _context.SoftwareSystems.Add(softwareSystem);
        _context.Contracts.Add(contract);
        await _context.SaveChangesAsync();

        var dto = new PaymentDto
        {
            Amount = 1000m
        };

        // Act & Assert
        await Assert.ThrowsAsync<PaymentException>(() => _contractService.MakePayment(contract.ContractId, dto));
    }

    [Fact]
    public async Task MakePayment_ShouldThrowPaymentExceptionForNotEnoughPaid()
    {
        // Arrange
        var client = new Individual
        {
            Address = "TestAddress",
            Email = "TestEmail@gmail.com",
            PhoneNumber = "123456",
            Name = "TestName",
            Surname = "TestSurname",
            PESEL = "TestPESEL",
            isDeleted = false,
        };
        var softwareSystem = new SoftwareSystem
        {
            Name = "TestName",
            Description = "TestDescription",
            Version = "1.1",
            Category = "TestCategory",
            Price = 1000m,
            SoftwareSystemDiscounts = new List<SoftwareSystem_Discount>()
        };
        var contract = new Contract
        {
            IndividualId = client.ClientId,
            SoftwareSystemId = softwareSystem.SoftwareSystemId,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(29),
            Price = 1000m,
            SupportedYears = 1,
            IsSigned = false,
            IsPaid = false,
            Paid = 0
        };

        _context.Clients.Add(client);
        _context.SoftwareSystems.Add(softwareSystem);
        _context.Contracts.Add(contract);
        await _context.SaveChangesAsync();

        var dto = new PaymentDto
        {
            Amount = 100m 
        };

        // Act & Assert
        await Assert.ThrowsAsync<PaymentException>(() => _contractService.MakePayment(contract.ContractId, dto));
    }
}