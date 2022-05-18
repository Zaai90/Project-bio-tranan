using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TheatreController : ControllerBase
{
    private IRepository<Theatre> _theatreRepository;
    private readonly ILogger<TheatreController> _logger;

    public TheatreController(ILogger<TheatreController> logger, IRepository<Theatre> repository)
    {
        _theatreRepository = repository;
        _logger = logger;
    }

    [HttpPost("/Theatres")]
    public async Task<ActionResult<Theatre>> PostTheatreAsync(Theatre theatre)
    {
        var result = await _theatreRepository.AddAsync(theatre);
        return CreatedAtAction(nameof(GetTheatreByIdAsync), new { id = result.Id }, result);
    }

    [HttpGet("/Theatres/{id:int}")]
    public async Task<ActionResult<Theatre>> GetTheatreByIdAsync(int id)
    {
        var result = await _theatreRepository.GetByIdAsync(id);
        return Ok(result);
    }
}