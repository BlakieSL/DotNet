using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.Protected;
using Project3.Context;
using Project3.DTOs;
using Project3.Exceptions;
using Project3.Models;
using Project3.Services;
using Xunit;

namespace Project3.Tests;

[TestSubject(typeof(RevenueService))]
public class RevenueTests
{
    private readonly LocalDbContext _context;
    private readonly RevenueService _revenueService;
    private readonly Mock<HttpMessageHandler> _httpHandler;

    public RevenueTests()
    {
        var options = new DbContextOptionsBuilder<LocalDbContext>()
            .UseInMemoryDatabase(databaseName: "DB")
            .Options;

        _context = new LocalDbContext(options);

        _httpHandler = new Mock<HttpMessageHandler>();
        var httpClient = new HttpClient(_httpHandler.Object)
        {
            BaseAddress = new Uri("https://v6.exchangerate-api.com")
        };

        _revenueService = new RevenueService(_context, httpClient);
    }

    [Fact]
    public async Task CalculateRevenue_ShouldReturnRevenue()
    {
        // Arrange
        var company = new Company
        {
            Address = "TestAddress",
            Email = "TestEmail@gmail.com",
            PhoneNumber = "123456",
            CompanyName = "TestCompany",
            KRS = "123456"
        };

        _context.Companies.Add(company);
        await _context.SaveChangesAsync();

        var softwareSystem = new SoftwareSystem
        {
            Name = "TestName",
            Description = "TestDescription",
            Version = "1.1",
            Category = "TestCategory",
            Price = 1000m,
            CompanyId = company.ClientId 
        };

        _context.SoftwareSystems.Add(softwareSystem);
        await _context.SaveChangesAsync();

        var contract = new Contract
        {
            IndividualId = 123123,
            SoftwareSystemId = softwareSystem.SoftwareSystemId,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(29),
            Price = 1000m,
            IsPaid = true,
            IsSigned = true,
            Paid = 1000m
        };

        _context.Contracts.Add(contract);
        await _context.SaveChangesAsync();

        var dto = new RevenueRequestDto
        {
            CompanyId = company.ClientId,
            SoftwareSystemId = softwareSystem.SoftwareSystemId,
            Currency = "PLN"
        };

        // Act
        var result = await _revenueService.CalculateRevenue(dto);

        // Assert
        Assert.Equal(1000m, result.Revenue);
    }
    
    [Fact]
    public async Task CalculatePredictedRevenue_ShouldReturnPredictedRevenue()
    {
        // Arrange
        var company = new Company
        {
            CompanyName = "TestCompany",
            Address = "TestAddress",
            Email = "TestEmail@gmail.com",
            PhoneNumber = "123456",
            KRS = "123456"
        };
        
        _context.Companies.Add(company);
        await _context.SaveChangesAsync();
        
        var softwareSystem = new SoftwareSystem
        {
            Name = "TestName",
            Description = "TestDescription",
            Version = "1.1",
            Category = "TestCategory",
            Price = 1000m,
            Company = company
        };
        
        _context.SoftwareSystems.Add(softwareSystem);
        await _context.SaveChangesAsync();
        
        var contract = new Contract
        {
            IndividualId = 2345234,
            SoftwareSystemId = softwareSystem.SoftwareSystemId,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(29),
            Price = 1000m,
            IsPaid = false,
            IsSigned = false,
            Paid = 0
        };
        
        _context.Contracts.Add(contract);
        await _context.SaveChangesAsync();

        var dto = new RevenueRequestDto
        {
            CompanyId = company.ClientId,
            SoftwareSystemId = softwareSystem.SoftwareSystemId,
            Currency = "PLN"
        };

        // Act
        var result = await _revenueService.CalculatePredictedRevenue(dto);

        // Assert
        Assert.Equal(1000m, result.Revenue);
    }
    
    [Fact]
    public async Task CalculateRevenue_ShouldThrowNotFoundExceptionForCompany()
    {
        
        // Arrange
        var dto = new RevenueRequestDto
        {
            CompanyId = 4123412,
            SoftwareSystemId = 213423,
            Currency = "PLN"
        };

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _revenueService.CalculateRevenue(dto));
    }

    [Fact]
    public async Task CalculateRevenue_ShouldThrowNotFoundExceptionForSoftwareSystem()
    {
        // Arrange
        var company = new Company
        {
            CompanyName = "TestCompany",
            Address = "TestAddress",
            Email = "TestEmail@gmail.com",
            PhoneNumber = "123456",
            KRS = "123456"
        };
        _context.Companies.Add(company);
        await _context.SaveChangesAsync();

        var dto = new RevenueRequestDto
        {
            CompanyId = company.ClientId,
            SoftwareSystemId = 23423,
            Currency = "PLN"
        };

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _revenueService.CalculateRevenue(dto));
    }
    
}