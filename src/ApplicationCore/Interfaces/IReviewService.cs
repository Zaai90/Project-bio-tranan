using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IReviewService
    {
        Task<List<Review>> GetAllReviewsAsync();
        Task<Review> CreateReviewAsync(Review review);
        Task<List<Review>> GetReviewsByMovieIdAsync(int id);

    }
}