using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System.Text.Json;

namespace ApplicationCore.Services;
public class MovieService : IMovieService
{
    private IRepository<Movie> _movieRepository;
    private string _baseUrl = "https://www.omdbapi.com/";
    private string _apiKey = "apikey=da3ce995";



    public MovieService(IRepository<Movie> movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<Movie> CreateMovieAsync(Movie movie)
    {
        try
        {
            return await _movieRepository.AddAsync(movie);
        }
        catch
        {
            return null;
        }
    }

    public async Task<Movie> CreateMovieByTitleAsync(string title, int timesToBePlayed = 10)
    {
        try
        {
            if (_movieRepository.GetListAsync().Result.Any(m => m.Title.ToLower() == title.ToLower()))
            {
                return null;
            }

            HttpClient client = new HttpClient();
            var response = await client.GetAsync($"{_baseUrl}?t={title}&plot=full&{_apiKey}");
            Movie movie = await JsonSerializer.DeserializeAsync<Movie>(await response.Content.ReadAsStreamAsync());
            movie.TimesToBePlayed = timesToBePlayed;
            movie.Subtitles = "Svenska";
            movie.Duration = int.Parse(movie.Runtime.Substring(0, movie.Runtime.IndexOf(" ")));

            return await _movieRepository.AddAsync(movie);
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<Movie>> GetAllMoviesAsync()
    {
        return await _movieRepository.GetListAsync();
    }

    public async Task<Movie> GetMovieByIdAsync(int id)
    {
        try
        {
            return await _movieRepository.GetByIdAsync(id);
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> RemoveMovieAsync(int id)
    {
        try
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            return await _movieRepository.DeleteAsync(movie);
        }
        catch
        {
            return false;
        }
    }
}
