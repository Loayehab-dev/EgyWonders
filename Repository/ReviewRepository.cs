using Microsoft.EntityFrameworkCore;
using EgyWonders.IRepository;
using EgyWonders.Models;
using EgyWonders.Data;

namespace EgyWonders.Repository
{
    public class ReviewRepository:IReviewRepository
    {
        private readonly TravelDbContext _context;

        public ReviewRepository(TravelDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Review>> GetAllAsync() => await _context.Reviews.AsNoTracking().ToListAsync();

        public async Task<Review > GetByIdAsync(int id) => await _context.Reviews.FindAsync(id);

        public async Task<IEnumerable<Review>> GetByListingIdAsync(int listingId) =>
            await _context.Reviews.Where(r => r.ListingId == listingId).ToListAsync();

        public async Task<IEnumerable<Review>> GetByTourIdAsync(int tourId) =>
            await _context.Reviews.Where(r => r.TourId== tourId).AsNoTracking().ToListAsync();

        public async Task AddAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Review review)
        {
            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }
        }
    }
}
