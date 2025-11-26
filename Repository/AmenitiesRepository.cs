using Microsoft.EntityFrameworkCore;
using EgyWonders.IRepository;
using EgyWonders.Models;
using EgyWonders.Data;

namespace EgyWonders.Repository
{
    public class AmenitiesRepository: IAmenitiesRepository
    {
        private readonly TravelDbContext _context;

        public AmenitiesRepository(TravelDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Amenity>> GetAllAsync() => await _context.Amenities.AsNoTracking().ToListAsync();

        public async Task<Amenity> GetByIdAsync(int id) => await _context.Amenities.FindAsync(id);

        public async Task AddAsync(Amenity amenity)
        {
            await _context.Amenities.AddAsync(amenity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Amenity amenity)
        {
            _context.Amenities.Update(amenity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var amenity = await _context.Amenities.FindAsync(id);
            if (amenity != null)
            {
                _context.Amenities.Remove(amenity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
