using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project3.DTOs;
using Project3.Exceptions;
using Project3.Services;

namespace Project3.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RevenueController : ControllerBase
{
    private readonly IRevenueService _revenueService;

    public RevenueController(IRevenueService revenueService)
    {
        _revenueService = revenueService;
    }

    [HttpPost("calculateRevenue")]
    [Authorize(Roles = "user,admin")]
    public async Task<IActionResult> CalculateRevenue([FromBody] RevenueRequestDto dto)
    {
        try
        {
            return Ok(await _revenueService.CalculateRevenue(dto));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch(CurrencyException e)
        {
            Console.Write("here");
            return BadRequest(e.Message);
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
    
    [HttpPost("calculatePredictedRevenue")]
    [Authorize(Roles = "user,admin")]
    public async Task<IActionResult> CalculatePredictedRevenue([FromBody] RevenueRequestDto dto)
    {
        try
        {
            return Ok(await _revenueService.CalculatePredictedRevenue(dto));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch(CurrencyException e)
        {
            Console.Write("here");
            return BadRequest(e.Message);
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
}