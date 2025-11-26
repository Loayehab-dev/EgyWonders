using EgyWonders.IRepository;
using EgyWonders.Models;
using EgyWonders.Data;
using Microsoft.EntityFrameworkCore;

namespace EgyWonders.Repository
{
    public class TourBookingRepository : ITourBookingRepository
    {
        private readonly TravelDbContext _context;

        public TourBookingRepository(TravelDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TourBooking>> GetAllAsync()
        {
            return await _context.TourBookings.ToListAsync();
        }

        public async Task<TourBooking?> GetByIdAsync(int id)
        {
            return await _context.TourBookings.FindAsync(id);
        }

        public async Task AddAsync(TourBooking booking)
        {
            await _context.TourBookings.AddAsync(booking);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TourBooking booking)
        {
            _context.TourBookings.Update(booking);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var booking = await _context.TourBookings.FindAsync(id);
            if (booking != null)
            {
                _context.TourBookings.Remove(booking);
                await _context.SaveChangesAsync();
            }
        }
    }
}
