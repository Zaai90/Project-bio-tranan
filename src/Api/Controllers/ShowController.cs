using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ShowController : ControllerBase
{
    private IShowService _showService;
    private IShowRestrictionService _showRestrictionService;

    public ShowController(IShowRestrictionService showRestrictionService, IShowService showService)
    {
        _showService = showService;
        _showRestrictionService = showRestrictionService;
    }

    [HttpGet("/Shows")]
    public async Task<ActionResult<List<Show>>> GetShowAsync()
    {
        var result = await _showService.GetAllShowsAsync();
        return Ok(result.OrderBy(x => x.ShowDateAndTime));
    }

    [HttpGet("/Shows/{id}")]
    [ActionName("GetShowByIdAsync")]
    public async Task<ActionResult<Show>> GetShowByIdAsync(int id)
    {
        var result = await _showService.GetShowAsync(id);
        return Ok(result);
    }

    [HttpPost("/Show")]
    public async Task<ActionResult<Show>> PostShowAsync(ShowRequest showRequest)
    {
        try
        {
            var result = await _showService.CreateShowAsync(showRequest);
            return CreatedAtAction(nameof(GetShowByIdAsync), new
            {
                id = result.Id
            }, result);
        }
        catch
        {
            return BadRequest("Could not add show");
        }
    }

    [HttpPut("/restrict/{id:int}/{removedSeats:int?}")]
    public async Task<ActionResult<ShowRestriction>> PutRestrictShowAsync(int id, int removedSeats = 22)
    {
        var result = await _showRestrictionService.CreateShowRestrictionAsync(new ShowRestriction
        {
            ShowId = id,
            RemovedSeats = removedSeats
        });
        return result != null ? Ok() : BadRequest("Could not restrict seats");
    }

    [HttpDelete("/Shows/{id}")]
    public async Task<ActionResult<Show>> DeleteShowAsync(int id)
    {
        var result = await _showService.DeleteShowAsync(id);
        if (result)
        {
            return Ok();
        }
        else
        {
            return BadRequest("Could not delete show");
        }
    }

}