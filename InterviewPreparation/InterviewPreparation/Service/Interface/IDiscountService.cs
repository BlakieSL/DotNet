using InterviewPreparation.Dto;

namespace InterviewPreparation.Service.Interface;

public interface IDiscountService
{
    Task<DiscountResponseDto> getDiscountByClientId(int clientId);
    Task<DiscountResponseDto> createDiscount(DiscountCreateDto discountDto);
}