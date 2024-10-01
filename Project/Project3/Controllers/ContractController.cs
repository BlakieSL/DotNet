using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project3.DTOs;
using Project3.Exceptions;
using Project3.Services;

namespace Project3.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ContractController : ControllerBase
{
    private readonly IContractService _contractService;

    public ContractController(IContractService contractService)
    {
        _contractService = contractService;
    }

    [HttpPost("createContract")]
    [Authorize(Roles = "user,admin")]
    public async Task<IActionResult> CreateContract(ContractRequestDto dto)
    {
        try
        {
            return Ok(await _contractService.CreateContract(dto));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    [HttpDelete("deleteContract/{id}")]
    [Authorize(Roles = "user,admin")]
    public async Task<IActionResult> DeleteContract(int id)
    {
        try
        {
            await _contractService.DeleteContract(id);
            return NoContent();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost("makePayment/{id}")]
    [Authorize(Roles = "user,admin")]
    public async Task<IActionResult> MakePayment(int id, PaymentDto dto)
    {
        try
        {
            await _contractService.MakePayment(id, dto);
            return Ok();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (PaymentException e)
        {
            return BadRequest(e.Message);
        }
    }
}