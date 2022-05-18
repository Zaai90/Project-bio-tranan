using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces;

public interface IShowService
{
    Task<Show> CreateShowAsync(ShowRequest request);
    Task<Show> GetShowAsync(int id);
    Task<List<Show>> GetAllShowsAsync();
    Task<bool> DeleteShowAsync(int id);
    // Task<bool> ToggleRestrictedSeatsAsync(int id, int procentAllowed = 50);
    Task<List<Show>> GetShowsByMovieIdAsync(int id);
    Task<int> GetAvailableSeats(int id);
}