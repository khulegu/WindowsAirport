using AirportServer.Data;
using AirportServer.Models;
using AirportServer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirportServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PassengerController : ControllerBase
{
    private readonly PassengerService _passengerService;

    public PassengerController(PassengerService passengerService)
    {
        _passengerService = passengerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var passengers = await _passengerService.GetAllAsync();
        return Ok(passengers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var passenger = await _passengerService.GetByIdAsync(id);
        if (passenger == null)
            return NotFound();
        return Ok(passenger);
    }

    [HttpGet("by-passport/{passportNumber}")]
    public async Task<IActionResult> GetByPassportNumber(string passportNumber)
    {
        var passenger = await _passengerService.GetByPassportNumberAsync(passportNumber);
        if (passenger == null)
            return NotFound();
        return Ok(passenger);
    }

    /// <summary>
    /// Зорчигчийн мэдээллийг бүртгүүлэх
    /// </summary>
    /// <param name="passenger">Зорчигчийн мэдээлэл</param>
    /// <returns>Бүртгүүлсэн зорчигчийн мэдээлэл</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Passenger passenger)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var created = await _passengerService.CreateAsync(passenger);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Passenger passenger)
    {
        if (id != passenger.Id)
            return BadRequest("ID mismatch");
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var success = await _passengerService.UpdateAsync(passenger);
        if (!success)
            return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _passengerService.DeleteAsync(id);
        if (!success)
            return NotFound();
        return NoContent();
    }
}
