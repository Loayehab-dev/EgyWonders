using Microsoft.EntityFrameworkCore;
using EgyWonders.IRepository;
using EgyWonders.Models;
using EgyWonders.Data;

namespace EgyWonders.Repository
{
    public class ListingRepository:IListingRepository
    {
        private readonly TravelDbContext _context;

        public ListingRepository(TravelDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Listing>> GetAllAsync() => await _context.Listings.AsNoTracking().ToListAsync();

        public async Task<Listing?> GetByIdAsync(int id)
        {
            return await _context.Listings.FirstOrDefaultAsync(l => l.ListingId == id);
        }
        public async Task<IEnumerable<Listing>> GetByCityAsync(string city) =>
            await _context.Listings.Where(l => l.CityName == city).ToListAsync();

        public async Task AddAsync(Listing listing)
        {
            await _context.Listings.AddAsync(listing);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Listing listing)
        {
            _context.Listings.Update(listing);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int listingId)
        {
            var listing = await _context.Listings
                .Include(l => l.Reviews)
                .Include(l => l.ListingPhotos)
                .FirstOrDefaultAsync(l => l.ListingId == listingId);

            if (listing == null)
                return;

            // Remove dependent tables
            if (listing.Reviews.Any())
                _context.Reviews.RemoveRange(listing.Reviews);

            if (listing.ListingPhotos.Any())
                _context.ListingPhotos.RemoveRange(listing.ListingPhotos);


            

            _context.Listings.Remove(listing);

            await _context.SaveChangesAsync();
        }

    }
}
