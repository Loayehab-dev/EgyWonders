using EgyWonders.Models;
namespace EgyWonders.IRepository
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAllAsync();
        Task<Review> GetByIdAsync(int id);
        Task<IEnumerable<Review>> GetByListingIdAsync(int listingId);
        Task<IEnumerable<Review>> GetByTourIdAsync(int tourId);
        Task AddAsync(Review review);
        Task UpdateAsync(Review review);
        Task DeleteAsync(int id);
    }
}
