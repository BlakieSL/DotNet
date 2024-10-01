using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestPrep2.Context;
using TestPrep2.Models;
using TestPrep2.Models.DTO;

namespace TestPrep2.Controllers;
[ApiController]
[Route("/api[controller]")]
public class UserController : ControllerBase
{
    private readonly LocalDbContext _context;

    public UserController(LocalDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetReservations(int id)
    {
        var client = await _context.Clients
            .Include(c => c.Reservations)
            .FirstOrDefaultAsync(c => c.IdClient == id);
        if (client == null)
        {
            return NotFound();
        }

        var clientResponse = new ClientResponse
        {
            IdClient = client.IdClient,
            Name = client.Name,
            LastName = client.LastNmae,
            Birthday = client.Birthday,
            Pesel = client.Pesel,
            Email = client.Email,
            IdClientCategory = client.IdClientCategory,
            Reservations = client.Reservations
                .OrderByDescending(r => r.DateTo)
                .Select(r => new ReservationDto
                {
                    IdReservation = r.IdReservation,
                    IdClient = r.IdClient,
                    DateFrom = r.DateFrom,
                    DateTo = r.DateTo,
                    Capacity = r.Capacity,
                    NumOfBoats = r.NumOfBoats,
                    FulFilled = r.FulFilled,
                    Price = r.Price,
                    CancelReason = r.CancelReason,
                    IdBoatStandard = r.IdBoatStandard
                }).ToList()
        };
        return Ok(clientResponse);
    }

    [HttpPost("makeReservation")]
    public async Task<IActionResult> MakeReservation([FromBody] ReservationRequest request) 
    {
        var hasActiveReservations = await _context.Reservations
            .AnyAsync(r => r.IdClient == request.IdClient && !r.FulFilled);
        
        if (hasActiveReservations)
        {
            return BadRequest("client already have active reservations");
        }

        var availableBoats = await _context.Sailboats
            .Include(s => s.SailboatReservations)
            .Where(s => s.IdBoatStandartd>= request.IdBoatStandard)
            .ToListAsync();
        var countAvailableByStandard = availableBoats
            .GroupBy(s=> s.IdBoatStandartd)
            .ToDictionary(
                g => g.Key,
                g => g.Count()
            );
        var standardToUse = countAvailableByStandard
            .Where(kv => kv.Value >= request.NumOfBoats)
            .OrderBy(kv => kv.Key)
            .Select(kv => kv.Key)
            .FirstOrDefault();
        if (standardToUse == 0)
        {
            var newReservation = new Reservation
            {
                IdClient = request.IdClient,
                DateFrom = request.DateFrom,
                DateTo = request.DateTo,
                IdBoatStandard = request.IdBoatStandard,
                NumOfBoats = request.NumOfBoats,
                FulFilled = false,
                CancelReason = "Not enough boats available."
            };

            _context.Reservations.Add(newReservation);
            await _context.SaveChangesAsync();

            return BadRequest("Not enough boats available.");
        }
        
        var client = await _context.Clients
            .Include(c => c.Reservations)
            .FirstOrDefaultAsync(c => c.IdClient == request.IdClient);
        var cost = CalculateCost(request, client);

        // Step 8: Update the reservation and database
        var reservation = new Reservation
        {
            IdClient = request.IdClient,
            DateFrom = request.DateFrom,
            DateTo = request.DateTo,
            IdBoatStandard = standardToUse,
            NumOfBoats = request.NumOfBoats,
            Price = cost,
            FulFilled = true
        };

        _context.Reservations.Add(reservation);
        await _context.SaveChangesAsync();

        return Ok(reservation.IdReservation);
    }
    private float CalculateCost(ReservationRequest request, Client client)
    {
      
        float basePrice = (float)100.0;
        float totalCost = basePrice * request.NumOfBoats * (float)(request.DateTo - request.DateFrom).TotalDays;
        
        if (client.ClientCategory.Name == "VIP")
        {
            totalCost *= (float)0.9m;
        }
        
        return totalCost;
    }
}