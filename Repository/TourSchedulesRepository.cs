using Microsoft.EntityFrameworkCore;
using EgyWonders.IRepository;
using EgyWonders.Models;
using EgyWonders.Data;

namespace EgyWonders.Repository
{
    public class TourSchedulesRepository : ITourSchedulesRepository
    {
        private readonly TravelDbContext _context;

        public TourSchedulesRepository(TravelDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TourSchedule>> GetAllAsync() => await _context.TourSchedules.AsNoTracking().ToListAsync();

        public async Task<TourSchedule> GetByIdAsync(int id) =>
            await _context.TourSchedules
                .Include(s => s.Tour)
                .AsNoTracking()  
                .FirstOrDefaultAsync(s => s.ScheduleId == id);
        public async Task<IEnumerable<TourSchedule>> GetByTourIdAsync(int tourId) =>
            await _context.TourSchedules.Where(s => s.TourId == tourId).AsNoTracking().ToListAsync();

        public async Task AddAsync(TourSchedule schedule)
        {
            await _context.TourSchedules.AddAsync(schedule);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TourSchedule schedule)
        {
            _context.TourSchedules.Update(schedule);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var schedule = await _context.TourSchedules.FindAsync(id);
            if (schedule != null)
            {
                _context.TourSchedules.Remove(schedule);
                await _context.SaveChangesAsync();
            }
        }
    }
}
