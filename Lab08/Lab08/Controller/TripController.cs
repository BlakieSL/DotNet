
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab08.Context;
using Lab08.Models;

namespace Lab08.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly MyDbContext _context;

        public TripController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trip>>> GetTrips(int page = 1, int pageSize = 10)
        {
            var trips = await _context.Trips.OrderByDescending(t => t.DateFrom)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return trips;
        }


        [HttpDelete("clients/{idClient}")]
        public async Task<IActionResult> DeleteClient(int idClient)
        {
            var client = await _context.Clients.FindAsync(idClient);
            if (client == null)
            {
                return NotFound();
            }

            var assignedTripsCount = await _context.Client_Trips.CountAsync(ct => ct.IdClient == idClient);
            if (assignedTripsCount > 0)
            {
                return Conflict("Client has assigned trips.");
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        [HttpPost("{idTrip}/clients")]
        public async Task<IActionResult> AssignClientToTrip(int idTrip, [FromBody] ClientTripAssignmentRequest request)
        {
            var existingClient = await _context.Clients.FirstOrDefaultAsync(c => c.Pesel == request.Pesel);
            if (existingClient != null)
            {
                return Conflict("Client with PESEL already exists.");
            }

            var existingAssignment = await _context.Client_Trips.FirstOrDefaultAsync(ct => ct.IdTrip == idTrip && ct.IdClientNavigation.Pesel == request.Pesel);
            if (existingAssignment != null)
            {
                return Conflict("Client is already registered for the trip.");
            }

            var trip = await _context.Trips.FirstOrDefaultAsync(t => t.IdTrip == idTrip && t.DateFrom > DateTime.Now);
            if (trip == null)
            {
                return NotFound("Trip does not exist or has already occurred.");
            }
            
            var clientTrip = new Client_Trip()
            {
                IdClient = existingClient != null ? existingClient.IdClient : 0,
                IdTrip = idTrip,
                RegisteredAt = DateTime.Now,
                PaymentDate = DateTime.Parse(request.PaymentDate)
            };

            _context.Client_Trips.Add(clientTrip);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
