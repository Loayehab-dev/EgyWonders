using Microsoft.EntityFrameworkCore;
using EgyWonders.IRepository;
using EgyWonders.Models;
using EgyWonders.Data;

namespace EgyWonders.Repository
{
    public class ListingBookingRepository:IListingBookingRepository
    {
        private readonly TravelDbContext _context;

        public ListingBookingRepository(TravelDbContext context)
        {
            _context = context;
        }
       
              public async Task<IEnumerable<ListingBooking>> GetByListingIdAsync(int listingId)
        {
            return await _context.ListingBookings
                .Where(b => b.ListingId == listingId)
                .ToListAsync();
        }
        

        public async Task<IEnumerable<ListingBooking>> GetAllAsync() => await _context.ListingBookings.AsNoTracking().ToListAsync();

        public async Task<ListingBooking> GetByIdAsync(int id) => await _context.ListingBookings.FindAsync(id);

        public async Task AddAsync(ListingBooking booking)
        {
            await _context.ListingBookings.AddAsync(booking);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ListingBooking booking)
        {
            _context.ListingBookings.Update(booking);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var booking = await _context.ListingBookings.FindAsync(id);
            if (booking != null)
            {
                _context.ListingBookings.Remove(booking);
                await _context.SaveChangesAsync();
            }
        }

        
    }
}
