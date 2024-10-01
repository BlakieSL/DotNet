using Project3.DTOs;

namespace Project3.Services;

public interface IRevenueService
{
    public Task<RevenueResponseDto> CalculateRevenue(RevenueRequestDto dto);
    public Task<RevenueResponseDto> CalculatePredictedRevenue(RevenueRequestDto dto);
}