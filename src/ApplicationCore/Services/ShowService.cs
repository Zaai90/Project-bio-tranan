using ApplicationCore.Entities;
using ApplicationCore.Interfaces;

namespace ApplicationCore.Services;
public class ShowService : IShowService
{
    private IRepository<Show> _showRepository;
    private IRepository<Theatre> _theatreRepository;
    private IRepository<Movie> _movieRepository;
    private IRepository<Reservation> _reservationRepository;

    private int MaintainTime = 20;

    public ShowService(IRepository<Show> showRepository, IRepository<Theatre> theatreRepository, IRepository<Movie> movieRepository, IRepository<Reservation> reservationRepository)
    {
        _showRepository = showRepository;
        _theatreRepository = theatreRepository;
        _movieRepository = movieRepository;
        _reservationRepository = reservationRepository;
    }


    public async Task<Show> CreateShowAsync(ShowRequest request)
    {
        var show = new Show();
        show.ShowDateAndTime = request.ShowDateAndTime;
        show.Price = request.Price;
        show.MovieId = request.MovieId;
        show.TheatreId = request.TheatreId;

        var shows = await _showRepository.GetListAsync(new string[] { "Movie", "Theatre" });

        if (shows.Any(x => (show.ShowDateAndTime <= x.ShowDateAndTime.AddMinutes(x.Movie.Duration + MaintainTime))
            && (x.ShowDateAndTime <= show.ShowDateAndTime.AddMinutes(show.Movie.Duration + MaintainTime)) && show.Theatre.Id == x.Theatre.Id))
        {
            return null;
        }
        if (show.ShowDateAndTime < DateTime.Now.AddDays(2))
        {
            return null;
        }

        var ExistingMovie = await _movieRepository.GetByIdAsync(show.MovieId);
        if (ExistingMovie.TimesPlayed >= ExistingMovie.TimesToBePlayed)
        {
            return null;
        }
        else
        {
            ExistingMovie.TimesPlayed++;
            await _movieRepository.UpdateAsync(ExistingMovie);
        }

        return await _showRepository.AddAsync(show);
    }

    public async Task<int> GetAvailableSeats(int id)
    {
        var show = await _showRepository.GetByIdAsync(id);
        var theatre = await _theatreRepository.GetByIdAsync(show.TheatreId);
        var seats = theatre.AmountOfSeats;
        var reservations = await _reservationRepository.GetListAsync();
        var reservationsByShow = reservations.Where(x => x.ShowId == id);
        var reservedSeats = reservationsByShow.Sum(x => x.BookedSeats);
        var availableSeats = seats - reservedSeats;

        return availableSeats;
    }

    private async Task<Show> BuildNewShow(ShowRequest request)
    {
        var show = await _showRepository.GetByIdAsync(request.Id);
        show.Movie = await _movieRepository.GetByIdAsync(request.MovieId);
        show.Theatre = await _theatreRepository.GetByIdAsync(request.TheatreId);
        return show;
    }

    private async Task<Show> BuildExistingShow(int id)
    {
        var show = await _showRepository.GetByIdAsync(id);
        show.Movie = await _movieRepository.GetByIdAsync(show.MovieId);
        show.Theatre = await _theatreRepository.GetByIdAsync(show.TheatreId);
        return show;
    }

    public async Task<Show> GetShowAsync(int id)
    {
        try
        {
            return await BuildExistingShow(id);
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<Show>> GetAllShowsAsync()
    {
        var paths = new string[] { "Movie", "Theatre" };
        List<Show> shows = await _showRepository.GetListAsync(paths);
        return shows.Where(x => x.ShowDateAndTime > DateTime.Now).OrderBy(x => x.ShowDateAndTime).ToList();

    }

    // public async Task<bool> ToggleRestrictedSeatsAsync(int id, int procentAllowed = 50)
    // {
    //     //TODO Ändra till att skötas i RestrictionService
    //     var show = await _showRepository.GetByIdAsync(id);
    //     show.LimitedProcent = procentAllowed;
    //     show.IsLimited = !show.IsLimited;
    //     if (procentAllowed > 100 || procentAllowed < 0)
    //     {
    //         return false;
    //     }
    //     return await _showRepository.UpdateAsync(show);
    // }

    public async Task<List<Show>> GetShowsByMovieIdAsync(int id)
    {
        var shows = await _showRepository.GetListAsync(new string[] { "Movie", "Theatre" });
        return shows.Where(x => x.Movie.Id == id).ToList();
    }

    public async Task<bool> DeleteShowAsync(int id)
    {
        var show = await _showRepository.GetByIdAsync(id);
        return await _showRepository.DeleteAsync(show);
    }
}