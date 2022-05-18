using ApplicationCore.Entities;
using ApplicationCore.Interfaces;

namespace ApplicationCore.Services;
public class ReviewService : IReviewService
{
    private IRepository<Movie> _movieRepository;
    private IRepository<Review> _reviewRepository;
    private IRepository<Reservation> _reservationRepository;


    public ReviewService(IRepository<Movie> movieRepository, IRepository<Review> reviewRepository, IRepository<Reservation> reservationRepository)
    {
        _movieRepository = movieRepository;
        _reviewRepository = reviewRepository;
        _reservationRepository = reservationRepository;
    }

    public async Task<Review> CreateReviewAsync(Review review)
    {
        return await _reviewRepository.AddAsync(review);
    }

    public async Task<List<Review>> GetAllReviewsAsync()
    {
        return await _reviewRepository.GetListAsync();
    }

    public async Task<List<Review>> GetReviewsByMovieIdAsync(int id)
    {
        var reviews = await _reviewRepository.GetListAsync();
        return reviews.Where(x => x.MovieId == id).ToList();
    }
}
