using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservationController : ControllerBase
{
    private IReservationService _reservationService;
    private readonly ILogger<ReservationController> _logger;

    public ReservationController(IReservationService reservationService, ILogger<ReservationController> logger)
    {
        _reservationService = reservationService;
        _logger = logger;
    }

    [HttpGet("/Reservations")]
    public async Task<ActionResult<List<Reservation>>> GetAllReservationsAsync()
    {
        _logger.LogInformation("GetAllReservationsAsync");
        var result = await _reservationService.GetAllReservationsAsync();
        return Ok(result);
    }

    [HttpGet("/Reservations/{guid:guid}")]
    public async Task<ActionResult<Reservation>> GetReservationByGuidAsync(Guid guid)
    {
        var result = await _reservationService.GetReservationByGuidAsync(guid);
        return Ok(result);
    }

    [HttpGet("/Reservations/Show/{showId:int}")]
    [ActionName("GetReservationByIdAsync")]
    public async Task<ActionResult<List<Reservation>>> GetReservationByShowIdAsync(int showId)
    {
        var result = await _reservationService.GetReservationByShowIdAsync(showId);
        return Ok(result);
    }

    [HttpPost("/Reservations")]
    public async Task<ActionResult<Reservation>> PostReservationAsync(ReservationRequest request)
    {
        var result = await _reservationService.CreateReservationAsync(request);
        return CreatedAtAction(nameof(GetReservationByGuidAsync), new { id = result.Id }, result);
    }
}