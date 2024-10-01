using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Project3;
using Project3.Context;
using Project3.DTOs;
using Project3.Exceptions;
using Project3.Models;
using Project3.Services;
using Xunit;

namespace Project3.Tests;

[TestSubject(typeof(ClientService))]
public class ClientTests
{

    private readonly LocalDbContext _context;
    private readonly ClientService _clientService;

    public ClientTests()
    {
        var options = new DbContextOptionsBuilder<LocalDbContext>()
            .UseInMemoryDatabase(databaseName: "DB")
            .Options;

        _context = new LocalDbContext(options);
        _clientService = new ClientService(_context);
    }

    [Fact]
    public async Task AddIndividual_ShouldAddIndividual()
    {
        // Arrange
        var dto = new IndividualRequestDto
        {
            Name = "TestName",
            Surname = "TestSurname",
            Address = "TestAddress",
            Email = "TestEmail@gmail.com",
            PhoneNumber = "123456",
            PESEL = "123456"
        };

        // Act
        var result = await _clientService.AddIndividual(dto);

        // Assert
        Assert.Equal(dto.Name, result.Name);
        Assert.Equal(dto.Surname, result.Surname);
        Assert.Equal(dto.Address, result.Address);
        Assert.Equal(dto.Email, result.Email);
        Assert.Equal(dto.PhoneNumber, result.PhoneNumber);
        Assert.Equal(dto.PESEL, result.PESEL);
    }
    
    [Fact]
    public async Task AddIndividual_ShouldAddCompany()
    {
        //Arrange
        var dto = new CompanyRequestDto
        {
            CompanyName = "TestCompanyName",
            Address = "TestAddress",
            Email = "testEmail@gmail.com",
            PhoneNumber = "123456",
            KRS = "123456"
        };

        // Act
        var result = await _clientService.AddCompany(dto);

        // Assert
        Assert.Equal(dto.CompanyName, result.CompanyName);
        Assert.Equal(dto.Address, result.Address);
        Assert.Equal(dto.Email, result.Email);
        Assert.Equal(dto.PhoneNumber, result.PhoneNumber);
        Assert.Equal(dto.KRS, result.KRS);
    }
    
    [Fact]
    public async Task UpdateIndividual_ShouldUpdateIndividual()
    {
        // Arrange
        var individual = new Individual
        {
            Name = "TestExistingName",
            Surname = "TestExistingSurname",
            Address = "TestExistingAddress",
            Email = "TestExistingEmail@gmail.com",
            PhoneNumber = "123456",
            PESEL = "123456"
        };
        _context.Individuals.Add(individual);
        await _context.SaveChangesAsync();

        var dto = new IndividualRequestUpdateDto
        {
            Name = "TestChangedName",
            Surname = "TestChangedSurname",
            Address = "TestChangedAddress",
            Email = "TestChangedEmail@gmail.com",
            PhoneNumber = "654321"
        };

        // Act
        var result = await _clientService.UpdateIndividual(individual.ClientId, dto);

        // Assert
        Assert.Equal(dto.Name, result!.Name);
        Assert.Equal(dto.Surname, result.Surname);
        Assert.Equal(dto.Address, result.Address);
        Assert.Equal(dto.Email, result.Email);
        Assert.Equal(dto.PhoneNumber, result.PhoneNumber);
        Assert.Equal(individual.PESEL, result.PESEL);
    }
    
    [Fact]
    public async Task UpdateCompany_ShouldUpdateCompany()
    {
        // Arrange
        var company = new Company
        {
            CompanyName = "TestExistingCompanyName",
            Address = "TestExisitngAddress",
            Email = "TestExistingEmail@gmail.com",
            PhoneNumber = "123456",
            KRS = "123456"
        };
        _context.Companies.Add(company);
        await _context.SaveChangesAsync();

        var dto = new CompanyRequestUpdateDto
        {
            CompanyName = "TestChangedCompanyName",
            Address = "TestChangedAddress",
            Email = "TestChandesEmail@gmail.com",
            PhoneNumber = "123456"
        };

        // Act
        var result = await _clientService.UpdateCompany(company.ClientId, dto);

        // Assert
     
        Assert.Equal(dto.CompanyName, result!.CompanyName);
        Assert.Equal(dto.Address, result.Address);
        Assert.Equal(dto.Email, result.Email);
        Assert.Equal(dto.PhoneNumber, result.PhoneNumber);
        Assert.Equal(company.KRS, result.KRS);
    }
    
    [Fact]
    public async Task DeleteIndividual_ShouldSoftDeleteIndividual()
    {
        // Arrange
        var individual = new Individual
        {
            Name = "TestName",
            Surname = "TestSurname",
            Address = "TestAddress",
            Email = "TestEmail@gmail.com",
            PhoneNumber = "123456",
            PESEL = "123456"
        };
        _context.Individuals.Add(individual);
        await _context.SaveChangesAsync();

        // Act
        await _clientService.DeleteIndividual(individual.ClientId);

        // Assert
        var deletedIndividual = await _context.Individuals.FindAsync(individual.ClientId);
        Assert.True(deletedIndividual!.isDeleted);
    }

    [Fact]
    public Task UpdateIndividual_ShouldThrowNotFoundException()
    {
        // Arrange
        var dto = new IndividualRequestUpdateDto
        {
            Name = "TestName",
            Surname = "TestSurname",
            Address = "TestAddress",
            Email = "TestEmail@gmail.com",
            PhoneNumber = "123456"
        };
        // Act & Assert
        MyAssert.Throws<NotFoundException>(() => {_clientService.UpdateIndividual(21231, dto).GetAwaiter().GetResult(); });
        return Task.CompletedTask;
    }

    [Fact]
    public Task UpdateCompany_ShouldThrowNotFoundException()
    {
        // Arrange
        var dto = new CompanyRequestUpdateDto()
        {
            CompanyName = "TestCompanyName",
            Address = "TestAddress",
            Email = "TestEmail@gmail.com",
            PhoneNumber = "123456"
        };
        // Act & Arrange
        MyAssert.Throws<NotFoundException>(() => { _clientService.UpdateCompany(223423, dto).GetAwaiter().GetResult();});
        return Task.CompletedTask;
    }

    [Fact]
    public async Task DeleteIndividual_ShouldThrowNotFoundException()
    {
        // Arrange
        var individual = new Individual
        {
            Name = "TestName",
            Surname = "TestSurname",
            Address = "TestAddress",
            Email = "TestEmail@gmail.com",
            PhoneNumber = "123456",
            PESEL = "123456"
        };
        
        _context.Individuals.Add(individual);
        await _context.SaveChangesAsync();
        
        //Act & Arrange
        MyAssert.Throws<NotFoundException>(() => { _clientService.DeleteIndividual(23534).GetAwaiter().GetResult();});
    }
}