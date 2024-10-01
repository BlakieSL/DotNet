using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project3.DTOs;
using Project3.Exceptions;
using Project3.Services;

namespace Project3.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpPost("addIndividual")]
    [Authorize(Roles = "user,admin")]
    public async Task<IActionResult> AddIndividual([FromBody] IndividualRequestDto dto)
    {
        try
        {
            return Ok(await _clientService.AddIndividual(dto));
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    [HttpPost("addCompany")]
    [Authorize(Roles = "user,admin")]
    public async Task<IActionResult> AddCompany([FromBody] CompanyRequestDto dto)
    {
        try
        {
            return Ok(await _clientService.AddCompany(dto));
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
    
    [HttpDelete("deleteIndividual/{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteIndividual(int id)
    {
        try
        {
            await _clientService.DeleteIndividual(id);
            return NoContent();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpPut("updateIndividual/{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> UpdateIndividual(int id, 
        [FromBody] IndividualRequestUpdateDto dto)
    {
        try
        {
            return Ok(await _clientService.UpdateIndividual(id, dto));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
    
    [HttpPut("updateCompany/{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> UpdateCompany(int id,
        [FromBody] CompanyRequestUpdateDto dto)
    {
        try
        {
            return Ok(await _clientService.UpdateCompany(id, dto));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
}