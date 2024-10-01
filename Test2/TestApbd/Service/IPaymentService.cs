using Microsoft.AspNetCore.Mvc;
using TestApbd.Dtos;

namespace TestApbd.Service;

public interface IPaymentService
{
    Task<IActionResult> GetActiveDiscounts(int idCustomer);
    Task<IActionResult> ModifySubscriptionRenewalPeriod(SubscriptionDtoRequest request,int idCustomer, int idSubscription);
}