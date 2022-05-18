using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System.Linq;

namespace ApplicationCore.Services;
public class ReservationService : IReservationService
{
    private IRepository<Reservation> _reservationRepository;
    private IRepository<Show> _showRepository;
    private IShowService _showService;
    // private IRepository<Theatre> _theatreRepository;
    // private IRepository<Movie> _movieRepository;


    public ReservationService(IRepository<Reservation> reservationRepository, IRepository<Show> showRepository, IRepository<Theatre> theatreRepository, IShowService showService)
    {

        _reservationRepository = reservationRepository;
        _showRepository = showRepository;
        _showService = showService;
        // _theatreRepository = theatreRepository;
        // _movieRepository = movieRepository;
    }

    public async Task<Reservation> CreateReservationAsync(ReservationRequest request)
    {
        try
        {
            var availableSeats = await _showService.GetAvailableSeats(request.ShowId);
            if (availableSeats < request.BookedSeats)
            {
                return null;
            }
            else
            {
                Reservation reservation = new Reservation
                {
                    ShowId = request.ShowId,
                    BookedSeats = request.BookedSeats,
                    Email = request.Email,
                    TimeStamp = DateTime.Now,
                    ReservNr = Guid.NewGuid()
                };

                return await _reservationRepository.AddAsync(reservation);
            }
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<Reservation>> GetAllReservationsAsync()
    {
        return await _reservationRepository.GetListAsync();
    }

    public async Task<Reservation> GetReservationByGuidAsync(Guid guid)
    {
        try
        {
            var reservations = await _reservationRepository.GetListAsync();
            var reservation = reservations.FirstOrDefault(r => r.ReservNr == guid);
            return reservation;
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<Reservation>> GetReservationByShowIdAsync(int showId)
    {
        var reservations = await GetAllReservationsAsync();
        try
        {
            return reservations.Where(r => r.ShowId == showId).ToList();
        }
        catch
        {
            return null;
        }
    }

    public async Task<Reservation> GetReservationByEmailAndReservNrAsync(string email, string reservNr)
    {
        var resevations = await GetAllReservationsAsync();
        var reservation = resevations.Find(r => r.Email.ToLower() == email.ToLower() && r.ReservNr.ToString().Substring(0, 6) == reservNr.ToLower());
        var show = await _showRepository.GetByIdAsync(reservation.ShowId);

        if (show.ShowEnds < DateTime.Now)
        {
            return reservation;
        }
        return null;
    }

    public async Task<bool> RemoveReservationAsync(Reservation reservation)
    {
        return await _reservationRepository.DeleteAsync(reservation);

    }
}