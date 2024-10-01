using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Project3.Context;
using Project3.DTOs;
using Project3.Exceptions;


namespace Project3.Services
{
    public class RevenueService : IRevenueService
    {
        private readonly LocalDbContext _context;
        private readonly HttpClient _httpClient;

        public RevenueService(LocalDbContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        public async Task<RevenueResponseDto> CalculateRevenue(RevenueRequestDto dto)
        {
            return await GetRevenue(dto, false);
        }

        public async Task<RevenueResponseDto> CalculatePredictedRevenue(RevenueRequestDto dto)
        {
            return await GetRevenue(dto, true);
        }
        
        private async Task<RevenueResponseDto> GetRevenue(RevenueRequestDto dto, bool flag)
        {
            decimal revenue = 0;
            string currency = dto.Currency;

            var company = await _context.Companies
                .Include(c => c.SoftwareSystems)
                .FirstOrDefaultAsync(c => c.ClientId == dto.CompanyId);

            if (company == null)
            {
                throw new NotFoundException("Company not found");
            }

            var contracts = _context.Contracts
                .Include(c => c.SoftwareSystem)
                .Where(c => c.SoftwareSystem.CompanyId == dto.CompanyId)
                .AsQueryable();
            
            
            if (dto.SoftwareSystemId != 0)
            {
                var softwareSystem = await _context.SoftwareSystems
                    .FirstOrDefaultAsync(ss => ss.SoftwareSystemId == dto.SoftwareSystemId);

                if (softwareSystem == null)
                {
                    throw new NotFoundException("SoftwareSystem not found");
                }
                
                contracts = contracts.Where(c => c.SoftwareSystemId == dto.SoftwareSystemId);
            }

            if (flag)
            {
                revenue = await contracts
                    .SumAsync(c => c.Price);
            }
            else
            {
                revenue = await contracts
                    .Where(c => c.IsPaid)
                    .SumAsync(c => c.Price);
            }

            if (currency != "PLN")
            { 
                var exchangeRate = await Exchange(currency);
                revenue *= exchangeRate;
            }

            return new RevenueResponseDto { Revenue = revenue };
        }

        private async Task<decimal> Exchange(string to)
        {
            var data = JObject
                .Parse(await _httpClient
                    .GetStringAsync("https://v6.exchangerate-api.com/v6/d32e8c2b23a1474352887ed6/latest/PLN"));
            if (data["conversion_rates"]![to] == null)
            {
                throw new CurrencyException("Invalid currency");
            }
            return data["conversion_rates"][to].Value<decimal>();
        }
    }
}
