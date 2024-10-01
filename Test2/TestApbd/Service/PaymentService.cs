using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApbd.Context;
using TestApbd.Dtos;
using TestApbd.Models;

namespace TestApbd.Service;

public class PaymentService : IPaymentService
{
    private readonly LocalDbContext _context;

    public PaymentService(LocalDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> GetActiveDiscounts(int id)
    {
        var customer = await _context.Clients
            .Include(c => c.Discounts)
            .FirstOrDefaultAsync(c => c.IdClient == id);

        if (customer == null)
            return new NotFoundResult();

        var discountDtoResponses = customer.Discounts
            .Where(d =>  d.DateTo > DateTime.Now)
            .Select(d => new DiscountDtoResponse
            {
                IdDiscount = d.IdDiscount,
                Value = d.Value,
                EndDate = d.DateTo,
                Description = "new Year Discount",
            })
            .ToList();

        var response = new CustomerDtoResponse
        {
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email,
            Phone = customer.Phone,
            DiscountDtoResponses = discountDtoResponses
        };
        return new OkObjectResult(response);
    }

    public async Task<IActionResult> ModifySubscriptionRenewalPeriod(SubscriptionDtoRequest request, int idCustomer, int idSubscription)
    {
        var client = await _context.Clients
            .FindAsync(idCustomer);
        if (client == null)
        {
            return new BadRequestObjectResult("client not found");
        }

        var subscription = await _context.Subscriptions
            .FindAsync(idSubscription);
        if (subscription == null)
        {
            return new BadRequestObjectResult("subscription not found");
        }
        if (DateTime.Now > subscription.EndTime)
        {
            return new BadRequestObjectResult("subscription not active");
        }
       
        var payments = await _context.Payments
            .Where(p => p.IdClient == idCustomer && p.IdSubscription == idSubscription)
            .ToListAsync();
        
        if (payments.Any())
        {
            var latest = payments.MaxBy(p => p.Date);
            
            var discountsT = await _context.Discounts
                .Where(d => d.IdClient == idCustomer && (d.DateTo > DateTime.Now))
                .ToListAsync();
        
            var percentageT = discountsT
                .Sum(d => d.Value);
            if (percentageT > 50)
            {
                percentageT = 50;
            }

            var discountedPrice = CalculatePrice(percentageT, subscription);

            if (latest.Value != discountedPrice)
            {
                return new BadRequestObjectResult("the amount paid is not consistent");
            }

            return new BadRequestObjectResult("subscription paid");
        } 
       
        
        var discounts = await _context.Discounts
            .Where(d => d.IdClient == idCustomer && (d.DateTo > DateTime.Now))
            .ToListAsync();
        
        var percentage = discounts
            .Sum(d => d.Value);
        if (percentage > 50)
        {
            percentage = 50;
        }
        
        subscription.RenewalPeriod = request.RenewalPeriod;
        _context.Subscriptions.Update(subscription);
        
        var payment = new Payment
        {
            Date = DateTime.Now,
            IdClient = idCustomer,
            IdSubscription = idSubscription,
            Value = CalculatePrice(percentage, subscription)
        };

        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();

        return new OkObjectResult(payment.IdPayment);
    }
    private double CalculatePrice(int percentage, Subscription subscription)
    {
        return subscription.Price * (1 - (percentage / 100.0));
    }
}