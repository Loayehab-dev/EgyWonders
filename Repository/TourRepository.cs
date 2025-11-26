using Microsoft.EntityFrameworkCore;
using EgyWonders.IRepository;
using EgyWonders.Models;
using EgyWonders.Data;

namespace EgyWonders.Repository
{
    public class TourRepository:ITourRepository
    {
        private readonly TravelDbContext _context;

        public TourRepository(TravelDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tour>> GetAllAsync() => await _context.Tours.AsNoTracking().ToListAsync();

        public async Task<Tour> GetByIdAsync(int id) => await _context.Tours.FindAsync(id);

        public async Task<IEnumerable<Tour>> GetByCityAsync(string city) =>
            await _context.Tours.AsNoTracking().Where(t => t.CityName == city).ToListAsync();

        public async Task AddAsync(Tour tour)
        {
            await _context.Tours.AddAsync(tour);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Tour tour)
        {
            _context.Tours.Update(tour);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var tour = await _context.Tours.FindAsync(id);
            if (tour != null)
            {
                _context.Tours.Remove(tour);
                await _context.SaveChangesAsync();
            }
        }
    }    
}
