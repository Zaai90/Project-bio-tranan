using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IMovieService
    {
        Task<Movie> CreateMovieAsync(Movie movie);
        Task<Movie> CreateMovieByTitleAsync(string title, int timesToBePlayed = 10);
        Task<Movie> GetMovieByIdAsync(int id);
        Task<List<Movie>> GetAllMoviesAsync();
        Task<bool> RemoveMovieAsync(int id);
    }
}