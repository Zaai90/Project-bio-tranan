using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private IMovieService _movieService;

    public MovieController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpGet("/Movies")]
    public async Task<ActionResult<List<Movie>>> GetAllMoviesAsync()
    {
        var result = await _movieService.GetAllMoviesAsync();
        return Ok(result);
    }


    [HttpGet("/Movies/{id}")]
    [ActionName("GetMovieByIdAsync")]
    public async Task<ActionResult<Movie>> GetMovieByIdAsync(int id)
    {
        try
        {
            var result = await _movieService.GetMovieByIdAsync(id);
            return Ok(result);
        }
        catch
        {
            return NotFound();
        }
    }

    [HttpPost("/Movies")]
    public async Task<ActionResult<Movie>> PostMovieAsync(Movie movie)
    {
        try
        {
            var result = await _movieService.CreateMovieAsync(movie);
            return CreatedAtAction(nameof(GetMovieByIdAsync), new { id = movie.Id }, movie);
        }
        catch
        {
            return NoContent();
        }
    }

    [HttpDelete("/Movies/{id}")]
    public async Task<ActionResult<Movie>> DeleteMovieIdAsync(int id)
    {
        var result = await _movieService.RemoveMovieAsync(id);
        if (result)
        {
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }
}