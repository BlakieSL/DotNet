using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApbd.Context;
using TestApbd.Dtos;
using TestApbd.Models;
using TestApbd.Service;

namespace TestApbd.Controllers;
[ApiController]
[Route("/api[controller]")]
public class ApiController: ControllerBase
{
    private readonly IPaymentService _paymentService;

    public ApiController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpGet("{idCustomer}/active-discounts")]
    public async Task<IActionResult> GetActiveDiscounts(int idCustomer)
    {
        return await _paymentService.GetActiveDiscounts(idCustomer);
    }

    [HttpPut("{idCustomer}/subscription/{idSubscription}/renewal-period")]
    public async Task<IActionResult> ModifySubscriptionRenewalPeriod([FromBody] SubscriptionDtoRequest request, int idCustomer, int idSubscription)
    {
        return await  _paymentService.ModifySubscriptionRenewalPeriod(request, idCustomer, idSubscription);
    }

}
