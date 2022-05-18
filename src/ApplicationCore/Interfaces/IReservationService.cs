using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IReservationService
    {
        Task<Reservation> CreateReservationAsync(ReservationRequest request);
        Task<Reservation> GetReservationByGuidAsync(Guid guid);
        Task<List<Reservation>> GetAllReservationsAsync();
        Task<bool> RemoveReservationAsync(Reservation reservation);
        Task<List<Reservation>> GetReservationByShowIdAsync(int showId);
        Task<Reservation> GetReservationByEmailAndReservNrAsync(string email, string reservNr);
    }
}